using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConfigOnlineDataService : IGameConfigOnlineDataProvider
{
    private const string END_POINT = "https://drive.google.com/uc?id=1jpZT0Qq2GezoaJqwPEKn2OvQlyp8oPPE";

    private WebClient webClient = new WebClient(new JsonSerializationOption());

    public void GetGameConfig(Action<GameConfig> onGameConfigReady)
    {
        webClient.Get<GameConfig>(END_POINT, (gameConfig, result) => {
            onGameConfigReady?.Invoke(gameConfig);
        });
    }
}
