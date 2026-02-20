using SimulatedBE.Networking.DTOs;

namespace SimulatedBE.Networking.Modifiers
{
    public class Enhancement_WildSuit : Enhancements
    {
        public Enhancement_WildSuit(string name, string description) : base(name, description)
        {
        }

        public override InstructionDto UseModifier(CardDto cardOwner, InstructionDto mainInstruction)
        {
            var instruction = new InstructionDto(cardOwner.ID, null, 0,0,0,mainInstruction.UpdatedInventory, mainInstruction.UpdatedRound);
            return instruction;
        }
    }
}