using SimulatedBE.Networking.DTOs;

namespace SimulatedBE.Networking.Modifiers
{
    public class Enhancement_AddMult : Enhancements
    {
        private int _multToAdd;

        public Enhancement_AddMult(string name, string description, int multToAdd) : base(name, description)
        {
            _multToAdd = multToAdd;
        }

        public override InstructionDto UseModifier(CardDto cardOwner, InstructionDto mainInstruction)
        {
            var instruction = new InstructionDto(cardOwner.ID, null, 0, _multToAdd, 0, mainInstruction.UpdatedInventory, mainInstruction.UpdatedRound);
            return instruction;
        }
    }
}