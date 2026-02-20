using System;

namespace SimulatedBE.Networking.DTOs
{
    [Serializable]
    public struct CardDto
    {
        public string ID { get; private set; }

        public Symbols Symbol { get; private set; }
        public CardValue Value { get; private set; }
        public bool IsFaceDown { get; private set; }
        public bool UsedCard { get; private set; }
        public int Score { get; private set; }
        public ModifierDto Enhancement { get; private set; }
        public ModifierDto Edition { get; private set; }
        public ModifierDto Seal { get; private set; }

        public CardDto(string id, Symbols symbol, CardValue value, int score, bool isFaceDown = false)
        {
            ID = id;
            Symbol = symbol;
            Value = value;
            Score = score;
            IsFaceDown = isFaceDown;
            Enhancement = default;
            Edition = default;
            Seal = default;
            UsedCard = false;
        }

        public CardDto SetID(string id)
        {
            ID = id;
            return this;
        }
        public void UseCard() => UsedCard = true;
        public void ResetCardState() => UsedCard = false;
        public void SetEnhancement(ModifierDto enhancement)
        {
            Enhancement = enhancement;
        }

        public void SetEdition(ModifierDto edition)
        {
            Edition = edition;
        }

        public void SetSeal(ModifierDto seal)
        {
            Seal = seal;
        }

        public override bool Equals(object obj)
        {
            try
            {
                var card = (CardDto)obj;
                return card.ID == ID;
            }
            catch
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Symbol, Value, IsFaceDown, Score);
        }
    }     
}
