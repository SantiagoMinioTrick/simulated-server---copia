using System.Collections.Generic;
using SimulatedBE.Networking.DTOs;

namespace SimulatedBE.Networking.BoosterPacks
{
    public class BoosterPacksManager
    { 
        public KeyValuePair<List<ItemDto>, int> OpenBoosterPack(string packId, BoosterPackRarity rarity)
        {
            BoosterPack boosterPack = BoosterPackFactoryProvider.CreateBoosterPack(packId);
            return boosterPack != null ? boosterPack.Open(rarity) : new KeyValuePair<List<ItemDto>, int>(new List<ItemDto>(), 0);
        }
        
    }
}
