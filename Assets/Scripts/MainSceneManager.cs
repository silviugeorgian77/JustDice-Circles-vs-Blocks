using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneManager : MonoBehaviour
{
    [SerializeField]
    private TextAsset gameConfigAsset;

    [SerializeField]
    private GameplayManager gameplayManager;

    [SerializeField]
    private UIManager uiManager;

    [SerializeField]
    private SaveManager saveManager;

    private GameConfigDataService gameConfigDataService;
    private UserDataLocalService userDataService;

    private GameConfig gameConfig;
    private UserData userData;

    private void Awake()
    {
        uiManager.ShowLoading();

        gameConfigDataService
            = new GameConfigDataService(gameConfigAsset.text);
        gameConfigDataService.GetGameConfig(gameConfig =>
        {
            this.gameConfig = gameConfig;
            OnServiceReady();
            
        });

        userDataService = new UserDataLocalService();
        userDataService.GetUserData(userData =>
        {
            this.userData = userData;
            OnServiceReady();
        });
    }

    private void OnServiceReady()
    {
        if (gameConfig != null)
        {
            if (this.userData == null)
            {
                this.userData = new UserData();
                this.userData.Reset(gameConfig.maxAttackersCount);
            }
            gameplayManager.Init(gameConfig, userData);
            saveManager.Init(userData, userDataService);
            uiManager.Init(gameConfig, userData);

            uiManager.HideLoading();
        }
    }
}
