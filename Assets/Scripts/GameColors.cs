using UnityEngine;

public static class GameColors
{
    // This class might not even be used because it might be easier to just select the tiles or just make different prefabs for the different colors

    public enum GameColor
    {
        GroundLight,
        GroundMedium,
        GroundMedium2,
        GroundDark
    }

    public static Color32 groundLight { get; private set; } = new Color32(0xcd, 0xe6, 0xf5, 0xff); // #CDE6F5
    public static Color32 groundMedium { get; private set; } = new Color32(0x8d, 0xa7, 0xbe, 0xff); // #8DA7BE
    public static Color32 groundMedium2 { get; private set; } = new Color32(0x87, 0x91, 0x9e, 0xff); // #87919E
    public static Color32 groundDark { get; private set; } = new Color32(0x17, 0x21, 0x21, 0xff); // #172121

    public static Color32 GetColorFromEnum(GameColor gameColor)
    {
        switch (gameColor)
        {
            case GameColor.GroundLight:
                return groundLight;
            case GameColor.GroundMedium:
                return groundMedium;
            case GameColor.GroundMedium2:
                return groundMedium2;
            case GameColor.GroundDark:
                return groundDark;
        }

        return new Color32(0xff, 0xff, 0xff, 0xff);
    }
}
