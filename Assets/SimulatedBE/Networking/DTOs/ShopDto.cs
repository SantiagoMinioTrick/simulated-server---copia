using System.Collections.Generic;

namespace SimulatedBE.Networking.DTOs
{
    [System.Serializable]
    public struct ShopDto
    {
        public List<ItemDto> GenericItems {  get; private set; }
        public List<ItemDto> Vouchers { get; private set; }
        public List<ItemDto> Packagings { get; private set; }
        public int RerollCost { get; private set; }

        public ShopDto(List<ItemDto> genericItems, List<ItemDto> vouchers, List<ItemDto> packagings, int rerollCost)
        {
            GenericItems = genericItems;
            Vouchers = vouchers;
            Packagings = packagings;
            RerollCost = rerollCost;
        }
    }
}
