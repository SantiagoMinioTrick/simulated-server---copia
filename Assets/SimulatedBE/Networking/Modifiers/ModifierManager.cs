using System.Collections.Generic;
using SimulatedBE.Networking.DTOs;
using UnityEngine;

namespace SimulatedBE.Networking.Modifiers
{
    public class ModifierManager
    {
        private readonly Dictionary<string, IModifier> _modifiers;
        private IJsonFacade _jsonFacade;

        public ModifierManager(IJsonFacade jsonFacade)
        {
            _jsonFacade = jsonFacade;
            _modifiers = ModifiersProvider.CreateModifiers(jsonFacade);
        }
    
        public List<CardDto> ApplyModifier(string modifierName, List<CardDto> cardsToModify)
        {
            var newList = new List<CardDto>();
            if(_modifiers.TryGetValue(modifierName, out var modifier))
            {
                for(var i = 0; i < cardsToModify.Count; i++)
                {
                    newList.Add(modifier.ApplyModifier(cardsToModify[i]));
                }
            }
            else
            {
                Debug.LogWarning("The Modifier doesn't exist.");
                newList = cardsToModify;
            }

            return newList;
        }
        
        public JokerUseResultDto UseModifiers(List<CardDto> cards)
        {
            List<InstructionDto> result = new();
            var round = _jsonFacade.RoundData;
            InstructionDto mainInstruction = new("", null, round.Score.ChipsValue, round.Score.MultiplierValue, _jsonFacade.CurrentInventory.PlayerTreasure, _jsonFacade.CurrentInventory, _jsonFacade.RoundData);

            foreach (var card in cards)
            {
                var cardModifiers = new[] { card.Edition, card.Enhancement, card.Seal };

                foreach (var modifier in cardModifiers)
                {
                    if (string.IsNullOrEmpty(modifier.Name) || !_modifiers.TryGetValue(modifier.Name, out var mod)) continue;
                    
                    var newInstruction = mod.UseModifier(card, mainInstruction);
                    mainInstruction = mainInstruction.Additive(newInstruction);
                    result.Add(newInstruction);
                }
            }

            return new JokerUseResultDto(result);
        }
        
    }
}

