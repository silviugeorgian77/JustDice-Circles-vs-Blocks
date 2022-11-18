using System;

[Serializable]
public class AttackerCostFormula
{
    public float initCost;
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
