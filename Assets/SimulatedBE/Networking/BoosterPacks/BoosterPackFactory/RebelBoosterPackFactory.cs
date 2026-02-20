namespace SimulatedBE.Networking.BoosterPacks
{
    public class RebelBoosterPackFactory : IBoosterPackFactory
    {
        public BoosterPack CreateBoosterPack()
        {
            return new BoosterPack_Rebel(new Shop_Pool(ShopManager.JokerPoolName, JSONFilesNames.Preset_Shop_Jokers))
                .SetRarity(BoosterPackRarity.Uncommon, 2, 1)
                .SetRarity(BoosterPackRarity.Rare, 4, 1)
                .SetRarity(BoosterPackRarity.Epic, 4, 2);
        }
    }
}