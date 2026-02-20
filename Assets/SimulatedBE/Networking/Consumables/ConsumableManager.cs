using System.Collections.Generic;
using SimulatedBE.Networking.DTOs;
using SimulatedBE.Networking.Modifiers;
using System.Linq;

namespace SimulatedBE.Networking.Consumable
{
    public class ConsumableManager
    {
        Dictionary<string, IConsumable> _allConsumables;
        IJsonFacade _jsonFacade;

        public ConsumableManager(IJsonFacade jsonFacade, ModifierManager modifierManager, GameRules rules)
        {
            _jsonFacade = jsonFacade;
           _allConsumables = ConsumableFactory.CreateConsumables(_jsonFacade, modifierManager, rules);
        }

        public List<CardDto> ApplyConsumable(string consumableName, List<CardDto> selectedCards)
        {
            if (!_allConsumables.TryGetValue(consumableName, out IConsumable consumable))
            {
                UnityEngine.Debug.LogWarning("The Consumable ID doesn't exist");
                return selectedCards;

            }

            if (selectedCards == null) selectedCards = new List<CardDto>();

            RemoveConsumableItem(consumableName);

            return ChangeCards(consumable.ApplyConsumable(selectedCards));
        }

        private void RemoveConsumableItem(string consumableName)
        {
            var inventory = _jsonFacade.CurrentInventory;
            
            var consumable = inventory.Consumables.FirstOrDefault(item => item.ItemName == consumableName);
            if (consumable == null)
                return;
            
            inventory.Consumables.Remove(consumable);
                
            _jsonFacade.CurrentInventory = inventory;
        }

        private List<CardDto> ChangeCards(List<CardDto> modifiedCards)
        {
            var handCards = _jsonFacade.PlayerCurrentHand;

            handCards = handCards.Select(x => modifiedCards.Contains(x) ? SearchByID(modifiedCards, x.ID) : x).ToList();

            modifiedCards = modifiedCards.Where(x => !handCards.Contains(x)).ToList();

            if(modifiedCards.Count > 0)
            {
                var deck = _jsonFacade.PlayerCurrentDeck;
                var newDeck = new DeckDto(deck.DeckName, deck.DeckDescription, deck.Cards.Concat(modifiedCards).ToList());
                _jsonFacade.PlayerCurrentDeck = newDeck;
            }

            _jsonFacade.PlayerCurrentHand = handCards;

            return handCards;
        }

        private CardDto SearchByID(List<CardDto> cards, string id) => cards.Where(x => x.ID == id).FirstOrDefault();
    }
}