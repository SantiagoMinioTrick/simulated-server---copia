using SimulatedBE.Networking.DTOs;

namespace SimulatedBE.Networking.Modifiers
{
    public class Enhancement_AddChips : Enhancements
    {
        private int _chipsToAdd;

        public Enhancement_AddChips(string name, string description, int chipsToAdd) : base(name, description)
        {
            _chipsToAdd = chipsToAdd;
        }

        public override InstructionDto UseModifier(CardDto cardOwner, InstructionDto mainInstruction)
        {
            var instruction = new InstructionDto(cardOwner.ID, null, _chipsToAdd, 0, 0, mainInstruction.UpdatedInventory, mainInstruction.UpdatedRound);
            return instruction;
        }
    }
}