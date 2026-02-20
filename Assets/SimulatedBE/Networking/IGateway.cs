using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using SimulatedBE.Networking.DTOs;

namespace SimulatedBE.Networking
{
    public interface IGateway
    {
        UniTask<List<DeckDto>> GetAllDecks(string playerId);
        UniTask<List<BreachDto>> GetAllBreaches();
        UniTask<RoundDto> GetGameInfo();
        UniTask SaveSelectedDeck(DeckDto selectedDeck);
        UniTask<List<PokerHandDto>> GetPokerHands();
        UniTask<List<CardDto>> GetShuffledCards();
        UniTask<SelectBreachResultDto> SendSelectBreachMessage();
        UniTask<SkipBreachResultDto> SendSkipBreachMessage(BreachDto breach);
        UniTask<PokerHandDto> ValidatePokerHand(List<CardDto> cards);
        UniTask<DiscardCardResultDto> DiscardCard(List<CardDto> cardsToDiscard);
        UniTask<EndOfGameResultDto> EndOfGame();
        UniTask<bool> WinGameConditionAccomplished();
        UniTask<WinResultDto> GetWinInfo(BreachDto breach);
        UniTask<long> GetTreasure();
        UniTask<PlayHandResultDto> PlayHand(List<CardDto> cardsPlayed);
        UniTask<InventoryDto> GetInventory();
        UniTask<ShopRefreshDto> BuyItem(ItemDto item);
        UniTask<InventoryDto> SellItem(ItemDto item);
        UniTask<ShopDto> GetShop();
        UniTask<ShopRefreshDto> RerollShop();
        UniTask<PokerHandDto> UpgradePokerHand(PokerHandDto pokerHand);
        UniTask ApplyBossEffect(string bossName);
        UniTask<InstructionDto> ApplyVoucherEffect(string voucherName);
        UniTask<DeckDto> ApplySeal(CardDto card, Seal seal);
        UniTask<JokerUseResultDto> UseModifiers(List<CardDto> cards);
        UniTask<int> CollectTreasure();
        UniTask<List<CardDto>> ApplyConsumable(string consumableItemName, List<CardDto> selectedCards);
        UniTask<KeyValuePair<List<ItemDto>, int>> OpenBoosterPack(ItemDto boosterPack);
        UniTask<InventoryDto> SelectItem(ItemDto item);
        UniTask<JokerUseResultDto> UseJokers(List<CardDto> cards, List<ItemDto> jokersToApply);
        UniTask<CalculatedScoreDto> CalculateScore();
        UniTask ApplyTag(TagDto tagToApply);
    }
}