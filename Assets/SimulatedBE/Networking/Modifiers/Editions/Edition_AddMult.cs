using SimulatedBE.Networking.DTOs;

namespace SimulatedBE.Networking.Modifiers
{
    public class Edition_AddMult : Editions
    {
        private int _multToAdd;
        private bool _multiply;

        public Edition_AddMult(string name, string description, int multToAdd, bool multiply) : base(name, description)
        {
            _multToAdd = multToAdd;
            _multiply = multiply;
        }
        public override InstructionDto UseModifier(CardDto cardOwner, InstructionDto mainInstruction)
        {
            var instruction = new InstructionDto(cardOwner.ID, null, 0, _multToAdd, 0, mainInstruction.UpdatedInventory, mainInstruction.UpdatedRound, _multiply);
            return instruction;
        }
    }
}
