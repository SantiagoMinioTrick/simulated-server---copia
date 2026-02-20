using SimulatedBE.Networking.DTOs;
using UnityEngine;

namespace SimulatedBE.Networking.Jokers
{
    public class Joker_TreasureMultiplier : Joker
    {
        private readonly IJsonFacade _jsonFacade;

        public Joker_TreasureMultiplier(bool cardDependency, IJsonFacade jsonFacade) : base(cardDependency)
        {
            _jsonFacade = jsonFacade;
        }

        public override bool CanUseJoker(CardDto card) => true;

        public override InstructionDto UseJoker(CardDto cardToApply, JokerRarity rarity, InstructionDto acumulativeValues)
        {
            var multiplier = (_jsonFacade.CurrentInventory.PlayerTreasure % 10) * _rarityValues[rarity];
            var treasure = _jsonFacade.CurrentInventory.PlayerTreasure * multiplier;
            return new InstructionDto(cardToApply.ID, null, Mathf.RoundToInt(treasure),0, acumulativeValues.UpdatedTreasure, acumulativeValues.UpdatedInventory, acumulativeValues.UpdatedRound);
        }
    }
}