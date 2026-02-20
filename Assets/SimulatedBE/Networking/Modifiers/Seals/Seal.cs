using SimulatedBE.Networking.DTOs;

namespace SimulatedBE.Networking.Modifiers.Seals
{
    public abstract class Seal : IModifier
    {
        private readonly string _name;
        private readonly string _description;

        protected Seal(string name, string description)
        {
            _name = name;
            _description = description;
        }

        public CardDto ApplyModifier(CardDto cardToApply)
        {
            cardToApply.SetSeal(new ModifierDto(_name, _description));
            return cardToApply;
        }

        public virtual bool CanUseModifier(CardDto cardOwner, InstructionDto mainInstruction)
        {
            return true;
        }

        public abstract InstructionDto UseModifier(CardDto cardOwner, InstructionDto mainInstruction);
    }
}