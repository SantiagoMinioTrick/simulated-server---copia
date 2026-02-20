using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimulatedBE.Networking.Jokers
{
    public static class JokerFactory
    {
        public static Joker SetRarities(this Joker joker, float rarityOne, float rarityTwo, float rarityThree)
        {
            return joker.SetRarity(JokerRarity.Common, rarityOne).SetRarity(JokerRarity.Rare, rarityTwo).SetRarity(JokerRarity.Epic, rarityThree);
        }

        public static Dictionary<string, Joker> CreateJokers(IJsonFacade _jsonFacade, PokerRules pokerRules)
        {
            Dictionary<string, Joker> result = new Dictionary<string, Joker>();

            result.Add(JokerNames.Joker_Name_Warlike, new Joker_SymbolExtraScore(true, Symbols.Spades).SetRarities(100,150,200));
            result.Add(JokerNames.Joker_Name_Romantic, new Joker_SymbolExtraScore(true, Symbols.Hearts).SetRarities(100,150,200));
            result.Add(JokerNames.Joker_Name_Luck, new Joker_SymbolExtraScore(true, Symbols.Clubs).SetRarities(100, 150, 200));
            result.Add(JokerNames.Joker_Name_Rich, new Joker_SymbolExtraScore(true, Symbols.Diamonds).SetRarities(100, 150, 200));
            result.Add(JokerNames.Joker_Name_Jack_Of_All_Trades, new Joker_JackOfAllTrades(false, _jsonFacade).SetRarities(500,750,1000));
            result.Add(JokerNames.Joker_Name_Treasure_Multiplier, new Joker_TreasureMultiplier(true, _jsonFacade).SetRarities(2, 3, 4));
            result.Add(JokerNames.Joker_Name_Discard_Treasure, new Joker_DiscardTreasure(false, 5).SetRarities(1, 1, 1));
            result.Add(JokerNames.Joker_Name_EGG, new Joker_Egg(false).SetRarities(1,2,3));
            result.Add(JokerNames.Joker_Name_All_Princes, new Joker_FaceCardHeld(false, _jsonFacade, CardValue.Jack).SetRarities(10, 20, 30));
            result.Add(JokerNames.Joker_Name_All_Queens, new Joker_FaceCardHeld(false, _jsonFacade, CardValue.Queen).SetRarities(10, 20, 30));
            result.Add(JokerNames.Joker_Name_All_Kings, new Joker_FaceCardHeld(false, _jsonFacade, CardValue.King).SetRarities(10, 20, 30));
            result.Add(JokerNames.Joker_Name_Pretty_Face, new Joker_FaceCardHeld(false, _jsonFacade, CardValue.Jack, CardValue.Queen, CardValue.King).SetRarities(10, 20, 30));
            result.Add(JokerNames.Joker_Name_Risk_Taker, new Joker_RiskTaker(false).SetRarities(1,1,1));
            result.Add(JokerNames.Joker_Name_Tricky_Guy, new Joker_TrickyGuy(false).SetRarities(200, 300, 500));
            result.Add(JokerNames.Joker_Name_Negative_Score, new Joker_NegativeScore(false, _jsonFacade).SetRarities(20,10,5));
            result.Add(JokerNames.Joker_Name_Very_Bad_with_Numbers, new Joker_VeryBadWithNumbers(false, pokerRules).SetRarities(2,2,2));
            result.Add(JokerNames.Joker_Name_Not_Good_With_Numbers, new Joker_BadWithNumbers(false, pokerRules).SetRarities(4,4,4));
            result.Add(JokerNames.Joker_Name_Treasure_Collector, new Joker_TreasureCollector(false, _jsonFacade));
            result.Add(JokerNames.Joker_Name_Cash_Under_the_Couch, new Joker_CashUnderTheCoach(false).SetRarities(5,10,15));
            return result;
        }
    }
}
