using System.Collections.Generic;

namespace SimulatedBE.Networking.DTOs
{
    public struct InventoryDto
    {
        public List<ItemDto> Jokers { get; private set; }
        public List<ItemDto> Consumables { get; private set; }
        public List<ItemDto> Vouchers { get; private set; }
        public List<TagDto> SavedTags { get; private set; }
        public int PlayerTreasure { get; private set; }
        public int JokerSlotAmmount { get; private set; }
        public int ConsumableSlotAmmount { get; private set; }

        public InventoryDto(List<ItemDto> jokers, List<ItemDto> consumables, List<ItemDto> vouchers, List<TagDto> savedTags, int playerTreasure, int jokerSlotAmmount, int consumableSlotAmmount)
        {
            Jokers = jokers;
            Consumables = consumables;
            Vouchers = vouchers;
            SavedTags = savedTags;
            PlayerTreasure = playerTreasure;
            JokerSlotAmmount = jokerSlotAmmount;
            ConsumableSlotAmmount = consumableSlotAmmount;
        }

        public InventoryDto AddItem(ItemDto item)
        {
            switch (item.Type)
            {
                case ItemType.Joker:
                    Jokers.Add(item);
                    break;
                case ItemType.Consumable:
                    Consumables.Add(item);
                    break;
                case ItemType.Voucher:
                    Vouchers.Add(item);
                    break;
            }
            return this;
        }

        public InventoryDto AddTreasure(int playerTreasure)
        {
            PlayerTreasure += playerTreasure;
            return this;
        }

        public bool HasRoom(ItemDto item)
        {
            if (item.Type == ItemType.Joker)
                return Jokers.Count < JokerSlotAmmount;
            else if (item.Type == ItemType.Consumable)
                return Consumables.Count < ConsumableSlotAmmount;
            else
                return true;
        }

        public InventoryDto RemoveItem(ItemDto itemToRemove)
        {
            if(itemToRemove.Type == ItemType.Joker)
            {
                for(int i = 0; i < Jokers.Count; i++)
                {
                    if (Jokers[i].ID != itemToRemove.ID) continue;

                    Jokers.RemoveAt(i);
                    break;
                }
            }
            else
            {
                for (int i = 0; i < Consumables.Count; i++)
                {
                    if (Consumables[i].ID != itemToRemove.ID) continue;

                    Consumables.RemoveAt(i);
                    break;
                }
            }

            return this;

        }

        public InventoryDto RemoveTag(TagDto tag)
        {
            for (int i = 0; i < SavedTags.Count; i++)
            {
                if (SavedTags[i].ID == tag.ID)
                {
                    SavedTags.RemoveAt(i);
                    break;
                }
            }

            return this;
        }

    }
}
