using SimulatedBE.Networking.DTOs;


namespace SimulatedBE.Networking.Jokers
{
    public class Joker_BadWithNumbers : Joker
    {
        private PokerRules _rules;
        public Joker_BadWithNumbers(bool cardDependency, PokerRules rules) : base(cardDependency)
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
            _rules.ChangeMaxCards((int)_rarityValues[rarity]);
        }

        public override void OnDestroyJoker(JokerRarity rarity)
        {
            base.OnDestroyJoker(rarity);
            _rules.ChangeMaxCards(5);
        }
    }
}