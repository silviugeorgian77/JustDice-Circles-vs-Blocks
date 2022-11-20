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

    [JsonProperty("userAttacker")]
    private Attacker userAttacker;
    [JsonIgnore]
    public Action<Attacker> onUserAttackerChanged;
    [JsonIgnore]
    public Attacker UserAttacker
    {
        get
        {
            return userAttacker;
        }
        set
        {
            userAttacker = value;
            onUserAttackerChanged?.Invoke(userAttacker);
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

    public void Reset(int attackersCount)
    {
        CurrencyCount = 0;
        UserAttacker = new Attacker();
        if (attackers == null || attackers.Count == 0)
        {
            var tempAttackers = new List<Attacker>();
            Attacker attacker;
            for (int i = 0; i < attackersCount; i++)
            {
                attacker = new Attacker()
                {
                    Id = i
                };
                tempAttackers.Add(attacker);
            }
            Attackers = tempAttackers;
        }
        else
        {
            foreach (var attacker in attackers)
            {
                attacker.UpgradeLevel = 0;
            }
        }
    }
}
