using System;

public interface IUserDataLocalProvider
{
    string GetUserDataDirectoryPath();
    string GetUserDataFilePath();
    void GetUserData(Action<UserData> onUserDataReady);
    void SaveUserData(
        UserData userData,
        Action<bool, UserData> onUserDataSaved
    );
}