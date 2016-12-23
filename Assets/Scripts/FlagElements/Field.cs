using UnityEngine;

public sealed class Field : FlagElement
{
    public Field(FlagColour elementColour):
        base(elementColour)
    {
    }

    public override void Draw(Texture2D texture)
    {
        texture.SetPixels32(ElementColour);
    }

    public override string ToString()
    {
        return string.Format("A Field of {0}", ElementColour);
    }
}
