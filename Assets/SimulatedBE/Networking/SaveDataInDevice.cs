using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace SimulatedBE.Networking
{
    public static class SaveDataInDevice
    {
        private static string persistentFolderPath;
        private const string DefaultDatabaseFolderName = "";
        private static string[] filePaths;
        private const string fileIndexName = "fileIndex.json";

        public static async Task GenerateDataInDevice(string dataBaseFolderName = DefaultDatabaseFolderName)
        {
            persistentFolderPath = Path.Combine(Application.persistentDataPath, dataBaseFolderName);
            await CopyAssetsToPersistentDataPath(dataBaseFolderName);
        }

        private static async Task CopyAssetsToPersistentDataPath(string dataBaseFolderName)
        {
            try
            {
                string sourceFolderPath = Path.Combine(Application.streamingAssetsPath, dataBaseFolderName);
                EnsureDirectoryExists(persistentFolderPath);
                await LoadFileListAsync();

                foreach (var file in filePaths)
                {
                    string destinationFilePath = Path.Combine(persistentFolderPath, file);

                    if (File.Exists(destinationFilePath.Replace("\\", "/")) && IsFileIndexUpToDate(destinationFilePath.Replace("\\", "/"), Path.Combine(Application.streamingAssetsPath, file))) 
                        continue;

                    EnsureDirectoryExists(Path.GetDirectoryName(destinationFilePath));

                    if (await CopyFileAsync(Path.Combine(Application.streamingAssetsPath, file), destinationFilePath))
                        Debug.Log($"File copied to: {destinationFilePath}");
                    else
                        Debug.LogError($"Error reading the file: {file}");
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to copy the files: {e.Message}");
            }
        }

        private static void EnsureDirectoryExists(string path)
        {
            if (!string.IsNullOrEmpty(path) && !Directory.Exists(path))
                Directory.CreateDirectory(path);
        }

        private static async Task<bool> CopyFileAsync(string sourceFilePath, string destinationFilePath)
        {
            try
            {
                byte[] fileBytes = await DownloadFileBytesAsync(sourceFilePath);
                await File.WriteAllBytesAsync(destinationFilePath, fileBytes);
                return true;
            }
            catch (Exception e)
            {
                Debug.LogError($"Error downloading {sourceFilePath}: {e.Message}");
                return false;
            }
        }

        private static async Task<byte[]> DownloadFileBytesAsync(string path)
        {
#if !UNITY_STANDALONE_OSX && !UNITY_IOS
            using var request = UnityWebRequest.Get(path);
            var taskCompletionSource = new TaskCompletionSource<byte[]>();

            request.SendWebRequest().completed += _ =>
            {
                if (request.result == UnityWebRequest.Result.Success)
                    taskCompletionSource.TrySetResult(request.downloadHandler.data);
                else
                    taskCompletionSource.TrySetException(new Exception($"Error downloading the file: {request.error}"));
            };

            return await taskCompletionSource.Task;
#else
            return await File.ReadAllBytesAsync(path);
#endif
        }

        private static async Task LoadFileListAsync()
        {
            string path = Path.Combine(Application.streamingAssetsPath, fileIndexName);
            try
            {
                string json = await DownloadFileStringAsync(path);
                filePaths = !string.IsNullOrEmpty(json)
                    ? JsonUtility.FromJson<FileList>(json)?.files ?? Array.Empty<string>()
                    : Array.Empty<string>();

                Debug.Log(filePaths.Length > 0 ? $"Loaded {filePaths.Length} files." : $"{fileIndexName} is empty or malformed.");
            }
            catch (Exception ex)
            {
                filePaths = Array.Empty<string>();
                Debug.LogError($"Error during JSON loading: {ex.Message}");
            }
        }

        private static async Task<string> DownloadFileStringAsync(string path)
        {

#if !UNITY_STANDALONE_OSX && !UNITY_IOS
            using var request = UnityWebRequest.Get(path);
            var taskCompletionSource = new TaskCompletionSource<string>();

            request.SendWebRequest().completed += _ =>
            {
                if (request.result == UnityWebRequest.Result.Success)
                    taskCompletionSource.TrySetResult(Encoding.UTF8.GetString(request.downloadHandler.data));
                else
                    taskCompletionSource.TrySetException(new Exception($"Error downloading the file: {request.error}"));
            };

            return await taskCompletionSource.Task;
#else
            return await File.ReadAllTextAsync(path);
#endif
        }

        private static bool IsFileIndexUpToDate(string outputFilePath, string newJsonFilePath) => File.ReadAllText(outputFilePath) == File.ReadAllText(newJsonFilePath);

        public static string[] GetFilePaths() => filePaths ?? Array.Empty<string>();

        [System.Serializable]
        private class FileList
        {
            public string[] files;
        }
    }

}

