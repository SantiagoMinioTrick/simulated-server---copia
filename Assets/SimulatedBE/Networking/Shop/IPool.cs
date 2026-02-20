using System.Collections.Generic;
using SimulatedBE.Networking.DTOs;

namespace SimulatedBE.Networking
{
    public interface IPool
    {
        public string PoolName { get; set; }
        ShopItemDto GetRandomItem(List<ShopItemDto> filterList);
    }
}
