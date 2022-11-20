using System;
using System.Collections.Generic;
using Newtonsoft.Json;

[Serializable]
public class UserData
{
    [JsonProperty("currencyCount")]
    private int currencyCount;
    [JsonIgnore]
    public Action<int> onCurrencyCountChanged;
    [JsonIgnore]
    public int CurrencyCount
    {
        get
        {
            return currencyCount;
        }
        private set
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

    public Action onReset;
    public void Reset(int attackersCount)
    {
        CurrencyCount = 0;
        UserAttacker = new Attacker()
        {
            UpgradeLevel = 1
        };
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
        onReset?.Invoke();
    }

    public bool AddCurrency(int currencyCount)
    {
        var sum = CurrencyCount + currencyCount;
        if (sum >= 0)
        {
            CurrencyCount = sum;
            return true;
        }
        return false;
    }
}
