using System;
using SimulatedBE.Networking.DTOs;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using SimulatedBE.Networking.BoosterPacks;
using SimulatedBE.Networking.Tags;
using Random = System.Random;
using SimulatedBE.Networking.Jokers;
using SimulatedBE.Networking.Consumable;
using SimulatedBE.Networking.Modifiers;
using SimulatedBE.Networking.Vouchers;

namespace SimulatedBE.Networking
{
    public class GameRules
    {
        public event Action OnNewAnte;
        
        private IJsonFacade _jsonFacade;
        private BreachManager _breachManager;
        private ShopManager _shopManager;
        private BaseStatsDto _baseData;
        private List<DeckDto> _loadedDecks;
        private PokerRules _myRules;
        private BossManager _bossManager;
        private ConsumableManager _consumableManager;
        private VoucherManager _voucherManager;
        private TagManager _tagManager;
        private ModifierManager _modifierManager;
        private JokerManager _jokerManager;
        private BoosterPacksManager _boosterPacksManager;
        private List<CardDto> _getCurrentDeck;
        private const int _interestDivider = 5;

        public List<DeckDto> LoadedDecks => _loadedDecks;

        public async UniTask Initialize()
        {
            await SaveDataInDevice.GenerateDataInDevice();

            _jsonFacade = new JsonFacade();
            _myRules = new PokerRules();
            _bossManager = new BossManager();
            _breachManager = new BreachManager();
            _boosterPacksManager = new BoosterPacksManager();
            _shopManager = new ShopManager(this);
            
            _consumableManager = new ConsumableManager(_jsonFacade, _modifierManager,this);
            _voucherManager = new VoucherManager(_jsonFacade);
            _tagManager = new TagManager(_jsonFacade, this);
            _jsonFacade.CurrentRules = _myRules.GetRules();
            _jokerManager = new JokerManager(_jsonFacade, _myRules);
            _modifierManager = new ModifierManager(_jsonFacade);
            _baseData = JsonSerializer.ReadJsonFile<BaseStatsDto>(JSONFilesNames.Preset_BaseStats, Paths.BaseStats);
            FillDecks();
        }

        public void SaveSelectedDeck(DeckDto selectedDeck)
        {
            _shopManager = new ShopManager(this);
            var database = new Database(_baseData.AmmountOfPlayHands, _baseData.AmmountOfDiscards);
            database.CurrentID = selectedDeck.Cards.Count;
            _jsonFacade.Database = database;
            var newBreaches = _breachManager.CreateNewBreaches(_baseData, _bossManager, _jsonFacade.Database.CurrentInitialBet);
            _getCurrentDeck = selectedDeck.Cards;

            _jsonFacade.PlayerCurrentDeck = selectedDeck;
            _jsonFacade.CurrentInventory = new InventoryDto(new(), new(), new(), new(),0, _baseData.JokerSlotsAmmount, _baseData.ConsumableSlotsAmmount);
            _jsonFacade.GameBreaches = newBreaches;
            _jsonFacade.PlayerCurrentHand = new List<CardDto>();
            _jsonFacade.RoundData = new RoundDto(database.PlayHandsAmmount, database.DiscardsAmmount, 0, 0, new ScoreDto());
        }

        #region Cards Management

        public List<CardDto> GetUsedCardsDeck()
        {
            return _getCurrentDeck;
        }
        public List<CardDto> GetShuffleCards()
        {   
            MixCurrentDeck();

            _jsonFacade.PlayerCurrentHand = new List<CardDto>();
            _jsonFacade.UsedCards = new List<CardDto>();

            _tagManager.GetFirstHand();
            var newCards = GetNewCards(_baseData.AmountOfCardsInHand);


            return _bossManager.IsBossActivated() ? _bossManager.FirstHandEffect(_jsonFacade.PlayerCurrentHand) : _jsonFacade.PlayerCurrentHand;
        }

