using System;

namespace SimulatedBE.Networking.DTOs
{
    [Serializable]
    public struct ShopItemDto
    {
        public string ID { get; private set; }
        public ItemDto Item { get; private set; }
        public float AppearChance { get; private set; }

        public ShopItemDto(string id, ItemDto item, float appearChance)
        {
            ID = id;
            Item = item;
            AppearChance = appearChance;
        }

        public override bool Equals(object obj)
        {
            try
            {
                return ((ShopItemDto)obj).ID == ID;
            }
            catch { return false; }
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ID, Item, AppearChance);
        }
    }
}
