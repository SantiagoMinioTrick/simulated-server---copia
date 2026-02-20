using SimulatedBE.Networking.DTOs;
using System.Collections.Generic;

namespace SimulatedBE.Networking.Consumable
{
    public class Consumable_ConvertSymbol : IConsumable
    {
        Symbols symbolToConvert;

        public Consumable_ConvertSymbol(Symbols symbolToConvert)
        {
            this.symbolToConvert = symbolToConvert;
        }

        public List<CardDto> ApplyConsumable(List<CardDto> selectedCards)
        {
            var result = new List<CardDto>();

            for (int i = 0; i < selectedCards.Count; i++)
            {
                var newCard = new CardDto(selectedCards[i].ID, symbolToConvert, selectedCards[i].Value, selectedCards[i].Score, selectedCards[i].IsFaceDown);
                newCard.SetEdition(selectedCards[i].Edition);
                newCard.SetEnhancement(selectedCards[i].Enhancement);
                newCard.SetSeal(selectedCards[i].Seal);
                result.Add(newCard);
            }

            return result;
        }
    }
}