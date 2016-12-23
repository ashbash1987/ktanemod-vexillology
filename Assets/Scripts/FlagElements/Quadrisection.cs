using System.Collections.Generic;
using UnityEngine;

public sealed class Quadrisection : FlagElement
{
    public Quadrisection(FlagColour primaryElementColour, FlagColour secondaryElementColour):
        base(primaryElementColour)
    {
        SecondaryElementColour = secondaryElementColour;
    }

    public readonly FlagColour SecondaryElementColour;

    public override IEnumerable<FlagColour> ElementColours
    {
        get
        {
            yield return ElementColour;
            yield return SecondaryElementColour;
        }
    }

    public override void Draw(Texture2D texture)
    {
        int quadrisectionWidth = texture.width >> 1;
        int quadrisectionHeight = texture.height >> 1;

        texture.SetPixels32(quadrisectionWidth, 0, quadrisectionWidth, quadrisectionHeight, ElementColour);
        texture.SetPixels32(0, quadrisectionHeight, quadrisectionWidth, quadrisectionHeight, SecondaryElementColour);
    }

    public override string ToString()
    {
        return string.Format("A Quadrisection of {0} and {1}", ElementColour, SecondaryElementColour);
    }
}
