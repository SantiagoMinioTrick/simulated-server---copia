using SimulatedBE.Networking.DTOs;
using System.Collections.Generic;

namespace SimulatedBE.Networking.Consumable
{
    public class Consumable_CloneCard : IConsumable
    {
        private IJsonFacade _jsonFacade;

        public Consumable_CloneCard(IJsonFacade jsonFacade)
        {
            _jsonFacade = jsonFacade;
        }

        public List<CardDto> ApplyConsumable(List<CardDto> selectedCards)
        {
            var result = new List<CardDto>();

            var database = _jsonFacade.Database;
            for (int i = 0; i < selectedCards.Count; i++)
            {
                result.Add(selectedCards[i]);
                var newCard = selectedCards[i];
                newCard = newCard.SetID(database.GetNewID().ToString());
                result.Add(newCard);
            }
            _jsonFacade.Database = database;

            return result;
        }
    }
}