        public DiscardCardResultDto DiscardCard(List<CardDto> cardsToDiscard)
        {
            if (_jsonFacade.RoundData.AmountOfDiscards <= 0)
                return new DiscardCardResultDto(_jsonFacade.PlayerCurrentDeck.Cards, _jsonFacade.RoundData.AmountOfDiscards);

            var currentDatabase = _jsonFacade.Database;
            currentDatabase.DiscardedCards += cardsToDiscard.Count;
            _jsonFacade.Database = currentDatabase;

            DiscardHandCards(cardsToDiscard);

            var currentRound = _jsonFacade.RoundData;
            _jsonFacade.RoundData = new RoundDto(
                currentRound.AmountOfHands,
                currentRound.AmountOfDiscards - 1,
                currentRound.CurrentRound,
                currentRound.InitialBet,
                currentRound.Score);

            var newCards = GetNewCards(AmmountOfCardsToMax());

            return new DiscardCardResultDto(
                _bossManager.IsBossActivated() ? _bossManager.OnDealNewCardsEffect(newCards) : newCards,
                _jsonFacade.RoundData.AmountOfDiscards);
        }

        public PlayHandResultDto PlayHand(List<CardDto> cardsPlayed)
        {

            _myRules.LoadRules(_jsonFacade.CurrentRules);
            var pokerHandAndScoredCards = _myRules.CheckPokerHand(cardsPlayed);
            var pokerHand = pokerHandAndScoredCards.Item1;

            var currentDatabase = _jsonFacade.Database;
            currentDatabase.LastHandPlayed = pokerHand;
            currentDatabase.NumberOfCardsPlayed += cardsPlayed.Count;
            _jsonFacade.Database = currentDatabase;
            DiscardHandCards(cardsPlayed);
            var newCards = GetNewCards(AmmountOfCardsToMax());

            if (_bossManager.IsBossActivated())
            {
                pokerHand = _bossManager.OnPlayHandEffect(pokerHand);
                newCards = _bossManager.OnDealNewCardsEffect(newCards);
            }

            _myRules.AddToPlayAmount(pokerHand);

            _jsonFacade.CurrentRules = _myRules.GetRules();

            var score = CalculateScore(pokerHand, pokerHandAndScoredCards.Item2);

            var currentRound = _jsonFacade.RoundData;

            var scoredto = new ScoreDto(currentRound.Score.ScoreValue, score.ChipsValue, score.MultiplierValue);

            var newRound = new RoundDto(
                currentRound.AmountOfHands - 1,
                currentRound.AmountOfDiscards,
                currentRound.CurrentRound,
            currentRound.InitialBet,
            scoredto);

            _jsonFacade.RoundData = newRound;

            return new PlayHandResultDto(pokerHand, pokerHandAndScoredCards.Item2 ,cardsPlayed, newCards, newRound);
        }

        public CalculatedScoreDto CalculateScore()
        {
            var currentRound = _jsonFacade.RoundData;
            currentRound = currentRound.SetScore(new ScoreDto(currentRound.Score.ScoreValue,currentRound.Score.ChipsValue, (long)(currentRound.Score.MultiplierValue * _tagManager.Multiplier)));
            var calculate = (currentRound.Score.ChipsValue * currentRound.Score.MultiplierValue);

            var score = new ScoreDto(currentRound.Score.ScoreValue + calculate, 0,0);

            var newRound = new RoundDto(
                currentRound.AmountOfHands,
                currentRound.AmountOfDiscards,
                currentRound.CurrentRound,
            currentRound.InitialBet,
            score);

            var database = _jsonFacade.Database;
            if (database.BestHandScore < calculate)
            {
                database.BestHandScore = calculate;
                _jsonFacade.Database = database;
            }

            _jsonFacade.RoundData = newRound;

            return new CalculatedScoreDto(newRound, CheckWin());
        }

        public void DrawCards(int cardsToDraw) => GetNewCards(cardsToDraw);

        int AmmountOfCardsToMax()
        {
            int currentHand = _jsonFacade.PlayerCurrentHand.Count;
            int max = _baseData.AmountOfCardsInHand;

            return UnityEngine.Mathf.Clamp(max - currentHand, 0, max);
        }

        private void ResetUseCardDeckValues()
        {
            foreach (var cardDto in _getCurrentDeck)
            {
                cardDto.ResetCardState();
            }
        }

