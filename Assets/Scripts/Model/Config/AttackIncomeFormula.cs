using System;
using Newtonsoft.Json;
using UnityEngine;

[Serializable]
public class AttackIncomeFormula
{
    [JsonProperty("x")]
    public float x;

    [JsonProperty("y")]
    public float y;

    /// <summary>
    /// Income formula:
    /// value = <see cref="x">
    ///     * <paramref name="upgradeLevel"> ^ <see cref="y">
    /// </summary>
    public int GetValue(int upgradeLevel)
    {
        return (int)(x * Mathf.Pow(upgradeLevel + 1, y));
    }
}
