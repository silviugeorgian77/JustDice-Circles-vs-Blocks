using System;
using Newtonsoft.Json;
using UnityEngine;

[Serializable]
public class UpgradeCostFormula
{
    [JsonProperty("x")]
    public float x;

    [JsonProperty("y")]
    public float y;

    /// <summary>
    /// Upgrade formula:
    /// value = <see cref="x">
    ///     * <see cref="y"> ^ <paramref name="upgradeLevel">
    /// </summary>
    public int GetValue(int upgradeLevel)
    {
        return (int)(x * Mathf.Pow(y, upgradeLevel));
    }
}