        private void UseDeckCard(string cardId)
        {
            CardDto? cardToUse = _getCurrentDeck.FirstOrDefault(card => card.ID == cardId);
            if (cardToUse.HasValue)
            {
                cardToUse.Value.UseCard();
            }
        }

        #endregion

        #region Breaches Management

        public List<BreachDto> GetAllBreaches()
        {
            var breaches = _jsonFacade.GameBreaches;
            for (int i = 0; i < breaches.Count; i++)
            {
                if (breaches[i].BreachType == BreachType.Boss) continue;
                    var newTag = _tagManager.UpdateDynamicDescription(breaches[i].Tag);
                breaches[i] = new BreachDto(breaches[i].Name, breaches[i].Description, breaches[i].BasicReward, breaches[i].BreachState, breaches[i].BreachType, breaches[i].Current, breaches[i].ObjectiveScore, newTag);
            }
            _jsonFacade.GameBreaches = breaches;
            return breaches;
        }

        public TagDto GetTagReward() => _breachManager.GetCurrentBreach(_jsonFacade.GameBreaches).Tag;

        public SelectBreachResultDto SendSelectBreachMessage()
        {
            var currentBreach = _breachManager.GetCurrentBreach(_jsonFacade.GameBreaches);
            ResetUseCardDeckValues();
            var database = _jsonFacade.Database;

            _jsonFacade.RoundData = new RoundDto(database.PlayHandsAmmount,
                database.DiscardsAmmount,
                _jsonFacade.RoundData.CurrentRound + 1,
                _jsonFacade.Database.CurrentInitialBet, new ScoreDto());

            return new SelectBreachResultDto(_jsonFacade.RoundData,
                _breachManager.GetCurrentBreach(_jsonFacade.GameBreaches), 4);
        }

        public SkipBreachResultDto SkipBreach()
        {
            var selectBreachResultDto = _breachManager.SetNewCurrentBreach(_jsonFacade.GameBreaches, true);
            _jsonFacade.GameBreaches = selectBreachResultDto.Item3;
            InventoryDto inventoryDto = _jsonFacade.CurrentInventory;
            inventoryDto.SavedTags.Add(selectBreachResultDto.Item1.Tag);
            _jsonFacade.CurrentInventory = inventoryDto;
            return new SkipBreachResultDto(selectBreachResultDto.Item1.Tag);
        }

        public void ApplyTag(TagDto tagToApply)
        {
            _tagManager.ApplyTag(tagToApply);
            InventoryDto inventoryDto = _jsonFacade.CurrentInventory;
            inventoryDto = inventoryDto.RemoveTag(tagToApply);
            _jsonFacade.CurrentInventory = inventoryDto;
        }

        #endregion

        #region PokerHand Management
        public List<PokerHandDto> GetPokerHands()
        {
            _myRules.LoadRules(_jsonFacade.CurrentRules);
            return _myRules.GetPokerHandDtos();
        }

        public PokerHandDto ValidatePokerHand(List<CardDto> cards)
        {
            _myRules.LoadRules(_jsonFacade.CurrentRules);
            return _myRules.CheckPokerHand(cards).Item1;
        }

        #endregion

