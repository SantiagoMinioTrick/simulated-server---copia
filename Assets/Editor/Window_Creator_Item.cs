using SimulatedBE.Networking.DTOs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using Unity.Plastic.Newtonsoft.Json;
using Unity.VisualScripting.YamlDotNet.Core.Tokens;
using UnityEditor;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

namespace SimulatedBE.Networking.Editor
{
    public enum ShopItemCategory
    {
        Jokers,
        FarmCards,
        Planetes,
        Vouchers,
        Packs
    }

    public class Window_Creator_Item : EditorWindow
    {
        ShopItemCategory category;
        string itemName;
        string itemDescription;
        int itemCost;
        HandState handState;
        ItemType itemType;
        bool created;

        ShopItemCategory categoryToRemove;
        string IDToRemove;

        bool removed;
        string removedMessage;
        string path = Application.dataPath + "/NewPresets/";


        Dictionary<ShopItemCategory, string> itemsPaths =  new Dictionary<ShopItemCategory, string>()
        {
            { ShopItemCategory.Jokers, JSONFilesNames.Preset_Shop_Jokers },
            { ShopItemCategory.FarmCards, JSONFilesNames.Preset_Shop_FarmCards },
            { ShopItemCategory.Planetes, JSONFilesNames.Preset_Shop_Planets },
            { ShopItemCategory.Vouchers, JSONFilesNames.Preset_Shop_Vouchers },
            { ShopItemCategory.Packs, JSONFilesNames.Preset_Shop_Packs }
        };

        [MenuItem("JSON Creator Tools/Item Creator")]
        public static void OpenWindow()
        {
            Window_Creator_Item myWindow = (Window_Creator_Item)GetWindow(typeof(Window_Creator_Item));
            myWindow.wantsMouseMove = true;
            myWindow.Show();
        }

        private void OnGUI()
        {
            EditorGUILayout.LabelField("Deck Creator", EditorStyles.largeLabel);


            category = (ShopItemCategory)EditorGUILayout.EnumPopup("Item Category", category);

            itemName = EditorGUILayout.TextField("Item Name", itemName);
            itemDescription = EditorGUILayout.TextField("Item Description", itemDescription);
            itemCost = EditorGUILayout.IntField("Item Cost", itemCost);


            itemType = (ItemType)EditorGUILayout.EnumPopup("Item Type", itemType);
            
            if(category == ShopItemCategory.Jokers) handState = (HandState)EditorGUILayout.EnumPopup("Joker Hand State Execution", handState);

            EditorGUILayout.Space(10);

            if (GUILayout.Button("Add Item"))
            {
                AddItem();
            }

            if (created)
                EditorGUILayout.HelpBox("Item Created without errors", MessageType.Info);

            EditorGUILayout.Space(10);

            categoryToRemove = (ShopItemCategory)EditorGUILayout.EnumPopup("Item Category", categoryToRemove);
            IDToRemove = EditorGUILayout.TextField("Item Name", IDToRemove);


            if (GUILayout.Button("Remove Item"))
            {
                RemoveItem();
            }

            if (removed)
                EditorGUILayout.HelpBox(removedMessage, MessageType.Info);
        }


        void AddItem()
        {
            List<ShopItemDto> file = new List<ShopItemDto>();
            if (JsonSerializer.FileExist(itemsPaths[category], path))
                file = JsonSerializer.ReadJsonFile<List<ShopItemDto>>(itemsPaths[category], path.Replace("\\", "/"));

            ItemDto newItem = new ItemDto("", itemName, itemDescription, itemCost, itemType, category == ShopItemCategory.Jokers ? handState.ToString() : "", JokerRarity.Common.ToString());

            //float appearChance = category == ShopItemCategory.Jokers ? ShopManager.jokerAppearChances[rarity] : 1;

            ShopItemDto newShopItem = new ShopItemDto(itemName, newItem, 1);

            var index = AlreadyExist(file, newShopItem.ID);

            for (int i = 0; i < file.Count; i++)
            {
                Debug.Log(JsonConvert.SerializeObject(file[i]));
            }
            if (index == -1)
            {
                file.Add(newShopItem);
            }
            else
                file[index] = newShopItem;

            JsonSerializer.WriteJsonFile(file, itemsPaths[category], path);

            itemName = "";
            itemDescription = "";
            itemCost = 0;

            created = true;
            Repaint();
        }

        void RemoveItem()
        {
            List<ShopItemDto> file = new List<ShopItemDto>();
            if (JsonSerializer.FileExist(itemsPaths[categoryToRemove], Paths.Items))
                file = JsonSerializer.ReadJsonFile<List<ShopItemDto>>(itemsPaths[categoryToRemove], path.Replace("\\", "/"));

            var index = AlreadyExist(file, IDToRemove);

            if (index != -1)
            {
                file.RemoveAt(index);
                removedMessage = "Item removed without errors";
            }
            else
            {
                removedMessage = "The item was not found";
            }


            JsonSerializer.WriteJsonFile(file, itemsPaths[categoryToRemove], path);

            IDToRemove = "";

            removed = true;
            Repaint();
        }

        int AlreadyExist(List<ShopItemDto> file, string id)
        {
            for (int i = 0; i < file.Count; i++)
            {
                if(id == file[i].ID)
                {
                    return i;
                }
            }

            return -1;
        }
    }
}
