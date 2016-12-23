using System.Linq;
using UnityEngine;

public sealed class FlagColour
{
    public static readonly FlagColour Black = new FlagColour("Black", new Color32(0, 0, 0, 255));
    public static readonly FlagColour White = new FlagColour("White", new Color32(255, 255, 255, 255));
    public static readonly FlagColour Red = new FlagColour("Red", new Color32(237, 32, 27, 255));
    public static readonly FlagColour Blue = new FlagColour("Blue", new Color32(0, 56, 168, 255));
    public static readonly FlagColour Green = new FlagColour("Green", new Color32(11, 146, 70, 255));
    public static readonly FlagColour Yellow = new FlagColour("Yellow", new Color32(249, 236, 36, 255));
    public static readonly FlagColour Orange = new FlagColour("Orange", new Color32(247, 150, 55, 255));
    public static readonly FlagColour Cyan = new FlagColour("Cyan", new Color32(0, 163, 221, 255));
    public static readonly FlagColour Burgundy = new FlagColour("Burgundy", new Color32(123, 20, 65, 255));

    public static readonly FlagColour[] FlagColours = new FlagColour[]
    {
        Black,
        White,
        Red,
        Blue,
        Green,
        Yellow,
        Orange,
        Cyan,
        Burgundy
    };

    private FlagColour(string colourName, Color32 trueColour)
    {
        ColourName = colourName;
        TrueColour = trueColour;
    }

    public readonly string ColourName;
    public readonly Color32 TrueColour;

    public override string ToString()
    {
        return ColourName;
    }

    public static implicit operator Color32(FlagColour flagColour)
    {
        return flagColour.TrueColour;
    }
}
