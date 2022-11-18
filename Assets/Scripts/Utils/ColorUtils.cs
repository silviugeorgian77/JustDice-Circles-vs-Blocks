using UnityEngine;

public class ColorUtils
{
    public static Color GetColorFromHex(string colorHex)
    {
        var defaultTileColorHex = colorHex;
        if (!defaultTileColorHex.StartsWith('#'))
        {
            defaultTileColorHex = defaultTileColorHex.Insert(0, "#");
        }
        Color color;
        ColorUtility.TryParseHtmlString(
            defaultTileColorHex,
            out color
        );
        return color;
    }
}
