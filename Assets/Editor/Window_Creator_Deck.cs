using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using SimulatedBE.Networking.DTOs;
using System.IO;

namespace SimulatedBE.Networking.Editor
{
    public class Window_Creator_Deck : EditorWindow
    {
        string deckName;
        string deckDesc;
        bool created;

        [MenuItem("JSON Creator Tools/Deck Creator")]
        public static void OpenWindow()
        {
            Window_Creator_Deck myWindow = (Window_Creator_Deck)GetWindow(typeof(Window_Creator_Deck));
            myWindow.wantsMouseMove = true;
            myWindow.Show();
        }

        private void OnGUI()
        {
            EditorGUILayout.LabelField("Deck Creator", EditorStyles.largeLabel);

            deckName = EditorGUILayout.TextField("Deck Name", deckName);
            deckDesc = EditorGUILayout.TextField("Deck Description", deckDesc);

            EditorGUILayout.Space(10);

            if(GUILayout.Button("Create Deck"))
            {
                CreateNewDeck();
            }

            if(created)
                EditorGUILayout.HelpBox("Deck Created without errors", MessageType.Info);
        }


        void CreateNewDeck()
        {
            var directoryPath = JsonSerializer.GetPath(Paths.Decks);

            JsonSerializer.CheckDirectory(directoryPath);

            DeckDto deck = new DeckDto(deckName, deckDesc, new List<CardDto>());

            var fileName = "Deck_" + Directory.GetFiles(directoryPath).Length.ToString() + ".json";

            JsonSerializer.WriteJsonFile(deck, fileName, Paths.Decks);

            created = true;
            deckDesc = "";
            deckName = "";
            Repaint();
        }
    
    }
}
