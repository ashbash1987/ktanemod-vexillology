using UnityEngine;

public sealed class Cross : FlagElement
{
    public enum Offset
    {
        Nordic,
        Symmetric
    }

    public Cross(FlagColour elementColour, Offset elementOffset, float elementRelativeWidth):
        base(elementColour)
    {
        ElementOffset = elementOffset;
        ElementRelativeWidth = elementRelativeWidth;
    }

    public readonly Offset ElementOffset;
    public readonly float ElementRelativeWidth;

    public override void Draw(Texture2D texture)
    {
        int halfTextureWidth = texture.width >> 1;
        int halfTextureHeight = texture.height >> 1;

        int halfCrossWidth = Mathf.RoundToInt(texture.height * ElementRelativeWidth * 0.5f);
        int crossWidth = halfCrossWidth << 1;

        int crossXStart = 0;

        int crossYStart = halfTextureHeight - halfCrossWidth;
        int crossYEnd = halfTextureHeight + halfCrossWidth;

        switch (ElementOffset)
        {
            case Offset.Nordic:
                int quarterTextureWidth = halfTextureWidth >> 1;
                crossXStart = quarterTextureWidth - halfCrossWidth;
                break;

            case Offset.Symmetric:
                crossXStart = halfTextureWidth - halfCrossWidth;
                break;

            default:
                return;
        }

        texture.SetPixels32(new Texture2DBlock[]
        {
            new Texture2DBlock(0, crossYStart, texture.width, crossWidth),
            new Texture2DBlock(crossXStart, 0, crossWidth, crossYStart),
            new Texture2DBlock(crossXStart, crossYEnd, crossWidth, crossYStart)
        }, ElementColour);
    }

    public override string ToString()
    {
        return string.Format("A {0} Cross of {1}", ElementOffset, ElementColour);
    }
}
