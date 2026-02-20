using SimulatedBE.Networking.DTOs;

namespace SimulatedBE.Networking.Jokers
{
    public class Joker_SymbolExtraScore : Joker
    {
        Symbols _symbolToAffect;
        public Joker_SymbolExtraScore(bool cardDependency, Symbols symbolToAffect) : base(cardDependency)
        {
            _symbolToAffect = symbolToAffect;
        }

        public override bool CanUseJoker(CardDto card)
        {
            return card.Symbol == _symbolToAffect;
        }

        public override InstructionDto UseJoker(CardDto cardToApply, JokerRarity rarity, InstructionDto acumulativeValues)
        {
            return new InstructionDto(cardToApply.ID, null, (long)_rarityValues[rarity], 0, 0, acumulativeValues.UpdatedInventory, acumulativeValues.UpdatedRound);
        }
    }
}
