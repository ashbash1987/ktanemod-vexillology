using System.Collections.Generic;
using UnityEngine;

public abstract class FlagElement
{
    public FlagElement(FlagColour elementColour)
    {
        ElementColour = elementColour;
    }

    public readonly FlagColour ElementColour;

    public virtual IEnumerable<FlagColour> ElementColours
    {
        get
        {
            yield return ElementColour;
        }
    }

    public abstract void Draw(Texture2D texture);
}
