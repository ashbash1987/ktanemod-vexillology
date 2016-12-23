using UnityEngine;

public sealed class Canton : FlagElement
{
    public Canton(FlagColour elementColour):
        base(elementColour)
    {
    }

    public override void Draw(Texture2D texture)
    {
        texture.SetPixels32(0, 0, texture.width >> 1, texture.height >> 1, ElementColour);
    }

    public override string ToString()
    {
        return string.Format("A Canton of {0}", ElementColour);
    }
}
