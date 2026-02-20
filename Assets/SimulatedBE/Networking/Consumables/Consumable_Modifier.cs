using System.Collections.Generic;
using SimulatedBE.Networking.DTOs;
using SimulatedBE.Networking.Modifiers;

namespace SimulatedBE.Networking.Consumable
{
    public class Consumable_Modifier : IConsumable
    {
        private readonly ModifierManager _modifierManager;
        private readonly string _modifierName;

        public Consumable_Modifier(string modifierName, ModifierManager modifierManager)
        {
            _modifierManager = modifierManager;
            _modifierName = modifierName;
        }
        public List<CardDto> ApplyConsumable(List<CardDto> selectedCards) => _modifierManager.ApplyModifier(_modifierName, selectedCards);
    }
}