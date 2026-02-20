using SimulatedBE.Networking.DTOs;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace SimulatedBE.Networking.Editor
{
    public class Window_Creator_Tag : EditorWindow
    {
        string tagName;
        string tagID;
        string tagDesc;
        bool created;
        HandState handState;

        [MenuItem("JSON Creator Tools/Tag Creator")]
        public static void OpenWindow()
        {
            Window_Creator_Tag myWindow = (Window_Creator_Tag)GetWindow(typeof(Window_Creator_Tag));
            myWindow.wantsMouseMove = true;
            myWindow.Show();
        }

        private void OnGUI()
        {
            EditorGUILayout.LabelField("Tag Creator", EditorStyles.largeLabel);
            tagID = EditorGUILayout.TextField("Tag ID", tagID);

            tagName = EditorGUILayout.TextField("Tag Name", tagName);
            tagDesc = EditorGUILayout.TextField("Tag Description", tagDesc);
            tagDesc = EditorGUILayout.TextField("Tag Description", tagDesc);
            handState = (HandState)EditorGUILayout.EnumPopup("Tag Hand State Execution", handState);

            EditorGUILayout.Space(10);

            if (GUILayout.Button("Create Tag"))
            {
                CreateNewTag();
            }

            if (created)
                EditorGUILayout.HelpBox("Tag Created without errors", MessageType.Info);
        }


        void CreateNewTag()
        {
            var directoryPath = JsonSerializer.GetPath(Paths.Tags);

            JsonSerializer.CheckDirectory(directoryPath);

            TagDto tag = new TagDto(tagID,tagName, tagDesc,"", handState.ToString());

            var fileName = "Tag_" + tagName + ".json";

            JsonSerializer.WriteJsonFile(tag, fileName, Paths.Tags);

            created = true;
            tagDesc = "";
            tagName = "";
            Repaint();
        }

    }
}
