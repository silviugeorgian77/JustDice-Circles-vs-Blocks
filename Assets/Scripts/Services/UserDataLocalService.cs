
using System;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;

public class UserDataLocalService : IUserDataLocalProvider
{
    private const string FOLDER_NAME = "UserData";
    private const string FILE_NAME = "userData.json";

    private readonly string DIRECTORY_PATH;

    private readonly string FILE_PATH;

    public UserDataLocalService()
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

    public string GetUserDataDirectoryPath()
    {
        return DIRECTORY_PATH;
    }

    public string GetUserDataFilePath()
    {
        return FILE_PATH;
    }

    public async void GetUserData(Action<UserData> onUserDataReady)
    {
        UserData userData = null;
        await Task.Run(() =>
        {
            try
            {
                if (File.Exists(FILE_PATH))
                {
                    var jsonString = File.ReadAllText(FILE_PATH);
                    userData
                        = JsonConvert.DeserializeObject<UserData>(jsonString);
                }
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
            }
        });
        onUserDataReady?.Invoke(userData);
    }

    public async void SaveUserData(
        UserData userData,
        Action<bool, UserData> onUserDataSaved)
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
                var jsonString = JsonConvert.SerializeObject(userData);
                File.WriteAllText(FILE_PATH, jsonString);
                isSuccess = true;
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
                isSuccess = false;
            }
        });
        onUserDataSaved?.Invoke(isSuccess, userData);
    }
}
