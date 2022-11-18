using System;

[Serializable]
public class GameConfig
{
    public int maxAttackersCount;

    public AttackIncomeFormula tapAttackIncomeFormula;
    public UpgradeCostFormula tapUpgradeCostFormula;

    public AttackerCostFormula attackerCostFormula;

    public AttackIncomeFormula attackerIncomeFormula;
    public UpgradeCostFormula attackerUpgradeCostFormula;
}
