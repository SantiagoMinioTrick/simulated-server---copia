using SimulatedBE.Networking.DTOs;
using System.Collections.Generic;
using System.Linq;

namespace SimulatedBE.Networking.Jokers
{
    public class Joker_JackOfAllTrades : Joker
    {
        private IJsonFacade _jsonFacade;
        public Joker_JackOfAllTrades(bool cardDependency, IJsonFacade facade) : base(cardDependency)
        {
            _jsonFacade = facade;
        }

        public override bool CanUseJoker(CardDto card)
        {
            return CheckIfContainsAllSymbols();
        }

        public override InstructionDto UseJoker(List<CardDto> cardsList, JokerRarity rarity, InstructionDto acumulativeValues)
        {
            return new InstructionDto("", null, (long)(_rarityValues[rarity] * cardsList.Count), 0, 0, acumulativeValues.UpdatedInventory, acumulativeValues.UpdatedRound);
        }

        bool CheckIfContainsAllSymbols()
        {
            var hand = _jsonFacade.PlayerCurrentHand.OrderBy(x => x.Symbol).ToList();
            Symbols current = hand[0].Symbol;
            int symbolsCount = 1;

            for (int i = 0; i < hand.Count; i++)
            {
                if (current == hand[i].Symbol) continue;

                current = hand[i].Symbol;
                symbolsCount += 1;
                if (symbolsCount >= 4)
                    return true;
            }

            return false;
        }
    }
}