        #region Game Condition Management
        public WinResultDto GetWinResult(BreachDto breach)
        {
            DiscardHandCards(_jsonFacade.PlayerCurrentHand);
            ReturnUsedCardsToMainDeck();
            _tagManager.SetTagMultiplier(1);
            
            var round = _jsonFacade.RoundData;
            int calculatedInterest = (round.AmountOfHands + breach.BasicReward) / _interestDivider;
            var selectBreachResultDto = _breachManager.SetNewCurrentBreach(_jsonFacade.GameBreaches, false);
            var newBreaches = selectBreachResultDto.Item3;
            UnityEngine.Debug.Log("Next Breach Name: " + selectBreachResultDto.Item2.Name);
                var database = _jsonFacade.Database;
            if (string.IsNullOrEmpty(selectBreachResultDto.Item2.Name))
            {
                database.CurrentInitialBet += 1;
                _jsonFacade.Database = database;
                newBreaches = _breachManager.CreateNewBreaches(_baseData, _bossManager, database.CurrentInitialBet);
                OnNewAnte?.Invoke();
                UnityEngine.Debug.Log("New breaches created");
            }

            _jsonFacade.RoundData = new RoundDto(database.PlayHandsAmmount,
                                                 database.DiscardsAmmount,
                                                 _jsonFacade.RoundData.CurrentRound,
                                                 _jsonFacade.Database.CurrentInitialBet, new ScoreDto());

            _jsonFacade.GameBreaches = newBreaches;

            var databaseTemporalTotalReward = selectBreachResultDto.Item1.BasicReward + round.AmountOfHands + calculatedInterest;
            var dataBaseTemporal = _jsonFacade.Database;

            dataBaseTemporal.temporalTotalReward = databaseTemporalTotalReward;
            _jsonFacade.Database = dataBaseTemporal;

            return new WinResultDto(selectBreachResultDto.Item1, round.AmountOfHands,
                round.AmountOfDiscards, calculatedInterest, _jsonFacade.Database.temporalTotalReward); ;
        }

        public int CollectTreasure()
        {
            var tempReward = _jsonFacade.Database.temporalTotalReward;
            AddPlayerTreasure(tempReward);
            return tempReward;
        }

        public EndOfGameResultDto EndOfGame()
        {
            var database = _jsonFacade.Database;
            var roundData = _jsonFacade.RoundData;
            _myRules.LoadRules(_jsonFacade.CurrentRules);
            _tagManager.SetTagMultiplier(1);
            // Needs more DTOS data.
            var endOfGameResultDto = new EndOfGameResultDto(
                database.BestHandScore,
                _myRules.GetPokerHandDtos().OrderByDescending(x => x.AmountOfTimesPlayed).First(),
                database.NumberOfCardsPlayed,
                database.DiscardedCards,
                database.CardsPurchased,
                database.Renewals,
                database.Discoveries,
                database.CurrentInitialBet,
                roundData.CurrentRound,
                _breachManager.GetCurrentBreach(_jsonFacade.GameBreaches).Name
                );
            return endOfGameResultDto;
        }
        public bool WinGameCondition()
        {
            return _breachManager.GetCurrentBreach(_jsonFacade.GameBreaches).BreachType == BreachType.Boss && _jsonFacade.Database.CurrentInitialBet >= _baseData.BetsToWinGame;
        }
        #endregion

        public PokerHandDto UpgradePokerHand(PokerHandDto pokerHand)
        {
            _myRules.LoadRules(_jsonFacade.CurrentRules);
            _myRules.ChangePokerHandDto(pokerHand);
            _jsonFacade.CurrentRules = _myRules.GetRules();
            return pokerHand;
        }

        public PokerHandDto UpgradePokerHand(string pokerHandName, int chips, int mult)
        {
            _myRules.LoadRules(_jsonFacade.CurrentRules);
            var upgradedPokerHand = _myRules.UpgradePokerHand(pokerHandName, chips, mult);
            _jsonFacade.CurrentRules = _myRules.GetRules();
            return upgradedPokerHand;
        }

        public KeyValuePair<List<ItemDto>, int> OpenBoosterPack(ItemDto boosterPack)
        {
            var rarity = Enum.Parse<BoosterPackRarity>(boosterPack.Rarity);
            return _boosterPacksManager.OpenBoosterPack(boosterPack.ItemName, rarity);
        }

        public InventoryDto AddItemToInventory(ItemDto item)
        {
            var inventory = _jsonFacade.CurrentInventory.AddItem(item);
            _jsonFacade.CurrentInventory = inventory;
            return inventory;
        }

        public InventoryDto AddItemsToInventory(List<ItemDto> items)
        {
            return items.Aggregate(_jsonFacade.CurrentInventory, (current, itemDto) => current.AddItem(itemDto));
        }
        
        public List<CardDto> ApplyConsumable(string consumableItemName, List<CardDto> selectedCards) => _consumableManager.ApplyConsumable(consumableItemName, selectedCards);

        public InstructionDto ApplyVoucher(string voucherID) => _voucherManager.ApplyVoucher(voucherID);

