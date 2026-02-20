using System.Collections.Generic;
using SimulatedBE.Networking.DTOs;

namespace SimulatedBE.Networking.Jokers
{
    public class Joker_NegativeScore : Joker
    {
        private readonly IJsonFacade _facade;
        
        public Joker_NegativeScore(bool cardDependency, IJsonFacade facade) : base(cardDependency)
        {
            _facade = facade;
        }

        public override bool CanUseJoker(CardDto card)
        {
            return true;
        }

        public override InstructionDto UseJoker(List<CardDto> cardsList, JokerRarity rarity, InstructionDto acumulativeValues)
        {
            var updatedRoundScore = acumulativeValues.AddedScore * (_rarityValues[rarity] / 100);
            var updatedTreasure = acumulativeValues.UpdatedTreasure + 2;
            return new InstructionDto("", null, (long)-updatedRoundScore, 0, updatedTreasure, acumulativeValues.UpdatedInventory, acumulativeValues.UpdatedRound);
        }
    }
}