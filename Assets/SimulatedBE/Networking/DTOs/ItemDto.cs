using System;

namespace SimulatedBE.Networking.DTOs
{
    [Serializable]
    public class ItemDto
    {
        public string ID {  get; private set; }
        public string ItemName { get; private set; }
        public string ItemDescription { get; private set; }
        public int Cost { get; private set; }
        public ItemType Type { get; private set; }
        public string HandState { get; private set; }
        public string Rarity { get; private set; }

        public ItemDto(string id, string itemName, string itemDescription, int cost, ItemType type, string handState, string rarity)
        {
            ID = id;
            ItemName = itemName;
            ItemDescription = itemDescription;
            Cost = cost;
            Type = type;
            HandState = handState;
            Rarity = rarity;
        }

        public ItemDto SetRarity(string rarity)
        {
            Rarity = rarity;
            return this;
        }

        public ItemDto SetDescription(string desc)
        {
            ItemDescription = desc;
            return this;
        }

        public ItemDto CopyItem()
        {
            return new ItemDto("", ItemName, ItemDescription, Cost, Type, HandState, Rarity).SetID();
        }

        public ItemDto SetID()
        {
            ID = GetHashCode().ToString();
            return this;
        }

        public void ChangeCost(int cost)
        {
            Cost = cost;
        }
    }
}