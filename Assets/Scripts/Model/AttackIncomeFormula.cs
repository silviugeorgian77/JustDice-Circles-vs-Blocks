using System;
using UnityEngine;

[Serializable]
public class AttackIncomeFormula
{
    public float x;
    public float y;

    /// <summary>
    /// Upgrade formula:
    /// value = <see cref="x">
    ///     * <paramref name="upgradeLevel"> ^ <see cref="y">
    /// </summary>
    public float GetValue(float upgradeLevel)
    {
        return x * Mathf.Pow(upgradeLevel, y);
    }
}
