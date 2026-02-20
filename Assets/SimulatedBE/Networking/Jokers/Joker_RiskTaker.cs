using System.Collections.Generic;
using System.Linq;
using SimulatedBE.Networking.DTOs;

namespace SimulatedBE.Networking.Jokers
{
    public class Joker_RiskTaker : Joker
    {
        public Joker_RiskTaker(bool cardDependency) : base(cardDependency)
        {
        }

        public override bool CanUseJoker(CardDto card)
        {
            return true;
        }

        public override InstructionDto UseJoker(List<CardDto> cardsList, JokerRarity rarity, InstructionDto acumulativeValues)
        {
            var isJokerApply = cardsList.Any(card => (int) card.Value < 5);
            if (isJokerApply)
            {
                return new InstructionDto("", null, 0, 2, -1,
                    acumulativeValues.UpdatedInventory, acumulativeValues.UpdatedRound, true);
            }
            return base.UseJoker(cardsList, rarity, acumulativeValues);
        }
    }
}