using System.Collections.Generic;
using UnityEngine;

public sealed class Saltire : FlagElement
{
    public Saltire(FlagColour elementColour, float elementRelativeWidth):
        base(elementColour)
    {
        ElementRelativeWidth = elementRelativeWidth;
    }

    public readonly float ElementRelativeWidth;

    public override void Draw(Texture2D texture)
    {
        float saltireWidth = texture.height * ElementRelativeWidth;

        List<Texture2DBlock> blocks = new List<Texture2DBlock>(texture.height);

        GenerateBlocksTopLeftBottomRight(ref blocks, saltireWidth, texture.width, texture.height);
        GenerateBlocksBottomLeftTopRight(ref blocks, saltireWidth, texture.width, texture.height);

        texture.SetPixels32(blocks.ToArray(), ElementColour);
    }

    private void GenerateBlocksTopLeftBottomRight(ref List<Texture2DBlock> blocks, float saltireWidth, int textureWidth, int textureHeight)
    {
        float gradient = (textureWidth * 1.0f) / textureHeight;
        float halfBendWidth = saltireWidth * 0.5f;

        float xExtent = Mathf.Sqrt((1 + gradient) * halfBendWidth * halfBendWidth);

        for (int y = 0; y < textureHeight; ++y)
        {
            float xMiddle = y * gradient;
            float xLeft = Mathf.Clamp(xMiddle - xExtent, 0, textureWidth);
            float xRight = Mathf.Clamp(xMiddle + xExtent, 0, textureWidth);
            blocks.Add(new Texture2DBlock(Mathf.RoundToInt(xLeft), y, Mathf.RoundToInt(xRight) - Mathf.RoundToInt(xLeft), 1));
        }
    }

    private void GenerateBlocksBottomLeftTopRight(ref List<Texture2DBlock> blocks, float saltireWidth, int textureWidth, int textureHeight)
    {
        float gradient = (textureWidth * 1.0f) / textureHeight;
        float halfBendWidth = saltireWidth * 0.5f;

        float xExtent = Mathf.Sqrt((1 + gradient) * halfBendWidth * halfBendWidth);

        for (int y = 0; y < textureHeight; ++y)
        {
            float xMiddle = ((textureHeight - 1) - y) * gradient;
            float xLeft = Mathf.Clamp(xMiddle - xExtent, 0, textureWidth);
            float xRight = Mathf.Clamp(xMiddle + xExtent, 0, textureWidth);
            blocks.Add(new Texture2DBlock(Mathf.RoundToInt(xLeft), y, Mathf.RoundToInt(xRight) - Mathf.RoundToInt(xLeft), 1));
        }
    }

    public override string ToString()
    {
        return string.Format("A Saltire of {0}", ElementColour);
    }
}
