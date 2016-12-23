using System;
using System.Linq;

public static class FlagElementFactory
{
    public enum FlagElementType
    {
        DexterBend,
        SinisterBend,
        Border,
        Canton,
        DexterChevron,
        SinisterChevron,
        SymmetricCross,
        NordicCross,
        UpperFess,
        MeridianFess,
        Field,
        DexterPale,
        MeridianPale,
        Quadrisection,
        Saltire
    }

    public static readonly FlagElementType[] FlagElementTypes = Enum.GetValues(typeof(FlagElementType)).Cast<FlagElementType>().ToArray();

    public const float RELATIVE_WIDTH = 0.1f;

    public static FlagElement CreateElement(FlagElementType elementType, params FlagColour[] colours)
    {
        FlagColour primaryColour = colours[0];

        switch (elementType)
        {
            case FlagElementType.DexterBend:
                return new Bend(primaryColour, Bend.Direction.Dexter, RELATIVE_WIDTH);
            case FlagElementType.SinisterBend:
                return new Bend(primaryColour, Bend.Direction.Sinister, RELATIVE_WIDTH);
            case FlagElementType.Border:
                return new Border(primaryColour, RELATIVE_WIDTH);
            case FlagElementType.Canton:
                return new Canton(primaryColour);
            case FlagElementType.DexterChevron:
                return new Chevron(primaryColour, Chevron.Side.Dexter);
            case FlagElementType.SinisterChevron:
                return new Chevron(primaryColour, Chevron.Side.Sinister);
            case FlagElementType.SymmetricCross:
                return new Cross(primaryColour, Cross.Offset.Symmetric, RELATIVE_WIDTH);
            case FlagElementType.NordicCross:
                return new Cross(primaryColour, Cross.Offset.Nordic, RELATIVE_WIDTH);
            case FlagElementType.UpperFess:
                return new Fess(primaryColour, Fess.Section.Upper);
            case FlagElementType.MeridianFess:
                return new Fess(primaryColour, Fess.Section.Meridian);
            case FlagElementType.Field:
                return new Field(primaryColour);
            case FlagElementType.DexterPale:
                return new Pale(primaryColour, Pale.Section.Dexter);
            case FlagElementType.MeridianPale:
                return new Pale(primaryColour, Pale.Section.Meridian);
            case FlagElementType.Quadrisection:
                return new Quadrisection(primaryColour, colours[1]);
            case FlagElementType.Saltire:
                return new Saltire(primaryColour, RELATIVE_WIDTH);

            default:
                return null;
        }
    }
}
