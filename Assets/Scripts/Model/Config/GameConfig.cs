using System;
using Newtonsoft.Json;

[Serializable]
public class GameConfig
{
    [JsonProperty("maxAttackersCount")]
    public int maxAttackersCount;

    [JsonProperty("attackTimeS")]
    public int attackTimeS;

    [JsonProperty("tapAttackIncomeFormula")]
    public AttackIncomeFormula tapAttackIncomeFormula;

    [JsonProperty("tapUpgradeCostFormula")]
    public UpgradeCostFormula tapUpgradeCostFormula;

    [JsonProperty("attackerCostFormula")]
    public AttackerCostFormula attackerCostFormula;

    [JsonProperty("attackerIncomeFormula")]
    public AttackIncomeFormula attackerIncomeFormula;

    [JsonProperty("attackerUpgradeCostFormula")]
    public UpgradeCostFormula attackerUpgradeCostFormula;
}
