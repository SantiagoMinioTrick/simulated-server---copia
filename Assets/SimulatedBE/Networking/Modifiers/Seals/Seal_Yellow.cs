using System.Collections.Generic;
using SimulatedBE.Networking.DTOs;

namespace SimulatedBE.Networking.Modifiers.Seals
{
    public class Seal_Yellow : Seal
    {
        private readonly Shop_Pool _pool;

        public Seal_Yellow(string name, string description, IJsonFacade jsonFacade, string poolName, string path) : base(name, description)
        {
            _pool = new Shop_Pool(poolName, path);
        }

        public override bool CanUseModifier(CardDto cardOwner, InstructionDto mainInstruction)
        {
            return mainInstruction.UpdatedInventory.Consumables.Count < 2;
        }

        public override InstructionDto UseModifier(CardDto cardOwner, InstructionDto mainInstruction)
        {
            var newItem = _pool.GetRandomItem(new List<ShopItemDto>()).Item;
            var inventory = mainInstruction.UpdatedInventory;

            if (inventory.Consumables.Count < 2)
                inventory.Consumables.Add(newItem);

            var instruction = new InstructionDto(cardOwner.ID, null, 0, 0, 0, inventory, mainInstruction.UpdatedRound);
            return instruction;
        }
    }
}
