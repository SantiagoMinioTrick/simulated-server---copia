using System.Collections.Generic;
using SimulatedBE.Networking.DTOs;

namespace SimulatedBE.Networking.Jokers
{
    public class Joker_CashUnderTheCoach : Joker
    {
        public Joker_CashUnderTheCoach(bool cardDependency) : base(cardDependency)
        {
            
        }

        public override InstructionDto UseJoker(List<CardDto> cardsList, JokerRarity rarity, InstructionDto acumulativeValues)
        {
            return new InstructionDto(null, null,(int) _rarityValues[rarity] * cardsList.Count , 0, 0, acumulativeValues.UpdatedInventory, acumulativeValues.UpdatedRound);
        }

        public override bool CanUseJoker(CardDto card)
        {
            return true;
        }
    }
}