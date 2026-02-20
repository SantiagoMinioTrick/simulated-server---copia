using System.Collections.Generic;

namespace SimulatedBE.Networking.BoosterPacks
{
    public static class BoosterPackFactoryProvider
    {
        private static readonly Dictionary<string, IBoosterPackFactory> _factories = new()
        {
            { BoosterPackNames.BoosterPack_Name_Nostalgia, new NostalgiaBoosterPackFactory() },
            { BoosterPackNames.BoosterPack_Name_Sugar, new SugarBoosterPackFactory() },
            { BoosterPackNames.BoosterPack_Name_Rebel, new RebelBoosterPackFactory() }
        };

        public static BoosterPack CreateBoosterPack(string boosterPackName)
        {
            return _factories.TryGetValue(boosterPackName, out var factory) ? factory.CreateBoosterPack() : null;
        }
    }
}