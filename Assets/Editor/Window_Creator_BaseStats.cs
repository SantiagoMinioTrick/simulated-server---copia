using SimulatedBE.Networking.DTOs;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace SimulatedBE.Networking.Editor
{
    public class Window_Creator_BaseStats : EditorWindow
    {
        int ammountOfCards;
        int ammountOfPlayHands;
        int ammountOfDiscards;
        string smallBreachName;
        string bigBreachName;
        int baseScore;
        int maxBetToWin;
        int anteMultipliersSize;
        int jokerSlots;
        int consumableSlots;
        float[] anteMultipliers = new float[0];
        bool created;


        [MenuItem("JSON Creator Tools/Base Stats Creator")]
        public static void OpenWindow()
        {
            Window_Creator_BaseStats myWindow = (Window_Creator_BaseStats)GetWindow(typeof(Window_Creator_BaseStats));
            myWindow.wantsMouseMove = true;
            myWindow.Show();

            JsonSerializer.CheckDirectory(JsonSerializer.GetPath(Paths.BaseStats));
            if (JsonSerializer.FileExist(JSONFilesNames.Preset_BaseStats, Paths.BaseStats))
            {
                BaseStatsDto stats = JsonSerializer.ReadJsonFile<BaseStatsDto>(JSONFilesNames.Preset_BaseStats, Paths.BaseStats);
                myWindow.ammountOfCards = stats.AmountOfCardsInHand;
                myWindow.ammountOfPlayHands = stats.AmmountOfPlayHands;
                myWindow.ammountOfDiscards = stats.AmmountOfDiscards;
                myWindow.smallBreachName = stats.SmallBreachName;
                myWindow.bigBreachName = stats.BigBreachName;
                myWindow.baseScore = stats.BreachBaseScore;
                myWindow.anteMultipliers = stats.AnteMultipliers;
                myWindow.anteMultipliersSize = myWindow.anteMultipliers.Length;
            }
        }

        private void OnGUI()
        {
            EditorGUILayout.LabelField("Base Stats", EditorStyles.largeLabel);

            ammountOfCards = EditorGUILayout.IntField("Ammount of Cards in Hand", ammountOfCards);
            ammountOfPlayHands = EditorGUILayout.IntField("Ammount of Play Hands", ammountOfPlayHands);
            ammountOfDiscards = EditorGUILayout.IntField("Ammount of Discards", ammountOfCards);
            smallBreachName = EditorGUILayout.TextField("Small Breach Name", smallBreachName);
            bigBreachName = EditorGUILayout.TextField("Big Breach Name", bigBreachName);
            baseScore = EditorGUILayout.IntField("Breach Base Score", baseScore);
            maxBetToWin = EditorGUILayout.IntField("Max Bet To Win", maxBetToWin);
            jokerSlots = EditorGUILayout.IntField("Joker Slots", jokerSlots);
            consumableSlots = EditorGUILayout.IntField("Consumable Slots", consumableSlots);
            EditorGUILayout.Space(5);

            EditorGUILayout.LabelField("Ante Multipliers");
            var currSize = anteMultipliersSize;

            currSize = EditorGUILayout.IntField("Array Size", currSize);

            if(currSize != anteMultipliersSize)
            {
                anteMultipliersSize = currSize;

                var newArray = new float[anteMultipliersSize];

                for (int i = 0; i < newArray.Length; i++)
                {
                    if (i >= anteMultipliers.Length) break;

                    newArray[i] = anteMultipliers[i];
                }
                anteMultipliers = newArray;
                Repaint();
                return;
            }

            for (int i = 0;i < anteMultipliers.Length; i++)
            {
                anteMultipliers[i] = EditorGUILayout.FloatField("Ante Multiplier " + (i +1).ToString(), anteMultipliers[i]);
            }

            EditorGUILayout.Space(10);

            if (GUILayout.Button("Refresh Base Stats"))
            {
                RefreshBaseStats();
            }

            if (created)
                EditorGUILayout.HelpBox("Deck Created without errors", MessageType.Info);
        }

        void RefreshBaseStats()
        {
            JsonSerializer.CheckDirectory(JsonSerializer.GetPath(Paths.BaseStats));

            var newBaseStats = new BaseStatsDto(ammountOfCards, ammountOfPlayHands, ammountOfDiscards,smallBreachName, bigBreachName, baseScore,maxBetToWin, jokerSlots, consumableSlots, anteMultipliers);
            JsonSerializer.WriteJsonFile(newBaseStats, JSONFilesNames.Preset_BaseStats, Paths.BaseStats);
        }
    }
}
