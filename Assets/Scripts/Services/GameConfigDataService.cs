using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

public class GameConfigDataService : IGameConfigDataProvider
{
    private IGameConfigLocalDataProvider localDataProvider;
    private IGameConfigOnlineDataProvider onlineDataProvider;

    private bool isInitialized;

    public GameConfigDataService(string defaultGameConfigJson)
    {
        localDataProvider = new GameConfigLocalDataService();
        onlineDataProvider = new GameConfigOnlineDataService();
        ManageDefaultGameConfigData(defaultGameConfigJson);
    }

    private async void ManageDefaultGameConfigData(
        string defaultGameConfigJson)
    {
        await Task.Run(() =>
        {
            var filePath = localDataProvider.GetGameConfigFilePath();
            if (!File.Exists(filePath))
            {
                var directoryPath = localDataProvider.GetGameConfigDirectoryPath();
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }
                Debug.Log(
                    nameof(GameConfigDataService) +
                    " Default GameConfig json not copied to persistentDataPath"
                );
                File.WriteAllText(filePath, defaultGameConfigJson);
                Debug.Log(
                    nameof(GameConfigDataService) +
                    " Default GameConfig json copied successful to " +
                    "persistentDataPath"
                );
            }
            else
            {
                Debug.Log(
                    nameof(GameConfigDataService) +
                    " GameConfig local data exists"
                );
            }
            isInitialized = true;
        });
    }

    public async void GetGameConfig(Action<GameConfig> onGameConfig)
    {
        while (!isInitialized)
        {
            await Task.Yield();
        }
        onlineDataProvider.GetGameConfig(gameConfig =>
        {
            if (gameConfig != null)
            {
                Debug.Log(
                    nameof(GameConfigDataService) +
                    " Online GameConfig obtained"
                );
                localDataProvider.SaveGameConfig(
                    gameConfig,
                    (isCacheSuccessful, gameConfig) =>
                    {
                        Debug.Log(
                            nameof(GameConfigDataService) +
                            " Cache successful: " +
                            isCacheSuccessful
                        );
                    }
                );
                onGameConfig?.Invoke(gameConfig);
            }
            else
            {
                Debug.Log(
                    nameof(GameConfigDataService) +
                    " No Online GameConfig data"
                );

                localDataProvider.GetGameConfig(gameConfig =>
                {
                    if (gameConfig != null)
                    {
                        Debug.Log(
                            nameof(GameConfigDataService) +
                            " Local GameConfig obtained"
                        );
                    }
                    else
                    {
                        Debug.Log(
                            nameof(GameConfigDataService) +
                            " No local GameConfig data"
                        );
                    }
                    onGameConfig?.Invoke(gameConfig);
                });
            }
        });
    }
}
