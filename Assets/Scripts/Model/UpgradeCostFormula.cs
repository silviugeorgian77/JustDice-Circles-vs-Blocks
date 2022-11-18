using System;
using UnityEngine;

[Serializable]
public class UpgradeCostFormula
{
    public float x;
    public float y;

    /// <summary>
    /// Upgrade formula:
    /// value = <see cref="x">
    ///     * <see cref="y"> ^ <paramref name="upgradeLevel">
    /// </summary>
    public float GetValue(float upgradeLevel)
    {
        return x * Mathf.Pow(y, upgradeLevel);
    }
}
