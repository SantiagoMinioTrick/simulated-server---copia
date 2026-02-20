using SimulatedBE.Networking.DTOs;
using System.Collections.Generic;

namespace SimulatedBE.Networking.Jokers
{
    public class Joker_Egg : Joker
    {
        public Joker_Egg(bool cardDependency) : base(cardDependency)
        {
        }

        public override bool CanUseJoker(CardDto card)
        {
            return true;
        }

        public override InstructionDto UseJoker(List<CardDto> cardsList, JokerRarity rarity, InstructionDto acumulativeValues)
        {
            return new InstructionDto(null, null, 0, 0, (int)_rarityValues[rarity] * cardsList.Count, acumulativeValues.UpdatedInventory, acumulativeValues.UpdatedRound);
        }
    }
}