using System;
using Newtonsoft.Json;

[Serializable]
public class Attacker
{
    [JsonProperty("id")]
    private int id;
    [JsonIgnore]
    public int Id
    {
        get
        {
            return id;
        }
        set
        {
            id = value;
        }
    }

    [JsonProperty("upgradeLevel")]
    private int upgradeLevel;
    [JsonIgnore]
    public Action<int> onUpgradeLevelChanged;
    [JsonIgnore]
    public int UpgradeLevel
    {
        get
        {
            return upgradeLevel;
        }
        set
        {
            upgradeLevel = value;
            onUpgradeLevelChanged?.Invoke(upgradeLevel);
        }
    }

}
