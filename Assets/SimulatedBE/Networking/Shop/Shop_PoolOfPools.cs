using SimulatedBE.Networking.DTOs;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SimulatedBE.Networking
{
    public class Shop_PoolOfPools : IPool
    {
        public Shop_PoolOfPools(string poolName, params ShopPoolDto[] _pools)
        {
            PoolName = poolName;

            pools = _pools.OrderByDescending(x => x.AppearChance).ToList();
        }

        public string PoolName { get; set; }

        List<ShopPoolDto> pools = new List<ShopPoolDto>();

        public ShopItemDto GetRandomItem(List<ShopItemDto> filterList)
        {
            ShopPoolDto result = new ShopPoolDto();

            float totalChance = pools.Select(x => x.AppearChance).Sum();
            float random = Random.Range(0f, totalChance);

            for (int i = 0; i < pools.Count; i++)
            {
                random -= pools[i].AppearChance;

                if (random <= 0)
                {
                    result = pools[i];
                    break;
                }
            }

            return result.Pool.GetRandomItem(filterList);
        }
    }

    public struct ShopPoolDto
    {
        public ShopPoolDto(IPool pool, float appearChance)
        {
            Pool = pool;
            AppearChance = appearChance;
        }

        public IPool Pool { get; private set; }
        public float AppearChance { get; private set; }
    }
}