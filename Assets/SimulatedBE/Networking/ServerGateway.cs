using System.Collections.Generic;
using SimulatedBE.Networking.DTOs;
using System;
using Cysharp.Threading.Tasks;

namespace SimulatedBE.Networking
{
    public class ServerGateway : IGateway
    {
        public static ServerGateway Instance { get; } = new ServerGateway();
        private readonly IJsonFacade _jsonFacade = new JsonFacade();

        private GameRules _gameRules = new GameRules();

        public async UniTask<List<DeckDto>> GetAllDecks(string playerId)
        {
            await _gameRules.Initialize();
            return await UniTask.FromResult(_gameRules.LoadedDecks);
        }

        public UniTask<List<CardDto>> GetUsedCardsDeck()
        {
            return UniTask.FromResult(_gameRules.GetUsedCardsDeck());
        }

        public UniTask<List<BreachDto>> GetAllBreaches()
        {
            return UniTask.FromResult(_gameRules.GetAllBreaches());
        }

        public UniTask<RoundDto> GetGameInfo()
        {
            return UniTask.FromResult(_jsonFacade.RoundData);
        }

        public UniTask<TagDto> GetTagReward()
        {
            return UniTask.FromResult(_gameRules.GetTagReward());
        }

        public UniTask<List<CardDto>> GetShuffledCards()
        {
            return UniTask.FromResult(_gameRules.GetShuffleCards());
        }

        public UniTask<SelectBreachResultDto> SendSelectBreachMessage()
        {
            return UniTask.FromResult(_gameRules.SendSelectBreachMessage());
        }

        public UniTask<SkipBreachResultDto> SendSkipBreachMessage(BreachDto breach)
        {
            return UniTask.FromResult(_gameRules.SkipBreach());
        }

        public UniTask SaveSelectedDeck(DeckDto selectedDeck)
        {
            _gameRules.SaveSelectedDeck(selectedDeck);
            return UniTask.CompletedTask;
        }

        public UniTask<List<PokerHandDto>> GetPokerHands()
        {
            return UniTask.FromResult(_gameRules.GetPokerHands());
        }

        public UniTask<PokerHandDto> ValidatePokerHand(List<CardDto> cards)
        {
            return UniTask.FromResult(_gameRules.ValidatePokerHand(cards));
        }

        public UniTask<DiscardCardResultDto> DiscardCard(List<CardDto> cardsToDiscard)
        {
            return UniTask.FromResult(_gameRules.DiscardCard(cardsToDiscard));
        }

        public UniTask<EndOfGameResultDto> EndOfGame()
        {
            return UniTask.FromResult(_gameRules.EndOfGame());
        }

        public UniTask<WinResultDto> GetWinInfo(BreachDto breach)
        {
            return UniTask.FromResult(_gameRules.GetWinResult(breach));
        }

        public UniTask<long> GetTreasure()
        {
            return UniTask.FromResult((long)_gameRules.GetInventory().PlayerTreasure);
        }

        public UniTask<PlayHandResultDto> PlayHand(List<CardDto> cardsPlayed)
        {
            return UniTask.FromResult(_gameRules.PlayHand(cardsPlayed));
        }

        public UniTask<PokerHandDto> UpgradePokerHand(PokerHandDto pokerHand)
        {
            return UniTask.FromResult(_gameRules.UpgradePokerHand(pokerHand));
        }

        public UniTask ApplyBossEffect(string bossName)
        {
            _gameRules.ApplyBossEffect(bossName);
            return UniTask.CompletedTask;
        }

        public UniTask<InventoryDto> GetInventory()
        {
            return UniTask.FromResult(_gameRules.GetInventory());
        }

        public UniTask<InventoryDto> AddItemsToInventory(List<ItemDto> itemsToAdd)
        {
            return UniTask.FromResult(_gameRules.AddItemsToInventory(itemsToAdd));
        }

        public UniTask<InventoryDto> SelectItem(ItemDto item)
        {
            return UniTask.FromResult(_gameRules.AddItemToInventory(item));
        }
        public UniTask<ShopRefreshDto> BuyItem(ItemDto item)
        {
            return UniTask.FromResult(_gameRules.BuyItem(item));
        }
        public UniTask<InventoryDto> SellItem(ItemDto item)
        {
            return UniTask.FromResult(_gameRules.SellItem(item));
        }

        public UniTask<ShopDto> GetShop()
        {
            return UniTask.FromResult(_gameRules.GetShop());
        }

        public UniTask<ShopRefreshDto> RerollShop()
        {
            return UniTask.FromResult(_gameRules.RerollShop());
        }

        public UniTask MultiplyJoker(ScoreDto score)
        {
            return UniTask.CompletedTask;
        }

        public UniTask SumJoker(ScoreDto score)
        {
            return UniTask.CompletedTask;
        }

        public UniTask<DeckDto> ApplySeal(CardDto card, Seal seal)
        {
            throw new NotImplementedException();
        }

        public UniTask<bool> WinGameConditionAccomplished()
        {
            return UniTask.FromResult(_gameRules.WinGameCondition());
        }

        public UniTask<int> CollectTreasure()
        {
            return UniTask.FromResult(_gameRules.CollectTreasure());
        }

        public UniTask<List<CardDto>> ApplyConsumable(string consumableItemName, List<CardDto> selectedCards)
        {
            return UniTask.FromResult(_gameRules.ApplyConsumable(consumableItemName, selectedCards));
        }

        public UniTask<InstructionDto> ApplyVoucherEffect(string voucherName)
        {
            return UniTask.FromResult(_gameRules.ApplyVoucher(voucherName));
        }

        public UniTask<KeyValuePair<List<ItemDto>, int>> OpenBoosterPack(ItemDto boosterPack)
        {
            return UniTask.FromResult(_gameRules.OpenBoosterPack(boosterPack));
        }

        public UniTask<JokerUseResultDto> UseJokers(List<CardDto> cards, List<ItemDto> jokersToApply)
        {
            return UniTask.FromResult(_gameRules.UseJokers(cards, jokersToApply));
        }

        public UniTask<CalculatedScoreDto> CalculateScore()
        {
            return UniTask.FromResult(_gameRules.CalculateScore());
        }

        public UniTask<JokerUseResultDto> UseModifiers(List<CardDto> cards)
        {
            return UniTask.FromResult(_gameRules.UseModifiers(cards));
        }

        public UniTask ApplyTag(TagDto tagToApply)
        {
            _gameRules.ApplyTag(tagToApply);
            return UniTask.CompletedTask;
        }
    }
}