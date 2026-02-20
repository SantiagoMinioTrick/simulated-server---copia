using System.Collections.Generic;
using SimulatedBE.Networking;
using SimulatedBE.Networking.DTOs;

public interface IJsonFacade
{
    List<CardDto> GameNormalCards { get; }
    List<DeckDto> GameDecks { get; }
    List<BreachDto> GameBreaches { get; set; }
    RoundDto RoundData { get; set; }
    List<CardDto> PlayerCurrentHand { get; set; }
    List<CardDto> UsedCards { get; set; }
    DeckDto PlayerCurrentDeck { get; set; }
    List<Rule> CurrentRules { get; set; }
    InventoryDto CurrentInventory { get; set; }
    ShopDto CurrentShop { get; set; }
    Database Database { get; set; }
}