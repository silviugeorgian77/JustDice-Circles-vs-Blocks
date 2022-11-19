using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneManager : MonoBehaviour
{
    [SerializeField]
    private TextAsset gameConfigAsset;

    [SerializeField]
    private GameplayManager gameplayManager;

    private void Awake()
    {
        var dataService = new GameConfigDataService(gameConfigAsset.text);
        dataService.GetGameConfig(gameConfig =>
        {
            gameplayManager.Init(gameConfig);
        });
    }
}
