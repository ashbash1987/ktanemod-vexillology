using UnityEngine;
using System.Linq;
using System;

public sealed class Texture2DBlock : IComparable<Texture2DBlock>
{
    public readonly int X;
    public readonly int Y;
    public readonly int BlockWidth;
    public readonly int BlockHeight;

    public Texture2DBlock(int x, int y, int blockWidth, int blockHeight)
    {
        X = x;
        Y = y;
        BlockWidth = blockWidth;
        BlockHeight = blockHeight;
    }

    public int CompareTo(Texture2DBlock other)
    {
        int thisSize = BlockWidth * BlockHeight;
        int otherSize = other.BlockWidth * other.BlockHeight;

        if (thisSize > otherSize)
        {
            return 1;
        }
        else if (thisSize < otherSize)
        {
            return -1;
        }

        return 0;
    }
}

public static class Texture2DExtensions
{
    public static void SetPixels32(this Texture2D texture2D, Color32 color, int mipLevel = 0)
    {
        texture2D.SetPixels32(CreateColorArray(texture2D.width * texture2D.height, color), mipLevel);
    }

    public static void SetPixels32(this Texture2D texture2D, int x, int y, int blockWidth, int blockHeight, Color32 color, int mipLevel = 0)
    {
        texture2D.SetPixels32(x, y, blockWidth, blockHeight, CreateColorArray(blockWidth * blockHeight, color), mipLevel);
    }

    public static void SetPixels32(this Texture2D texture2D, Texture2DBlock[] blocks, Color32 color, int mipLevel = 0)
    {
        Texture2DBlock maxBlock = blocks.Max();
        Color32[] colorArray = CreateColorArray(maxBlock.BlockWidth * maxBlock.BlockHeight, color);

        foreach(Texture2DBlock block in blocks)
        {
            texture2D.SetPixels32(block.X, block.Y, block.BlockWidth, block.BlockHeight, colorArray, mipLevel);
        }
    }

    private static Color32[] CreateColorArray(int arraySize, Color32 color)
    {
        Color32[] newColors = new Color32[arraySize];
        for (int texelIndex = 0; texelIndex < arraySize; ++texelIndex)
        {
            newColors[texelIndex] = color;
        }

        return newColors;
    }
}