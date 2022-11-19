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

    private void Awake()
    {
        uiManager.ShowLoading();

        var dataService = new GameConfigDataService(gameConfigAsset.text);
        dataService.GetGameConfig(gameConfig =>
        {
            gameplayManager.Init(gameConfig);
            uiManager.HideLoading();
        });
    }
}
