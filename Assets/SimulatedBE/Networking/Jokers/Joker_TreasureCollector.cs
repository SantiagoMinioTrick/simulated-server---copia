using SimulatedBE.Networking.DTOs;

namespace SimulatedBE.Networking.Jokers
{
    public class Joker_TreasureCollector : Joker
    {
        private readonly IJsonFacade _facade;

        public Joker_TreasureCollector(bool cardDependency, IJsonFacade facade) : base(cardDependency)
        {
            _facade = facade;
        }

        public override InstructionDto UseJoker(CardDto cardToApply, JokerRarity rarity, InstructionDto acumulativeValues)
        {
            return new InstructionDto("", null, 0, 0, (int)_facade.RoundData.Score.ScoreValue % 10,
                acumulativeValues.UpdatedInventory, acumulativeValues.UpdatedRound, false);
        }

        public override bool CanUseJoker(CardDto card) => true;
    }
}