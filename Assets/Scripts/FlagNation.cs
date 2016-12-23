using FlagElementType = FlagElementFactory.FlagElementType;

using ColourDependency = System.Collections.Generic.KeyValuePair<FlagColour, FlagColour>;

public sealed class FlagNation
{
    public static readonly FlagNation Nobelia = new FlagNation()
    {
        FullName = "The Monarchy of Nobelia",
        DisplayName = "Nobelia",
        RequiredColours = new FlagColour[] { FlagColour.Burgundy },
        RequiredElements = new FlagElementType[] { FlagElementType.SymmetricCross, FlagElementType.Border },
        RequiredElementCount = 2,
        DisallowedColours = new FlagColour[] { FlagColour.Red, FlagColour.Cyan },
        DisallowedFieldColour = FlagColour.Green,
        ColourDependency = new ColourDependency(FlagColour.Blue, FlagColour.White),
    };

    public static readonly FlagNation Picri = new FlagNation()
    {
        FullName = "The Collective Micronations of Picri",
        DisplayName = "Picri",
        RequiredColours = new FlagColour[] { FlagColour.Orange },
        RequiredElements = new FlagElementType[] { FlagElementType.Quadrisection, FlagElementType.Canton },
        RequiredElementCount = 1,
        DisallowedColours = new FlagColour[] { FlagColour.Black, FlagColour.Burgundy },
        DisallowedFieldColour = FlagColour.Red,
        ColourDependency = new ColourDependency(FlagColour.Yellow, FlagColour.Green),
    };

    public static readonly FlagNation Katain = new FlagNation()
    {
        FullName = "Federal Republic of Katain",
        DisplayName = "Katain",
        RequiredColours = new FlagColour[] { FlagColour.Yellow },
        RequiredElements = new FlagElementType[] { FlagElementType.SinisterChevron, FlagElementType.DexterPale },
        RequiredElementCount = 1,
        DisallowedFieldColour = FlagColour.Black,
        ColourDependency = new ColourDependency(FlagColour.Red, FlagColour.Cyan),
    };

    public static readonly FlagNation Litbobuania = new FlagNation()
    {
        FullName = "Nation of Litbobuania",
        DisplayName = "Litbobuania",
        RequiredColours = new FlagColour[] { FlagColour.Green },
        RequiredElements = new FlagElementType[] { FlagElementType.UpperFess, FlagElementType.MeridianFess },
        RequiredElementCount = 2,
        DisallowedColours = new FlagColour[] { FlagColour.Orange },
        DisallowedFieldColour = FlagColour.Blue,
    };

    public static readonly FlagNation Tikthok = new FlagNation()
    {
        FullName = "The Sultanat of Tikthok",
        DisplayName = "Tikthok",
        RequiredColours = new FlagColour[] { FlagColour.White },
        RequiredElements = new FlagElementType[] { FlagElementType.NordicCross },
        RequiredElementCount = 1,
        DisallowedFieldColour = FlagColour.Cyan,
    };

    public static readonly FlagNation Bomjikizstan = new FlagNation()
    {
        FullName = "The People's Republic of Bomjikizstan",
        DisplayName = "Bomjikizstan",
        RequiredColours = new FlagColour[] { FlagColour.Black },
        RequiredElements = new FlagElementType[] { FlagElementType.SymmetricCross, FlagElementType.DexterBend, FlagElementType.SinisterBend },
        RequiredElementCount = 1,
        DisallowedColours = new FlagColour[] { FlagColour.Blue, FlagColour.Green, FlagColour.Yellow },
        DisallowedFieldColour = FlagColour.Orange,
    };

    public static readonly FlagNation Shkval = new FlagNation()
    {
        FullName = "The City-state of Shkval",
        DisplayName = "Shkval",
        RequiredColours = new FlagColour[] { FlagColour.Blue },
        RequiredElements = new FlagElementType[] { FlagElementType.NordicCross, FlagElementType.MeridianFess },
        RequiredElementCount = 2,
        ColourDependency = new ColourDependency(FlagColour.Red, FlagColour.Black),
    };

    public static readonly FlagNation Tzarmenia = new FlagNation()
    {
        FullName = "The Socialist Nation of Tzarmenia",
        DisplayName = "Tzarmenia",
        RequiredColours = new FlagColour[] { FlagColour.Red, FlagColour.Yellow },
        RequiredElements = new FlagElementType[] { FlagElementType.Saltire, FlagElementType.DexterChevron },
        RequiredElementCount = 1,
        DisallowedColours = new FlagColour[] { FlagColour.Green },
        DisallowedFieldColour = FlagColour.Burgundy,
        ColourDependency = new ColourDependency(FlagColour.White, FlagColour.Black),
    };

    public static readonly FlagNation Glyzikhistan = new FlagNation()
    {
        FullName = "The Republic of Glyzikhistan",
        DisplayName = "Glyzikhistan",
        RequiredColours = new FlagColour[] { FlagColour.Cyan },
        RequiredElements = new FlagElementType[] { FlagElementType.DexterPale, FlagElementType.MeridianPale },
        RequiredElementCount = 1,
        DisallowedElements = new FlagElementType[] { FlagElementType.SymmetricCross, FlagElementType.NordicCross, FlagElementType.Saltire },
    };

    public static readonly FlagNation[] Nations = new FlagNation[]
    {
        Nobelia,
        Picri,
        Katain,
        Litbobuania,
        Tikthok,
        Bomjikizstan,
        Shkval,
        Tzarmenia,
        Glyzikhistan
    };

    public string FullName;
    public string DisplayName;

    public FlagColour[] RequiredColours;
    public FlagColour[] DisallowedColours;

    public FlagElementType[] RequiredElements;
    public FlagElementType[] DisallowedElements;

    public int RequiredElementCount;

    public ColourDependency? ColourDependency;

    public FlagColour DisallowedFieldColour;
}
