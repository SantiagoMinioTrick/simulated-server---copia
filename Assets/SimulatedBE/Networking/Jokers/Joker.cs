using SimulatedBE.Networking.DTOs;
using System.Collections.Generic;

namespace SimulatedBE.Networking.Jokers
{
    public abstract class Joker
    {
        protected Dictionary<JokerRarity, float> _rarityValues = new Dictionary<JokerRarity, float>();
        public bool CardDependency { get; private set; }

        public Joker(bool cardDependency)
        {
            CardDependency = cardDependency;
        }

        public abstract bool CanUseJoker(CardDto card);

        public Joker SetRarity(JokerRarity jokerRarity, float value) { _rarityValues.Add(jokerRarity, value); return this; }

        public virtual InstructionDto UseJoker(CardDto cardToApply, JokerRarity rarity, InstructionDto acumulativeValues)
        {
            return new InstructionDto();
        }

        public virtual InstructionDto UseJoker(List<CardDto> cardsList,JokerRarity rarity, InstructionDto acumulativeValues)
        {
            return new InstructionDto();
        }

        public virtual void OnGetJoker(JokerRarity rarity)
        {

        }

        public virtual void OnDestroyJoker(JokerRarity rarity)
        {

        }

        public virtual string GetValueString(JokerRarity rarity)
        {
            return ((int)_rarityValues[rarity]).ToString();
        }
    }
}