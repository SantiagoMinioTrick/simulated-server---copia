using SimulatedBE.Networking.DTOs;
using SimulatedBE.Networking;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System;

namespace SimulatedBE.Networking.Editor
{
    public class Window_Creator_Card : EditorWindow
    {
        Symbols symbol;
        CardValue value = CardValue.Ace;
        int score;
        bool created;
        bool toogleGroupActivated;

        Tuple<string, int>[] scorePerValue;

        List<CardDto> cards = new List<CardDto>();

        [MenuItem("JSON Creator Tools/Card Creator")]
        public static void OpenWindow()
        {
            Window_Creator_Card myWindow = (Window_Creator_Card)GetWindow(typeof(Window_Creator_Card));
            myWindow.wantsMouseMove = true;
            myWindow.Show();

            var names = Enum.GetNames(typeof(CardValue));
            myWindow.scorePerValue = new Tuple<string, int>[names.Length];

            for (int i = 0; i < names.Length; i++)
            {
                myWindow.scorePerValue[i] = new Tuple<string, int>(names[i], 0);
            }
            JsonSerializer.CheckDirectory(JsonSerializer.GetPath(Paths.Cards));
            if (JsonSerializer.FileExist(JSONFilesNames.Preset_CardsList, Paths.Cards))
                myWindow.cards = JsonSerializer.ReadJsonFile<List<CardDto>>(JSONFilesNames.Preset_CardsList, Paths.Cards);
        }

        private void OnGUI()
        {
            EditorGUILayout.LabelField("Deck Creator", EditorStyles.largeLabel);

            symbol = (Symbols)EditorGUILayout.EnumPopup("Card Symbol", symbol);
            value = (CardValue)EditorGUILayout.EnumPopup("Card Value", value);
            score = EditorGUILayout.IntField("Card Score", score);

            EditorGUILayout.Space(10);

            if (GUILayout.Button("AddCard"))
            {
                AddCard();
            }

            if (created)
                EditorGUILayout.HelpBox("Deck Created without errors", MessageType.Info);

            toogleGroupActivated = EditorGUILayout.BeginToggleGroup("Cards score per value", toogleGroupActivated);

            for (int i = 0; i < scorePerValue.Length; i++)
            {
                var newValue = scorePerValue[i].Item2;

                newValue = EditorGUILayout.IntField(scorePerValue[i].Item1, newValue);

                if (newValue != scorePerValue[i].Item2)
                    scorePerValue[i] = new Tuple<string, int>(scorePerValue[i].Item1, newValue);
            }

            EditorGUILayout.EndToggleGroup();

            if (GUILayout.Button("Create Default Card Deck"))
            {
                CreateDefaultCardJSON();
            }
        }


        void AddCard()
        {
            CardDto newCard = new CardDto("",symbol, value, score);

            cards.Add(newCard);
            symbol = 0;
            value = 0;
            score = 0;
            created = true;
            Repaint();
            JsonSerializer.CheckDirectory(JsonSerializer.GetPath(Paths.Cards));
            JsonSerializer.WriteJsonFile(cards, JSONFilesNames.Preset_CardsList, Paths.Cards);
        }

        void CreateDefaultCardJSON()
        {
            cards.Clear();
            var symbols = Enum.GetNames(typeof(Symbols));
            for (int i = 0;i < symbols.Length;i++)
            {
                for (int j = 0;j < scorePerValue.Length; j++)
                {
                    CardDto newCard = new CardDto("", Enum.Parse<Symbols>(symbols[i]), Enum.Parse<CardValue>(scorePerValue[j].Item1), scorePerValue[j].Item2);

                    cards.Add(newCard);
                }
            }
            JsonSerializer.CheckDirectory(JsonSerializer.GetPath(Paths.Cards));
            JsonSerializer.WriteJsonFile(cards, JSONFilesNames.Preset_CardsList, Paths.Cards);
        }
    }
}
