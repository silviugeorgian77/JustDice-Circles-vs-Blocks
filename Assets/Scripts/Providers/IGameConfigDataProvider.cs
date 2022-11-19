using System;

public interface IGameConfigDataProvider
{
    void GetGameConfig(Action<GameConfig> onGameConfig);
}
