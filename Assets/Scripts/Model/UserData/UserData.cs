using System;
using System.Collections.Generic;
using Newtonsoft.Json;

[Serializable]
public class UserData
{
    [JsonProperty("currencyCount")]
    private float currencyCount;
    [JsonIgnore]
    public Action<float> onCurrencyCountChanged;
    [JsonIgnore]
    public float CurrencyCount
    {
        get
        {
            return currencyCount;
        }
        set
        {
            currencyCount = value;
            onCurrencyCountChanged?.Invoke(currencyCount);
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

    [JsonProperty("attackers")]
    private List<Attacker> attackers;
    [JsonIgnore]
    public Action<List<Attacker>> onAttackersChanged;
    [JsonIgnore]
    public List<Attacker> Attackers
    {
        get
        {
            return attackers;
        }
        set
        {
            attackers = value;
            onAttackersChanged?.Invoke(attackers);
        }
    }
}
