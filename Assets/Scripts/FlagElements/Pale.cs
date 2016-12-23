using UnityEngine;

public sealed class Pale : FlagElement
{
    public enum Section
    {
        Dexter,
        Meridian
    }

    public Pale(FlagColour elementColour, Section elementSection):
        base(elementColour)
    {
        ElementSection = elementSection;
    }

    public readonly Section ElementSection;

    public override void Draw(Texture2D texture)
    {
        int paleWidth = texture.width / 3;

        switch (ElementSection)
        {
            case Section.Dexter:
                texture.SetPixels32(0, 0, paleWidth, texture.height, ElementColour);
                break;

            case Section.Meridian:
                texture.SetPixels32(paleWidth, 0, paleWidth, texture.height, ElementColour);
                break;

            default:
                return;
        }
    }

    public override string ToString()
    {
        return string.Format("A Pale {0} of {1}", ElementSection.ToString(), ElementColour);
    }
}
