using UnityEngine;

public sealed class Border : FlagElement
{
    public Border(FlagColour elementColour, float elementRelativeWidth):
        base(elementColour)
    {
        ElementRelativeWidth = elementRelativeWidth;
    }

    public readonly float ElementRelativeWidth;

    public override void Draw(Texture2D texture)
    {
        int borderWidth = Mathf.RoundToInt(texture.height * ElementRelativeWidth);

        texture.SetPixels32(new Texture2DBlock[]
        {
            new Texture2DBlock(0, 0, texture.width, borderWidth),
            new Texture2DBlock(0, texture.height - borderWidth, texture.width, borderWidth),
            new Texture2DBlock(0, borderWidth, borderWidth, texture.height - (borderWidth * 2)),
            new Texture2DBlock(texture.width - borderWidth, borderWidth, borderWidth, texture.height - (borderWidth * 2)),
        }, ElementColour);
    }

    public override string ToString()
    {
        return string.Format("A Border of {0}", ElementColour);
    }
}
