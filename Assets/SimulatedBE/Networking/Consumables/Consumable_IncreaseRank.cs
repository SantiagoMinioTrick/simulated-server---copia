using SimulatedBE.Networking.DTOs;
using System.Collections.Generic;

namespace SimulatedBE.Networking.Consumable
{
    public class Consumable_IncreaseRank : IConsumable
    {
        public List<CardDto> ApplyConsumable(List<CardDto> selectedCards)
        {
            var result = new List<CardDto>();

            for (int i = 0; i < selectedCards.Count; i++)
            {
                int currentValue = (int)selectedCards[i].Value;

                if (currentValue == 13 || currentValue == 1) currentValue = 1;
                else
                    currentValue += 1;
                var newCard = new CardDto(selectedCards[i].ID, selectedCards[i].Symbol, (CardValue)currentValue, selectedCards[i].Score, selectedCards[i].IsFaceDown);
                newCard.SetEdition(selectedCards[i].Edition);
                newCard.SetEnhancement(selectedCards[i].Enhancement);
                newCard.SetSeal(selectedCards[i].Seal);
                result.Add(newCard);
            }

            return result;
        }
    }
}
