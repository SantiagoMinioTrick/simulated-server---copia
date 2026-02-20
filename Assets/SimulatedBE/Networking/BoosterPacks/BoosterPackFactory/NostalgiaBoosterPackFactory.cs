namespace SimulatedBE.Networking.BoosterPacks
{
    public class NostalgiaBoosterPackFactory : IBoosterPackFactory
    {
        public BoosterPack CreateBoosterPack()
        {
            return new BoosterPack_Nostalgia(new Shop_Pool(ShopManager.FarmCardsPoolName, JSONFilesNames.Preset_Shop_FarmCards))
                .SetRarity(BoosterPackRarity.Uncommon, 2, 1)
                .SetRarity(BoosterPackRarity.Rare, 4, 1)
                .SetRarity(BoosterPackRarity.Epic, 4, 2);
        }
    }
}