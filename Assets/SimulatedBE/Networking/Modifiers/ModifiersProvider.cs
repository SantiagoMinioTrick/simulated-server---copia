using System.Collections.Generic;
using SimulatedBE.Networking.Modifiers.Seals;

namespace SimulatedBE.Networking.Modifiers
{
    public static class ModifiersProvider
    {
        public static Dictionary<string, IModifier> CreateModifiers(IJsonFacade jsonFacade)
        {
            var modifiers = new Dictionary<string, IModifier>();
              //Add Seals
            modifiers.Add(
                ModifiersNames.Blue_Seal_Name, 
                new Seal_Blue(ModifiersNames.Blue_Seal_Name,
                    ModifiersNames.Blue_Seal_Description,
                    jsonFacade,
                    ShopManager.SugarTownPoolName, 
                    JSONFilesNames.Preset_Shop_Planets
                ));
        
            modifiers.Add(
                ModifiersNames.Yellow_Seal_Name,
                new Seal_Yellow(ModifiersNames.Yellow_Seal_Name,
                    ModifiersNames.Yellow_Seal_Description, 
                    jsonFacade ,
                    ShopManager.FarmCardsPoolName,
                    JSONFilesNames.Preset_Shop_FarmCards
                ));
        
            //Add Editions
            modifiers.Add(ModifiersNames.Hope_Editions_Name, new Edition_AddMult(ModifiersNames.Hope_Editions_Name, ModifiersNames.Hope_Editions_Description, 2, true));
            modifiers.Add(ModifiersNames.Blue_Velvet_Editions_Name, new Edition_AddChips(ModifiersNames.Blue_Velvet_Editions_Name, ModifiersNames.Blue_Velvet_Editions_Description, 20)); 
            modifiers.Add(ModifiersNames.Spider_Web_Editions_Name, new Edition_AddMult(ModifiersNames.Spider_Web_Editions_Name, ModifiersNames.Spider_Web_Editions_Description, 10, false));
        
            //Add Enhancements
            modifiers.Add(ModifiersNames.Big_Bounus_Enhancement_Name, new Enhancement_AddChips(ModifiersNames.Big_Bounus_Enhancement_Name, ModifiersNames.Big_Bonus_Enhancement_Description, 30));
            modifiers.Add(ModifiersNames.Big_Mult_Enhancement_Name, new Enhancement_AddMult(ModifiersNames.Big_Mult_Enhancement_Name, ModifiersNames.Big_Mult_Enhancement_Description, 4));
            modifiers.Add(ModifiersNames.Real_Wild_Enhancement_Name, new Enhancement_WildSuit(ModifiersNames.Real_Wild_Enhancement_Name, ModifiersNames.Real_Wild_Enhancement_Description));
            modifiers.Add(ModifiersNames.Real_Gold_Enhancement_Name, new Enhancement_AddCurrency(ModifiersNames.Real_Gold_Enhancement_Name, ModifiersNames.Real_Gold_Enhancement_Description, 3, jsonFacade)); 
            
            return modifiers;
        }
    }
}