using SimulatedBE.Networking.DTOs;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace SimulatedBE.Networking
{
    public class Shop_Pool : IPool
    {
        public Shop_Pool(string poolName, string poolPath)
        {
            PoolName = poolName;
            poolItems = JsonSerializer.ReadJsonFile<List<ShopItemDto>>(poolPath, Paths.Items);

            poolItems = poolItems.OrderByDescending(x => x.AppearChance).ToList();
        }

        public string PoolName { get; set; }

        List<ShopItemDto> poolItems = new List<ShopItemDto>();

        public ShopItemDto GetRandomItem(List<ShopItemDto> filterList)
        {
            ShopItemDto result = new ShopItemDto();

            var filtered = poolItems.Where(x => !filterList.Contains(x)).ToList();

            float totalChance = filtered.Select(x => x.AppearChance).Sum();
            float random = Random.Range(0f, totalChance);
            for (int i = 0; i < filtered.Count; i++)
            {
                random -= filtered[i].AppearChance;

                if(random <= 0)
                {
                    result = filtered[i];
                    break;
                }
            }

            return result;
        }
    }
}
