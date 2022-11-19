using System;
using Newtonsoft.Json;

[Serializable]
public class AttackerCostFormula
{
    [JsonProperty("initCost")]
    public float initCost;

    [JsonProperty("x")]
    public float x;

    /// <summary>
    /// Cost formula:
    /// value = <see cref="x"> * <paramref name="initCost">
    /// </summary>
    public float GetValue()
    {
        return x * initCost;
    }
}
