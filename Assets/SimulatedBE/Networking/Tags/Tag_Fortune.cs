using System.Collections.Generic;

namespace SimulatedBE.Networking.Tags
{
    public class Tag_Fortune : ITag
    {
        private IJsonFacade _jsonFacade;
        private Shop_Pool _pool;

        public Tag_Fortune(IJsonFacade jsonFacade, string poolName, string poolPath)
        {
            _jsonFacade = jsonFacade;
            _pool = new Shop_Pool(poolName, poolPath);

        }

        public void ApplyTag(TagManager tagManager)
        {
            var newItem = _pool.GetRandomItem(new List<SimulatedBE.Networking.DTOs.ShopItemDto>()).Item;
            var inventory = _jsonFacade.CurrentInventory;

            if (inventory.Consumables.Count < 2)
                inventory.Consumables.Add(newItem);

            _jsonFacade.CurrentInventory = inventory;
        }

        public string GetDynamicDescription()
        {
            return "";
        }
    }
}