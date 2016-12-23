using System.Collections.Generic;
using UnityEngine;

public sealed class Bend : FlagElement
{
    public enum Direction
    {
        //Dexter is top-left to bottom-right
        Dexter,

        //Sinister is from top-right to bottom-left
        Sinister
    }

    public Bend(FlagColour elementColour, Direction elementDirection, float elementRelativeWidth):
        base(elementColour)
    {
        ElementDirection = elementDirection;
        ElementRelativeWidth = elementRelativeWidth;
    }

    public readonly Direction ElementDirection;
    public readonly float ElementRelativeWidth;

    public override void Draw(Texture2D texture)
    {
        float bendWidth = texture.height * ElementRelativeWidth;

        List<Texture2DBlock> blocks = new List<Texture2DBlock>(texture.height);

        switch (ElementDirection)
        {
            case Direction.Dexter:
                GenerateBlocksTopLeftBottomRight(ref blocks, bendWidth, texture.width, texture.height);
                break;

            case Direction.Sinister:
                GenerateBlocksBottomLeftTopRight(ref blocks, bendWidth, texture.width, texture.height);
                break;

            default:
                return;
        }

        texture.SetPixels32(blocks.ToArray(), ElementColour);
    }

    private void GenerateBlocksTopLeftBottomRight(ref List<Texture2DBlock> blocks, float bendWidth, int textureWidth, int textureHeight)
    {
        float gradient = (textureWidth * 1.0f) / textureHeight;
        float halfBendWidth = bendWidth * 0.5f;

        float xExtent = Mathf.Sqrt((1 + gradient) * halfBendWidth * halfBendWidth);

        for (int y = 0; y < textureHeight; ++y)
        {
            float xMiddle = y * gradient;
            float xLeft = Mathf.Clamp(xMiddle - xExtent, 0, textureWidth);
            float xRight = Mathf.Clamp(xMiddle + xExtent, 0, textureWidth);
            blocks.Add(new Texture2DBlock(Mathf.RoundToInt(xLeft), y, Mathf.RoundToInt(xRight) - Mathf.RoundToInt(xLeft), 1));
        }
    }

    private void GenerateBlocksBottomLeftTopRight(ref List<Texture2DBlock> blocks, float bendWidth, int textureWidth, int textureHeight)
    {
        float gradient = (textureWidth * 1.0f) / textureHeight;
        float halfBendWidth = bendWidth * 0.5f;

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
        return string.Format("A Bend {0} of {1}", ElementDirection, ElementColour);
    }
}
