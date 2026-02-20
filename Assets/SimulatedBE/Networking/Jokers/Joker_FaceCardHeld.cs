using SimulatedBE.Networking.DTOs;
using System.Collections.Generic;
using System.Linq;

namespace SimulatedBE.Networking.Jokers
{
    public class Joker_FaceCardHeld : Joker
    {
        private CardValue[] _cardValues;
        private IJsonFacade _jsonFacade;

        public Joker_FaceCardHeld(bool cardDependency, IJsonFacade jsonFacade, params CardValue[] cardValues) : base(cardDependency)
        {
            _cardValues = cardValues;
            _jsonFacade = jsonFacade;
        }

        public override bool CanUseJoker(CardDto card)
        {
            return CheckIfContainsValue();
        }

        public override InstructionDto UseJoker(List<CardDto> cardsList, JokerRarity rarity, InstructionDto acumulativeValues)
        {
            return new InstructionDto("", null, 0, (long)(GetValueAmmount() * _rarityValues[rarity]), 0, acumulativeValues.UpdatedInventory, acumulativeValues.UpdatedRound);
        }

        bool CheckIfContainsValue()
        {
            var hand = _jsonFacade.PlayerCurrentHand;

            for (int i = 0; i < hand.Count; i++)
            {
                if (_cardValues.Contains(hand[i].Value))
                    return true;
            }

            return false;
        }

        int GetValueAmmount()
        {
            var hand = _jsonFacade.PlayerCurrentHand;
            int count = 0;

            for (int i = 0; i < hand.Count; i++)
            {
                if (_cardValues.Contains(hand[i].Value))
                    count += 1;
            }

            return count; 
        }
    }
}