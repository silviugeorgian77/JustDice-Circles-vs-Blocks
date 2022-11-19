
using System;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;

public class GameConfigLocalDataService : IGameConfigLocalDataProvider
{
    private const string FOLDER_NAME = "GameConfig";
    private const string FILE_NAME = "config.json";

    private readonly string DIRECTORY_PATH;

    private readonly string FILE_PATH;

    public GameConfigLocalDataService()
    {
        DIRECTORY_PATH =
            Application.persistentDataPath +
            Path.DirectorySeparatorChar +
            FOLDER_NAME;

        FILE_PATH =
            DIRECTORY_PATH +
            Path.DirectorySeparatorChar +
            FILE_NAME;
    }

    public string GetGameConfigDirectoryPath()
    {
        return DIRECTORY_PATH;
    }

    public string GetGameConfigFilePath()
    {
        return FILE_PATH;
    }

    public async void GetGameConfig(Action<GameConfig> onGameConfigReady)
    {
        GameConfig gameConfig = null;
        await Task.Run(() =>
        {
            try
            {
                if (File.Exists(FILE_PATH))
                {
                    var jsonString = File.ReadAllText(FILE_PATH);
                    gameConfig
                        = JsonConvert.DeserializeObject<GameConfig>(jsonString);
                }
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
            }
        });
        onGameConfigReady?.Invoke(gameConfig);
    }

    public async void SaveGameConfig(
        GameConfig gameConfig,
        Action<bool, GameConfig> onGameConfigSaved)
    {
        bool isSuccess = false;
        await Task.Run(() =>
        {
            try
            {
                if (!Directory.Exists(DIRECTORY_PATH))
                {
                    Directory.CreateDirectory(DIRECTORY_PATH);
                }
                var jsonString = JsonConvert.SerializeObject(gameConfig);
                File.WriteAllText(FILE_PATH, jsonString);
                isSuccess = true;
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
                isSuccess = false;
            }
        });
        onGameConfigSaved?.Invoke(isSuccess, gameConfig);
    }
}
