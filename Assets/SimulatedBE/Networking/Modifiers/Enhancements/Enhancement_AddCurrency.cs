using SimulatedBE.Networking.DTOs;

namespace SimulatedBE.Networking.Modifiers
{
    public class Enhancement_AddCurrency : Enhancements
    {
        private int _currencyToAdd;
        private IJsonFacade _facade;

        public Enhancement_AddCurrency(string name, string description, int currencyToAdd, IJsonFacade facade) : base(name, description)
        {
            _currencyToAdd = currencyToAdd;
            _facade = facade;
        }

        public override InstructionDto UseModifier(CardDto cardOwner, InstructionDto mainInstruction)
        {
            var instruction = new InstructionDto(cardOwner.ID, null, _currencyToAdd, _currencyToAdd, _currencyToAdd, mainInstruction.UpdatedInventory, mainInstruction.UpdatedRound);
            return instruction;
        }
    }
}