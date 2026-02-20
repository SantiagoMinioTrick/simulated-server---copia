using System.Collections.Generic;
using SimulatedBE.Networking.DTOs;

namespace SimulatedBE.Networking
{
    public class JsonFacade : IJsonFacade
    {
        public List<CardDto> GameNormalCards =>
            JsonSerializer.ReadJsonFile<List<CardDto>>(JSONFilesNames.Preset_CardsList, Paths.Cards);

        public List<DeckDto> GameDecks => JsonSerializer.ReadJsonFiles<DeckDto>(Paths.Decks);

        public List<BreachDto> GameBreaches
        {
            get =>
                JsonSerializer.ReadJsonFile<List<BreachDto>>(JSONFilesNames.SaveData_CurrentBreaches, Paths.GameData);
            set => JsonSerializer.WriteJsonFile(value, JSONFilesNames.SaveData_CurrentBreaches, Paths.GameData);
        }

        public RoundDto RoundData
        {
            get => JsonSerializer.ReadJsonFile<RoundDto>(JSONFilesNames.SaveData_RoundData, Paths.GameData);
            set => JsonSerializer.WriteJsonFile(value, JSONFilesNames.SaveData_RoundData, Paths.GameData);
        }

        public List<CardDto> PlayerCurrentHand
        {
            get =>
                JsonSerializer.ReadJsonFile<List<CardDto>>(JSONFilesNames.SaveData_CurrentPlayerHand, Paths.PlayerData);
            set => JsonSerializer.WriteJsonFile(value, JSONFilesNames.SaveData_CurrentPlayerHand, Paths.PlayerData);
        }
        
        public List<CardDto> UsedCards
        {
            get => JsonSerializer.ReadJsonFile<List<CardDto>>(JSONFilesNames.SaveData_UsedCards, Paths.PlayerData);
            set => JsonSerializer.WriteJsonFile(value, JSONFilesNames.SaveData_UsedCards, Paths.PlayerData);
        }
        
        public DeckDto PlayerCurrentDeck
        {
            get => JsonSerializer.ReadJsonFile<DeckDto>(JSONFilesNames.SaveData_CurrentPlayerDeck, Paths.PlayerData);
            set => JsonSerializer.WriteJsonFile(value, JSONFilesNames.SaveData_CurrentPlayerDeck, Paths.PlayerData);
        }
        
        public List<Rule> CurrentRules
        {
            get => JsonSerializer.ReadJsonFile<List<Rule>>(JSONFilesNames.SaveData_PokerHand, Paths.GameData);
            set => JsonSerializer.WriteJsonFile(value, JSONFilesNames.SaveData_PokerHand, Paths.GameData);
        }
        
        public InventoryDto CurrentInventory
        {
            get => JsonSerializer.ReadJsonFile<InventoryDto>(JSONFilesNames.SaveData_Inventory, Paths.PlayerData);
            set => JsonSerializer.WriteJsonFile(value, JSONFilesNames.SaveData_Inventory, Paths.PlayerData);
        }
        
        public Database Database
        {
            get => JsonSerializer.ReadJsonFile<Database>(JSONFilesNames.SaveData_Database,
                Paths.GameData);
            set => JsonSerializer.WriteJsonFile(value, JSONFilesNames.SaveData_Database, Paths.GameData);
        }
        public ShopDto CurrentShop
        {
            get => JsonSerializer.ReadJsonFile<ShopDto>(JSONFilesNames.SaveData_CurrentShop, Paths.GameData);
            set => JsonSerializer.WriteJsonFile(value, JSONFilesNames.SaveData_CurrentShop, Paths.GameData);
        }
    }
}
