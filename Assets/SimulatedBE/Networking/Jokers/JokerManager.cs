using SimulatedBE.Networking.DTOs;
using System.Collections.Generic;
using System.Linq;
using System;

namespace SimulatedBE.Networking.Jokers
{
    public class JokerManager
    {
        private Dictionary<string, Joker> _jokers = new Dictionary<string, Joker>();
        private IJsonFacade _jsonFacade;

        public JokerManager(IJsonFacade jsonFacade, PokerRules pokerRules)
        {
            _jsonFacade = jsonFacade;
            _jokers = JokerFactory.CreateJokers(jsonFacade, pokerRules);
        }

        public void GetJoker(ItemDto joker)
        {
            if (!_jokers.TryGetValue(joker.ItemName, out Joker outValue))
                return;

            outValue.OnGetJoker(Enum.Parse<JokerRarity>(joker.Rarity));
        }

        public void DestroyJoker(ItemDto joker)
        {
            if (!_jokers.TryGetValue(joker.ItemName, out Joker outValue))
                return;

            outValue.OnDestroyJoker(Enum.Parse<JokerRarity>(joker.Rarity));
        }

        public JokerUseResultDto UseJokers(List<CardDto> cards, List<ItemDto> jokers)
        {
            var result = new List<InstructionDto>();
            var round = _jsonFacade.RoundData;
            var mainInstruction = new InstructionDto("", null,round.Score.ChipsValue,round.Score.MultiplierValue,_jsonFacade.CurrentInventory.PlayerTreasure, _jsonFacade.CurrentInventory, _jsonFacade.RoundData);

            var jokerList = jokers.Where(x => _jokers.ContainsKey(x.ItemName)).Select(x => (x, _jokers[x.ItemName])).ToList();

            for (int i = 0; i < cards.Count; i++)
            {
                for (int m = 0; m < jokerList.Count; m++)
                {
                    if (jokerList[m].Item2.CardDependency && jokerList[m].Item2.CanUseJoker(cards[i]))
                    {
                        var newInstruction = jokerList[m].Item2.UseJoker(cards[i],
                                                                        Enum.Parse<JokerRarity>(jokerList[m].x.Rarity),
                                                                        mainInstruction)
                                                                        .SetItem(jokerList[m].x);
                        mainInstruction = mainInstruction.Additive(newInstruction);
                        result.Add(newInstruction);
                    }
                }
            }

            for (int i = 0; i < jokerList.Count; i++)
            {
                if (!jokerList[i].Item2.CardDependency && jokerList[i].Item2.CanUseJoker(new CardDto()))
                {
                    var newInstruction = jokerList[i].Item2.UseJoker(cards,
                                                                    Enum.Parse<JokerRarity>(jokerList[i].x.Rarity),
                                                                    mainInstruction)
                                                                    .SetItem(jokerList[i].x); ;
                    mainInstruction = mainInstruction.Additive(newInstruction);
                    result.Add(newInstruction);
                }
            }

            RefreshValues(mainInstruction);

            return new JokerUseResultDto(result);
        }

        private void RefreshValues(InstructionDto instructionDto)
        {
            _jsonFacade.CurrentInventory = new InventoryDto(instructionDto.UpdatedInventory.Jokers, instructionDto.UpdatedInventory.Consumables, instructionDto.UpdatedInventory.Vouchers,instructionDto.UpdatedInventory.SavedTags,
                                                            instructionDto.UpdatedTreasure, instructionDto.UpdatedInventory.JokerSlotAmmount, instructionDto.UpdatedInventory.ConsumableSlotAmmount);
            var round = _jsonFacade.RoundData;

            round = new RoundDto(instructionDto.UpdatedRound.AmountOfHands,
                                instructionDto.UpdatedRound.AmountOfDiscards,
                                instructionDto.UpdatedRound.CurrentRound,
                                instructionDto.UpdatedRound.InitialBet,
                                new ScoreDto(round.Score.ScoreValue, instructionDto.AddedScore, instructionDto.AddedMultiplier));

            _jsonFacade.RoundData = round;
        }

        public string SetDescriptionValues(ItemDto itemDto)
        {
            if (!_jokers.TryGetValue(itemDto.ItemName, out Joker joker)) return itemDto.ItemDescription;

            return itemDto.ItemDescription.Replace("[value]", joker.GetValueString(Enum.Parse<JokerRarity>(itemDto.Rarity)));
        }
    }
}

/*
  en el metodo UseJokers hay duplicacion de codigo, sacar la logica en q se repite a un metodo aparte o mejorar el metodo existente
 */
