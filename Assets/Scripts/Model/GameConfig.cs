using System;

[Serializable]
public class GameConfig
{
    public AttackIncomeFormula tapAttackIncomeFormula;
    public UpgradeCostFormula tapUpgradeCostFormula;

    public AttackIncomeFormula friendAttackerIncomeFormula;
    public UpgradeCostFormula friendAttackerCostFormula;
}
