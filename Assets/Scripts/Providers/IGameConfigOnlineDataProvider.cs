using System;

public interface IGameConfigOnlineDataProvider
{
    void GetGameConfig(Action<GameConfig> onGameConfigReady);
}
