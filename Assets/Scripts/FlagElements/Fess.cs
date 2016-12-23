using UnityEngine;

public sealed class Fess : FlagElement
{
    public enum Section
    {
        Upper,
        Meridian
    }

    public Fess(FlagColour elementColour, Section elementSection):
        base(elementColour)
    {
        ElementSection = elementSection;
    }

    public readonly Section ElementSection;

    public override void Draw(Texture2D texture)
    {
        int fessHeight = texture.height / 3;

        switch (ElementSection)
        {
            case Section.Upper:
                texture.SetPixels32(0, 0, texture.width, fessHeight, ElementColour);
                break;

            case Section.Meridian:
                texture.SetPixels32(0, fessHeight, texture.width, fessHeight, ElementColour);
                break;

            default:
                return;
        }
    }

    public override string ToString()
    {
        return string.Format("A Fess {0} of {1}", ElementSection.ToString(), ElementColour);
    }
}
