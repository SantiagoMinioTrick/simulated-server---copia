using SimulatedBE.Networking.DTOs;
using System.Collections.Generic;

namespace SimulatedBE.Networking.Jokers
{
    public class Joker_DiscardTreasure : Joker
    {
        private int _pointsToLose;
        public Joker_DiscardTreasure(bool cardDependency, int pointsToLose) : base(cardDependency)
        {
            _pointsToLose = pointsToLose;
        }

        public override bool CanUseJoker(CardDto card)
        {
            return true;
        }

        public override InstructionDto UseJoker(List<CardDto> cardsList, JokerRarity rarity, InstructionDto acumulativeValues)
        {
            return new InstructionDto(null, null, -(_pointsToLose * cardsList.Count), 0, (int)_rarityValues[rarity] * cardsList.Count, acumulativeValues.UpdatedInventory, acumulativeValues.UpdatedRound);
        }
    }
}