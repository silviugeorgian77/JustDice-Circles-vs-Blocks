using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    [SerializeField]
    private Delayer delayer;

    [SerializeField]
    private float autoSaveIntervalS = 10f;

    private UserData userData;
    private IUserDataLocalProvider userDataLocalProvider;

    public void Init(
        UserData userData,
        IUserDataLocalProvider userDataLocalProvider)
    {
        this.userData = userData;
        this.userDataLocalProvider = userDataLocalProvider;
        AutoSave();
    }

    private void AutoSave()
    {
        delayer.AddDelay(autoSaveIntervalS, delay =>
        {
            SaveGame();
            AutoSave();
        });
    }

    // ToDo keep either OnApplicationPause or OnApplicationFocus
    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            SaveGame();
        }
    }

    private void OnApplicationFocus(bool focus)
    {
        if (!focus)
        {
            SaveGame();
        }
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    private void SaveGame()
    {
        if (userDataLocalProvider == null || userData == null)
        {
            return;
        }
        userDataLocalProvider.SaveUserData(userData, (success, userData) =>
        {
            if (success)
            {
                Debug.Log(nameof(SaveManager) + " Autosave Successful");
            }
            else
            {
                Debug.Log(nameof(SaveManager) + " Autosave Failed");
            }
        });
    }
}
