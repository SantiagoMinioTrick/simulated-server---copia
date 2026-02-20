using System.IO;
﻿using System.Collections.Generic;
using Newtonsoft.Json;
using Application = UnityEngine.Device.Application;

namespace SimulatedBE.Networking
{
    public enum Paths
    {
        Decks,
        Cards,
        PlayerData,
        GameData,
        Breaches,
        BaseStats,
        Tags,
        Items
    }

    public static class JsonSerializer
    {
        private static readonly Dictionary<Paths, string> FolderPaths = new()

        {
            { Paths.GameData, Application.persistentDataPath + "/Database/GameData/" },
            { Paths.PlayerData, Application.persistentDataPath + "/Database/PlayerData/" },
            { Paths.Decks, Application.persistentDataPath + "/Database/Decks/" },
            { Paths.BaseStats, Application.persistentDataPath + "/Database/BaseStats/" },
            { Paths.Cards, Application.persistentDataPath + "/Database/Cards/" },
            { Paths.Tags, Application.persistentDataPath + "/Database/Tags/" },
            { Paths.Items, Application.persistentDataPath + "/Database/Items/" }
        };

        public static void WriteJsonFile<T>(T item, string fileName, Paths folder)
        {
            CheckDirectory(GetPath(folder));
            var newJson = JsonConvert.SerializeObject(item);
            File.WriteAllText(GetPath(folder) + fileName, newJson);
        }

        public static void WriteJsonFile<T>(T item, string fileName, string folder)
        {
            CheckDirectory(folder);
            var newJson = JsonConvert.SerializeObject(item);
            File.WriteAllText(folder + fileName, newJson);
        }

        public static T ReadJsonFile<T>(string fileName, Paths folder)
        {
            var loadedJson = File.ReadAllText(GetPath(folder) + fileName);
            return JsonConvert.DeserializeObject<T>(loadedJson);
        }
        public static T ReadJsonFile<T>(string fileName, string folder)
        {
            var loadedJson = File.ReadAllText(folder + fileName);
            return JsonConvert.DeserializeObject<T>(loadedJson);
        }

        public static List<T> ReadJsonFiles<T>(Paths folder)
        {
            var convertedJsons = new List<T>();
            var jsonFiles = Directory.GetFiles(GetPath(folder), "*.json");
            foreach (var json in jsonFiles)
            {
                var loadedJson = File.ReadAllText(json);
                convertedJsons.Add(JsonConvert.DeserializeObject<T>(loadedJson));
            }

            return convertedJsons;
        }

        public static bool FileExist(string fileName, Paths folder)
        {
            return File.Exists(GetPath(folder) + fileName);
        }

        public static bool FileExist(string fileName, string folder)
        {
            return File.Exists(folder + fileName);
        }

        public static string GetPath(Paths folder)
        {
            return FolderPaths.GetValueOrDefault(folder, " ");
        }

        public static void CheckDirectory(string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }
    }
}