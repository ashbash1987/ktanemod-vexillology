using System.Collections.Generic;
using UnityEngine;

public sealed class Chevron : FlagElement
{
    public enum Side
    {
        //Left
        Dexter,

        //Right
        Sinister
    }

    public Chevron(FlagColour elementColour, Side elementSide):
        base(elementColour)
    {
        ElementSide = elementSide;
    }

    public readonly Side ElementSide;

    public override void Draw(Texture2D texture)
    {
        List<Texture2DBlock> blocks = new List<Texture2DBlock>(texture.height);

        GenerateBlocksTop(ref blocks, texture.width, texture.height);
        GenerateBlocksBottom(ref blocks, texture.width, texture.height);

        texture.SetPixels32(blocks.ToArray(), ElementColour);
    }

    private void GenerateBlocksTop(ref List<Texture2DBlock> blocks, int textureWidth, int textureHeight)
    {
        float gradient = (textureWidth * 1.0f) / textureHeight;

        switch (ElementSide)
        {
            case Side.Dexter:
                for (int y = 0; y < textureHeight >> 1; ++y)
                {
                    int xMiddle = Mathf.RoundToInt(y * gradient);
                    blocks.Add(new Texture2DBlock(0, y, Mathf.RoundToInt(xMiddle), 1));
                }
                break;

            case Side.Sinister:
                for (int y = 0; y < textureHeight >> 1; ++y)
                {
                    int xMiddle = Mathf.RoundToInt(y * gradient);
                    blocks.Add(new Texture2DBlock(textureWidth - xMiddle, y, xMiddle, 1));
                }
                break;

            default:
                return;
        }
    }

    private void GenerateBlocksBottom(ref List<Texture2DBlock> blocks, int textureWidth, int textureHeight)
    {
        float gradient = (textureWidth * 1.0f) / textureHeight;

        switch (ElementSide)
        {
            case Side.Dexter:
                for (int y = textureHeight >> 1; y < textureHeight; ++y)
                {
                    int xMiddle = Mathf.RoundToInt(((textureHeight - 1) - y) * gradient);
                    blocks.Add(new Texture2DBlock(0, y, xMiddle, 1));
                }
                break;

            case Side.Sinister:
                for (int y = textureHeight >> 1; y < textureHeight; ++y)
                {
                    int xMiddle = Mathf.RoundToInt(((textureHeight - 1) - y) * gradient);
                    blocks.Add(new Texture2DBlock(textureWidth - xMiddle, y, xMiddle, 1));
                }
                break;

            default:
                return;
        }
    }

    public override string ToString()
    {
        return string.Format("A Chevron {0} of {1}", ElementSide, ElementColour);
    }
}
