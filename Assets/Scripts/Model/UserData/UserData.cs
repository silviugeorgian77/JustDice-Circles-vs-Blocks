using System;
using System.Collections.Generic;

[Serializable]
public class UserData
{
    public int currencyCount;
    public int upgradeLevel;
    public int attackTimeS;
    public List<Attacker> attackers;
}
