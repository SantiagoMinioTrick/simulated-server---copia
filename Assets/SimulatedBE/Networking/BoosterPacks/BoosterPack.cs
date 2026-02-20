using System.Collections.Generic;
using SimulatedBE.Networking.DTOs;

namespace SimulatedBE.Networking.BoosterPacks
{
    public abstract class BoosterPack
    {
        protected readonly Dictionary<BoosterPackRarity, KeyValuePair<int,int>> CardCounts;
        protected IPool Pool;
        
        protected BoosterPack(IPool pool)
        {
            Pool = pool;
            CardCounts = new Dictionary<BoosterPackRarity, KeyValuePair<int,int>>();
        }

        public BoosterPack SetRarity(BoosterPackRarity rarity, int cardOptions, int cardsToSelect)
        { 
            CardCounts.Add(rarity,  new KeyValuePair<int,int>(cardOptions, cardsToSelect));
            return this;
        }

        public virtual KeyValuePair<List<ItemDto>, int> Open(BoosterPackRarity rarity) => new();
    }
}