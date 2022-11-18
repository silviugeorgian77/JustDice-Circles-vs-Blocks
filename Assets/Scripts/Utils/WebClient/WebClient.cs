using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class WebClient
{
    private readonly ISerializationOption serializationOption;

    public WebClient(ISerializationOption serializationOption)
    {
        this.serializationOption = serializationOption;
    }

    public async void Get<TResultType>(
        string url,
        Action<TResultType, UnityWebRequest.Result> action)
    {
        try
        {
            using var www = UnityWebRequest.Get(url);

            www.timeout = 10;

            www.SetRequestHeader(
                "Content-Type",
                serializationOption.ContentType
            );

            var operation = www.SendWebRequest();

            while (!operation.isDone)
            {
                await Task.Yield();
            }

            if (www.result == UnityWebRequest.Result.Success)
            {
                var result = serializationOption.Deserialize<TResultType>(
                    www.downloadHandler.text
                );
                action?.Invoke(result, www.result);
            }
            else
            {
                Debug.LogError($"Failed: {www.error}");
                action?.Invoke(default, www.result);
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"{nameof(Get)} failed: {ex.Message}");

            action?.Invoke(
                default,
                UnityWebRequest.Result.DataProcessingError
            );
        }
    }
}
