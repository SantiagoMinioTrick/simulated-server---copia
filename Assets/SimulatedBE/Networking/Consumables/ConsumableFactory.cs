using SimulatedBE.Networking.Modifiers;
using System.Collections.Generic;

namespace SimulatedBE.Networking.Consumable
{
    public static class ConsumableFactory
    {
        public static Dictionary<string, IConsumable> CreateConsumables(IJsonFacade jsonFacade, ModifierManager modifierManager, GameRules rules)
        {
            var allConsumables = new Dictionary<string, IConsumable>();
            allConsumables.Add(ConsumableNames.Consumable_Farm_ClonedSheep, new Consumable_CloneCard(jsonFacade));
            allConsumables.Add(ConsumableNames.Consumable_Farm_CloverHarvest, new Consumable_ConvertSymbol(Symbols.Clubs));
            allConsumables.Add(ConsumableNames.Consumable_Farm_CornHarvest, new Consumable_ConvertSymbol(Symbols.Diamonds));
            allConsumables.Add(ConsumableNames.Consumable_Farm_StrawberryHarvest, new Consumable_ConvertSymbol(Symbols.Hearts));
            allConsumables.Add(ConsumableNames.Consumable_Farm_WheatHarvest, new Consumable_ConvertSymbol(Symbols.Spades));
            allConsumables.Add(ConsumableNames.Consumable_Farm_Fertilizer, new Consumable_IncreaseRank());

            //Seals
            allConsumables.Add(ModifiersNames.Blue_Seal_Name, new Consumable_Modifier(ModifiersNames.Blue_Seal_Name, modifierManager));
            allConsumables.Add(ModifiersNames.Yellow_Seal_Name, new Consumable_Modifier(ModifiersNames.Yellow_Seal_Name, modifierManager));

            //Editions
            allConsumables.Add(ModifiersNames.Blue_Velvet_Editions_Name, new Consumable_Modifier(ModifiersNames.Blue_Velvet_Editions_Name, modifierManager));
            allConsumables.Add(ModifiersNames.Hope_Editions_Name, new Consumable_Modifier(ModifiersNames.Hope_Editions_Name, modifierManager));
            allConsumables.Add(ModifiersNames.Spider_Web_Editions_Name, new Consumable_Modifier(ModifiersNames.Spider_Web_Editions_Name, modifierManager));

            //Enhancements
            allConsumables.Add(ModifiersNames.Big_Bounus_Enhancement_Name, new Consumable_Modifier(ModifiersNames.Big_Bounus_Enhancement_Name, modifierManager));
            allConsumables.Add(ModifiersNames.Big_Mult_Enhancement_Name, new Consumable_Modifier(ModifiersNames.Big_Mult_Enhancement_Name, modifierManager));
            allConsumables.Add(ModifiersNames.Real_Wild_Enhancement_Name, new Consumable_Modifier(ModifiersNames.Real_Wild_Enhancement_Name, modifierManager));
            allConsumables.Add(ModifiersNames.Real_Gold_Enhancement_Name, new Consumable_Modifier(ModifiersNames.Real_Gold_Enhancement_Name, modifierManager));

            //Planetarian
            allConsumables.Add(ConsumableNames.Consumable_City_Traffic, new Consumable_UpgradePokerHand("Pair", 10, 2, rules));
            allConsumables.Add(ConsumableNames.Consumable_City_RushHour, new Consumable_UpgradePokerHand("Flush", 35, 4, rules));
            allConsumables.Add(ConsumableNames.Consumable_City_Highway, new Consumable_UpgradePokerHand("Straight", 30, 4, rules));

            return allConsumables;
        }
    }
}
