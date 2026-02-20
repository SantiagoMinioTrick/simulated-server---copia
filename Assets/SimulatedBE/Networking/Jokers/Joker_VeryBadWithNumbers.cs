using SimulatedBE.Networking.DTOs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimulatedBE.Networking.Jokers
{
    public class Joker_VeryBadWithNumbers : Joker
    {
        private PokerRules _rules;
        public Joker_VeryBadWithNumbers(bool cardDependency, PokerRules rules) : base(cardDependency)
        {
            _rules = rules;
        }

        public override bool CanUseJoker(CardDto card)
        {
            return false;
        }

        public override void OnGetJoker(JokerRarity rarity)
        {
            base.OnGetJoker(rarity);
            _rules.ChangeStraightDiff((int)_rarityValues[rarity]);
        }

        public override void OnDestroyJoker(JokerRarity rarity)
        {
            base.OnDestroyJoker(rarity);
            _rules.ChangeStraightDiff(1);
        }
    }
}