        public void ApplyBossEffect(string bossName) => _bossManager.SetBoss(bossName);

        public JokerUseResultDto UseJokers(List<CardDto> cards, List<ItemDto> jokersToApply) => _jokerManager.UseJokers(cards, jokersToApply);
        public JokerUseResultDto UseModifiers(List<CardDto> cards) => _modifierManager.UseModifiers(cards);

        public InventoryDto GetInventory() => _jsonFacade.CurrentInventory;

        public ShopRefreshDto BuyItem(ItemDto item)
        {
            var savedShop = _jsonFacade.CurrentShop;
            var inventory = _jsonFacade.CurrentInventory;

            if(item.Cost > inventory.PlayerTreasure || !inventory.HasRoom(item))
            {
                return new ShopRefreshDto(savedShop, 0, false);
            }
            var itemCost = item.Cost;
            item.ChangeCost(itemCost / 2);
            var refreshedShop = _shopManager.RemoveShopDto(savedShop, item, _jsonFacade);
            if (item.Type == ItemType.Joker)
                _jokerManager.GetJoker(item);
            _jsonFacade.CurrentShop = refreshedShop;
            AddPlayerTreasure(-itemCost);
            var database = _jsonFacade.Database;
            database.CardsPurchased += 1;
            _jsonFacade.Database = database;
            return new ShopRefreshDto(refreshedShop, itemCost, true);
        }

        public InventoryDto SellItem(ItemDto item)
        {
            var inventory = _jsonFacade.CurrentInventory;
            inventory = inventory.RemoveItem(item).AddTreasure(item.Cost);
            
            _jsonFacade.CurrentInventory = inventory;
            

            return inventory;
        }

        private void AddPlayerTreasure(int addValue)
        {
            var inventory = _jsonFacade.CurrentInventory;
            inventory = inventory.AddTreasure(addValue);
            _jsonFacade.CurrentInventory = inventory;
        }

        public ShopDto GetShop()
        {
            _jsonFacade.CurrentShop = _shopManager.GetRandomShopItems(_jokerManager);
            return _jsonFacade.CurrentShop;
        }

        public ShopRefreshDto RerollShop()
        {
            var savedShop = _jsonFacade.CurrentShop;
            var newShop = _shopManager.GetRandomShopItems(_jokerManager);
            var inventory = _jsonFacade.CurrentInventory;

            if (savedShop.RerollCost > inventory.PlayerTreasure)
            {
                return new ShopRefreshDto(savedShop, 0, false);
            }

            AddPlayerTreasure(-savedShop.RerollCost);
            var changedShop = new ShopDto(newShop.GenericItems, savedShop.Vouchers, savedShop.Packagings, savedShop.RerollCost + 1);
            _jsonFacade.CurrentShop = changedShop;
            var database = _jsonFacade.Database;
            database.Renewals += 1;
            _jsonFacade.Database = database;
            return new ShopRefreshDto(changedShop, savedShop.RerollCost, true);
        }

        #region Aux Methods

        private void FillDecks()
        {
            var decks = _jsonFacade.GameDecks;
            var normalCards = _jsonFacade.GameNormalCards;
            //Test Only
            //EnhancementDto testEnhancement = new(10, 1, 3);
            //EditionDto testEdition = new(20, 4, 0);

            foreach (var currentDeck in decks)
            {
                int id = 0;
                foreach (var currentCard in normalCards)
                {
                    var card = currentCard.SetID(id.ToString());
                    currentDeck.Cards.Add(card);
                    id += 1;
                }

                //currentDeck.Cards[3].SetEnhancement(testEnhancement);
                //currentDeck.Cards[6].SetEnhancement(testEnhancement);
                //currentDeck.Cards[10].SetEnhancement(testEnhancement);
                //currentDeck.Cards[20].SetEnhancement(testEnhancement);
                //currentDeck.Cards[44].SetEnhancement(testEnhancement);

                //currentDeck.Cards[1].SetEdition(testEdition);
                //currentDeck.Cards[40].SetEdition(testEdition);
                //currentDeck.Cards[27].SetEdition(testEdition);
                //currentDeck.Cards[18].SetEdition(testEdition);
                //currentDeck.Cards[35].SetEdition(testEdition);

                //currentDeck.Cards[7].SetSeal(Seal.Blue);
                //currentDeck.Cards[17].SetSeal(Seal.Blue);
                //currentDeck.Cards[21].SetSeal(Seal.Blue);
                //currentDeck.Cards[42].SetSeal(Seal.Red);
                //currentDeck.Cards[36].SetSeal(Seal.Red);
                //currentDeck.Cards[31].SetSeal(Seal.Red);
                //currentDeck.Cards[11].SetSeal(Seal.Yellow);
                //currentDeck.Cards[12].SetSeal(Seal.Yellow);
                //currentDeck.Cards[38].SetSeal(Seal.Purple);
            }

            _loadedDecks = decks;
        }

