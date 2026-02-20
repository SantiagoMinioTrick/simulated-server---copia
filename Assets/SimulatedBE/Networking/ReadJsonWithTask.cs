using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

namespace SimulatedBE.Networking
{
    public class ReadJsonWithTask : MonoBehaviour
    {
        private string filePath;

        private void Start()
        {
            filePath = Application.streamingAssetsPath + "/Database/Decks/Deck_0.json";
        }

        private async UniTask<T> ReadJsonFileAsync<T>()
        {
            var jsonContent = await GetJsonFileAsync(filePath);
            return JsonConvert.DeserializeObject<T>(jsonContent);
        }

        private async UniTask<string> GetJsonFileAsync(string path)
        {
            var request = UnityWebRequest.Get(path);
        
            var taskCompletionSource = new UniTaskCompletionSource<string>();

            request.SendWebRequest().completed += (asyncOp) =>
            {
                if (request.result == UnityWebRequest.Result.Success)
                {
                    taskCompletionSource.TrySetResult(request.downloadHandler.text);
                }
                else
                {
                    taskCompletionSource.TrySetException(new System.Exception("Error al descargar el archivo: " + request.error));
                }
            };

            return await taskCompletionSource.Task;
        }
    }
}
