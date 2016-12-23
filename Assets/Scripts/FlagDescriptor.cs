using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public sealed class FlagDescriptor : IEnumerable<FlagElement>
{
    public FlagDescriptor(FlagBuilder flagBuilder)
    {
        _flagElements = new List<FlagElement>();

        foreach(FlagBuilder.FlagPart flagPart in flagBuilder.FlagParts)
        {
            _flagElements.Add(FlagElementFactory.CreateElement(flagPart.flagElement.Value, flagPart.flagColours));
        }
    }

    private List<FlagElement> _flagElements = null;
    public IEnumerable<FlagElement> FlagElements
    {
        get
        {
            return _flagElements;
        }
    }

    public Field Field
    {
        get
        {
            return _flagElements[0] as Field;
        }
    }

    public void Draw(Texture2D texture)
    {
        foreach(FlagElement flagElement in FlagElements)
        {
            flagElement.Draw(texture);
        }

        texture.Apply();
    }

    public override string ToString()
    {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.AppendLine("A flag consisting of:");

        for(int flagElementIndex = 0; flagElementIndex < _flagElements.Count; ++flagElementIndex)
        {
            FlagElement flagElement = _flagElements[flagElementIndex];

            if (flagElementIndex == _flagElements.Count - 1)
            {
                stringBuilder.AppendLine(flagElement.ToString());
            }
            else if (flagElementIndex == _flagElements.Count - 2)
            {
                stringBuilder.Append(flagElement.ToString());
                stringBuilder.AppendLine(" and");
            }
            else
            {
                stringBuilder.Append(flagElement.ToString());
                stringBuilder.AppendLine(",");
            }
        }

        return stringBuilder.ToString();
    }

    public IEnumerator<FlagElement> GetEnumerator()
    {
        return _flagElements.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return _flagElements.GetEnumerator();
    }
}
