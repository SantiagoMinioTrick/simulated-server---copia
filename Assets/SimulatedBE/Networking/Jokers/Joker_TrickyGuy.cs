using System.Collections.Generic;
using System.Linq;
using SimulatedBE.Networking.DTOs;

namespace SimulatedBE.Networking.Jokers
{
    public class Joker_TrickyGuy : Joker
    { 
        const long AddedScore = 200;
        
        public Joker_TrickyGuy(bool cardDependency) : base(cardDependency)
        {
        }

        public override bool CanUseJoker(CardDto card)
        {
            return true;
        }

        public override InstructionDto UseJoker(List<CardDto> cardsList, JokerRarity rarity, InstructionDto acumulativeValues)
        {
            var isDiamond = cardsList.Any(card => card.Symbol == Symbols.Diamonds);
            var isHearth = cardsList.Any(card => card.Symbol == Symbols.Hearts);
            var isClub = cardsList.Any(card => card.Symbol == Symbols.Clubs);
            var isSpade = cardsList.Any(card => card.Symbol == Symbols.Spades);
            
            if (isDiamond && isHearth || isClub && isSpade)
            {
                return new InstructionDto("", null, AddedScore, 0, acumulativeValues.UpdatedTreasure, acumulativeValues.UpdatedInventory,
                    acumulativeValues.UpdatedRound);
            }
            return base.UseJoker(cardsList, rarity, acumulativeValues);
        }
    }
}