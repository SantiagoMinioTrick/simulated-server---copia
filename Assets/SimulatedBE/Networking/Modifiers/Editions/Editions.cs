using SimulatedBE.Networking.DTOs;

namespace SimulatedBE.Networking.Modifiers
{
    public abstract class Editions : IModifier
    {
        protected string _name;
        protected string _description;

        protected Editions(string name, string description)
        {
            _name = name;
            _description = description;
        }

        public CardDto ApplyModifier(CardDto cardToApply)
        {
            ModifierDto modifier = new ModifierDto(_name, _description);
            cardToApply.SetEdition(modifier);

            return cardToApply;
        }

        public virtual bool CanUseModifier(CardDto cardOwner, InstructionDto mainInstruction)
        {
            return true;
        }

        public abstract InstructionDto UseModifier(CardDto cardOwner, InstructionDto mainInstruction);
    }
}
