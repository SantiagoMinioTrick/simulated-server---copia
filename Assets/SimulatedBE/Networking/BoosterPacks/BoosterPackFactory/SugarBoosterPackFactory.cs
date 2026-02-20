namespace SimulatedBE.Networking.BoosterPacks
{
    public class SugarBoosterPackFactory : IBoosterPackFactory
    {
        public BoosterPack CreateBoosterPack()
        {
            return new BoosterPack_Sugar(new Shop_Pool(ShopManager.SugarTownPoolName, JSONFilesNames.Preset_Shop_Planets))
                .SetRarity(BoosterPackRarity.Uncommon, 2,1)
                .SetRarity(BoosterPackRarity.Rare, 4,1)
                .SetRarity(BoosterPackRarity.Epic, 4,2);
        }
    }
}