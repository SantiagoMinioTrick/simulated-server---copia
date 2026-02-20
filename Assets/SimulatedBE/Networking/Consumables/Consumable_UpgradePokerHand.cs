using SimulatedBE.Networking.DTOs;
using System.Collections.Generic;

namespace SimulatedBE.Networking.Consumable
{
    public class Consumable_UpgradePokerHand : IConsumable
    {
        private string _pokerHandName;
        private int _chipsToAdd;
        private int _multToAdd;
        private GameRules _rules;

        public Consumable_UpgradePokerHand(string pokerHandName, int chipsToAdd, int multToAdd, GameRules rules)
        {
            _pokerHandName = pokerHandName;
            _chipsToAdd = chipsToAdd;
            _multToAdd = multToAdd;
            _rules = rules;
        }

        public List<CardDto> ApplyConsumable(List<CardDto> selectedCards)
        {
            _rules.UpgradePokerHand(_pokerHandName, _chipsToAdd, _multToAdd);
            return selectedCards;
        }
    }
}
