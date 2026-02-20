using System.Collections.Generic;
using SimulatedBE.Networking.DTOs;

namespace SimulatedBE.Networking.Modifiers.Seals
{
    public class Seal_Blue : Seal
    {
        private Shop_Pool _pool;
        private IJsonFacade _jsonFacade;

        public Seal_Blue(string name, string description, IJsonFacade jsonFacade,string poolName, string path) : base(name, description)
        {
            _pool = new Shop_Pool(poolName, path);
            _jsonFacade = jsonFacade;
        }

        public override bool CanUseModifier(CardDto cardOwner, InstructionDto mainInstruction)
        {
            return _jsonFacade.Database.LastHandPlayed.Name == "Pair" && mainInstruction.UpdatedInventory.Consumables.Count < 2;
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