        private bool CheckWin() => _jsonFacade.RoundData.Score.ScoreValue >= _breachManager.GetCurrentBreach(_jsonFacade.GameBreaches).ObjectiveScore;

        private List<CardDto> GetNewCards(int amount)
        {
            var currentDatabase = _jsonFacade.Database;
            var playerDeck = _jsonFacade.PlayerCurrentDeck;
            var playerHand = _jsonFacade.PlayerCurrentHand;

            currentDatabase.CardsInHand += amount < playerDeck.Cards.Count ? amount : playerDeck.Cards.Count; ;
            _jsonFacade.Database = currentDatabase;

            List<CardDto> newCards = playerDeck.Cards.Take(amount).ToList();
            playerDeck.Cards.RemoveRange(0, amount);
            playerHand.AddRange(newCards);

            _jsonFacade.PlayerCurrentDeck = playerDeck;
            _jsonFacade.PlayerCurrentHand = playerHand;

            return newCards;
        }

        private void DiscardHandCards(List<CardDto> cards)
        {
            var currentDatabase = _jsonFacade.Database;
            currentDatabase.CardsInHand -= cards.Count;
            _jsonFacade.Database = currentDatabase;

            var playerHand = _jsonFacade.PlayerCurrentHand;
            var updatedUsedCards = _jsonFacade.UsedCards;
            updatedUsedCards.AddRange(cards);

            foreach (var currentCard in cards)
            {
                playerHand.Remove(currentCard);
                UseDeckCard(currentCard.ID);
            }

            _jsonFacade.UsedCards = updatedUsedCards;
            _jsonFacade.PlayerCurrentHand = playerHand;
        }

        private void ReturnUsedCardsToMainDeck()
        {
            var playerDeck = _jsonFacade.PlayerCurrentDeck;
            var usedCards = _jsonFacade.UsedCards;

            playerDeck.Cards.AddRange(usedCards);
            _jsonFacade.PlayerCurrentDeck = playerDeck;

            usedCards.Clear();
            _jsonFacade.UsedCards = usedCards;
        }

        private ScoreDto CalculateScore(PokerHandDto playedHand, List<CardDto> scoredCards)
        {
            var score = playedHand.Score.ChipsValue;

            for (int i = 0; i < scoredCards.Count; i++)
                score += scoredCards[i].Score;

            UnityEngine.Debug.Log(score);
            return new ScoreDto(0, score, playedHand.Score.MultiplierValue);
        }

        private void MixCurrentDeck()
        {
            _jsonFacade.PlayerCurrentDeck = new DeckDto(
                _jsonFacade.PlayerCurrentDeck.DeckName, _jsonFacade.PlayerCurrentDeck.DeckDescription,
                _jsonFacade.PlayerCurrentDeck.Cards.OrderBy(_ => new Random().Next()).ToList());
        }

        private DeckDto GetDeckWithAllCards()
        {
            var handCards = _jsonFacade.PlayerCurrentHand;
            var deckCards = _jsonFacade.PlayerCurrentDeck.Cards;
            var discardedCards = _jsonFacade.UsedCards;

            return new DeckDto(_jsonFacade.PlayerCurrentDeck.DeckName, _jsonFacade.PlayerCurrentDeck.DeckDescription, handCards.Concat(deckCards).Concat(discardedCards).ToList());
        }
        #endregion


    }
}