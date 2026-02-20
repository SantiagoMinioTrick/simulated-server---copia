using System.Collections.Generic;
using SimulatedBE.Networking.DTOs;

namespace SimulatedBE.Networking.BoosterPacks
{
    public class BoosterPack_Sugar : BoosterPack
    {
        private readonly IPool _pool;

        public BoosterPack_Sugar(IPool pool) : base(pool)
        {  
            _pool = pool;
        }
        
        public override KeyValuePair<List<ItemDto>, int> Open(BoosterPackRarity rarity)
        {
            var cards = new List<ItemDto>();
            for (int i = 0; i < CardCounts[rarity].Key; i++)
            {
                cards.Add(_pool.GetRandomItem(new List<ShopItemDto>()).Item);
            }

            return new KeyValuePair<List<ItemDto>, int>(cards, CardCounts[rarity].Value);
        }   
    }
}