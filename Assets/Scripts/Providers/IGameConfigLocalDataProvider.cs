using System;

public interface IGameConfigLocalDataProvider
{
    string GetGameConfigDirectoryPath();
    string GetGameConfigFilePath();
    void GetGameConfig(Action<GameConfig> onGameConfigReady);
    void SaveGameConfig(
        GameConfig gameConfig,
        Action<bool, GameConfig> onGameConfigSaved
    );
}
