using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using FlagElementType = FlagElementFactory.FlagElementType;

public sealed class FlagBuilder
{
    public class FlagPart
    {
        public FlagElementType? flagElement;
        public FlagColour[] flagColours;
    }

    private const int FLAG_PART_COUNT = 4;

    public FlagPart[] FlagParts
    {
        get;
        private set;
    }

    private readonly FlagNation _nation = null;

    private List<FlagElementType> _remainingFlagElementOptions = FlagElementFactory.FlagElementTypes.ToList();
    private List<FlagColour> _remainingFlagColourOptions = FlagColour.FlagColours.ToList();

    private bool _twoColoursMet = false;

    public FlagBuilder(FlagNation nation)
    {
        _nation = nation;

        FlagParts = new FlagPart[FLAG_PART_COUNT];
        for(int flagPartIndex = 0; flagPartIndex < FLAG_PART_COUNT; ++flagPartIndex)
        {
            FlagParts[flagPartIndex] = new FlagPart();
        }

        //First element is always a field
        SetFlagPartElement(FlagParts[0], FlagElementType.Field);

        //Do nation-based pre-requisites first
        if (nation != null)
        {
            PrepopulateForNation();
        }

        //Then populate the field
        PopulateField();

        //Then populate the remaining elements
        PopulateRemainingElements();

        //Finally populate the remaining colours
        PopulateRemainingColours();

        //Correct any quadrisections
        CorrectQuadrisections();
    }

    private void PrepopulateForNation()
    {
        //Do required elements
        if (_nation.RequiredElements != null)
        {
            List<FlagElementType> flagElements = _nation.RequiredElements.ToList();
            for (int requiredElementCount = 0; requiredElementCount < _nation.RequiredElementCount; ++requiredElementCount)
            {
                FlagElementType flagElement = flagElements.RandomPick();
                flagElements.Remove(flagElement);

                switch (flagElement)
                {
                    case FlagElementType.Quadrisection:
                    case FlagElementType.Canton:
                        for (int possibleFlagPartIndex = 2; possibleFlagPartIndex < FLAG_PART_COUNT; ++possibleFlagPartIndex)
                        {
                            FlagPart flagPart = FlagParts[possibleFlagPartIndex];
                            if (!flagPart.flagElement.HasValue)
                            {
                                SetFlagPartElement(flagPart, flagElement);
                            }
                        }
                        break;
                    default:
                        SetFlagPartElement(PickFreePart(true, false), flagElement);
                        break;
                }

            }
        }

        //Do required colours
        if (_nation.RequiredColours != null)
        {
            foreach (FlagColour flagColour in _nation.RequiredColours)
            {
                SetFlagPartColours(PickFreePart(false, true), new FlagColour[] { flagColour });
            }
        }

        //Remove disallowed elements
        if (_nation.DisallowedElements != null)
        {
            foreach (FlagElementType flagElement in _nation.DisallowedElements)
            {
                _remainingFlagElementOptions.Remove(flagElement);
            }
        }

        //Remove disallowed colours
        if (_nation.DisallowedColours != null)
        {
            foreach (FlagColour flagColour in _nation.DisallowedColours)
            {
                _remainingFlagColourOptions.Remove(flagColour);
            }
        }

        //Do colour dependencies 
        if (_nation.ColourDependency.HasValue)
        {
            if (Random.Range(0.0f, 1.0f) <= 0.3f)
            {
                SetFlagPartColours(PickFreePart(false, true), new FlagColour[] { _nation.ColourDependency.Value.Key });
                SetFlagPartColours(PickFreePart(false, true), new FlagColour[] { _nation.ColourDependency.Value.Value });
            }
            else
            {
                _remainingFlagColourOptions.Remove(_nation.ColourDependency.Value.Key);
            }
        }
    }

    private void PopulateField()
    {
        //As declared previously, first element is always a field

        FlagPart flagPart = FlagParts[0];

        //First flag part is already fully defined, nothing to do here
        if (flagPart.flagElement.HasValue && flagPart.flagColours != null)
        {
            return;
        }

        //Check to see if any canton, pale or fess elements have been declared, and with any colours, since field cannot share colours with these elements
        IEnumerable<FlagPart> cantonPaleFessFlagPart = FlagParts.Where((x) => x.flagElement != null &&
                                                                             (x.flagElement.Value == FlagElementType.Canton ||
                                                                              x.flagElement.Value == FlagElementType.DexterPale ||
                                                                              x.flagElement.Value == FlagElementType.MeridianPale ||
                                                                              x.flagElement.Value == FlagElementType.UpperFess ||
                                                                              x.flagElement.Value == FlagElementType.MeridianFess));
        IEnumerable<FlagColour> cantonPaleFessFlagColours = cantonPaleFessFlagPart.Where((x) => x.flagColours != null).SelectMany((x) => x.flagColours);

        IEnumerable<FlagColour> flagColourOptions = _remainingFlagColourOptions.Except(cantonPaleFessFlagColours);

        //Get the second flag colours, since field cannot share colours with the second flag part
        FlagColour[] secondFlagColours = FlagParts[1].flagColours;

        if (secondFlagColours != null)
        {
            flagColourOptions = flagColourOptions.Except(secondFlagColours);
        }

        //Check to see if a disallowed field colour has been declared in the nation
        if (_nation != null && _nation.DisallowedFieldColour != null)
        {
            flagColourOptions = flagColourOptions.Except(new FlagColour[] { _nation.DisallowedFieldColour });
        }

        //Set the field colour based on a random pick of the remaining available options
        SetFlagPartColours(flagPart, new FlagColour[] { flagColourOptions.RandomPick() });
    }

    private void PopulateRemainingElements()
    {
        //Go through all the remaining flag parts and ensure they are populated with flag elements
        for(int flagPartIndex = 1; flagPartIndex < FLAG_PART_COUNT; ++flagPartIndex)
        {
            FlagPart flagPart = FlagParts[flagPartIndex];
            if (flagPart.flagElement.HasValue)
            {
                continue;
            }

            IEnumerable<FlagElementType> flagElementOptions = _remainingFlagElementOptions;

            //To be sure that cantons and quadrisections cannot be placed into vulnerable positions
            if (flagPartIndex == 1)
            {
                flagElementOptions = flagElementOptions.Except(new FlagElementType[] { FlagElementType.Quadrisection, FlagElementType.Canton });
            }

            //If a colour is already pre-defined in this flag part, and it shares the colour of the field, then the element cannot be a canton, pale or fess
            if (flagPart.flagColours != null && flagPart.flagColours.Union(FlagParts[0].flagColours).Any())
            {
                SetFlagPartElement(flagPart, flagElementOptions.Except(new FlagElementType[] { FlagElementType.Canton, FlagElementType.DexterPale, FlagElementType.MeridianPale, FlagElementType.UpperFess, FlagElementType.MeridianFess }).RandomPick());
            }
            //Otherwise, just pick from all remaining options
            else
            {
                SetFlagPartElement(flagPart, flagElementOptions.RandomPick());
            }
        }
    }

    private void PopulateRemainingColours()
    {
        //Go through all the remaining flag parts and ensure they are populated with flag colours
        for (int flagPartIndex = 1; flagPartIndex < FLAG_PART_COUNT; ++flagPartIndex)
        {
            FlagPart flagPart = FlagParts[flagPartIndex];
            if (flagPart.flagColours != null)
            {
                continue;
            }

            //If this is the second flag part, or the pre-defined flag element is a canton, pale or fess, then this colour cannot be the same as the field colour
            if (flagPartIndex == 1 ||
                flagPart.flagElement.Value == FlagElementType.Canton ||
                flagPart.flagElement.Value == FlagElementType.DexterPale ||
                flagPart.flagElement.Value == FlagElementType.MeridianPale ||
                flagPart.flagElement.Value == FlagElementType.UpperFess ||
                flagPart.flagElement.Value == FlagElementType.MeridianFess)
            {
                SetFlagPartColours(flagPart, new FlagColour[] { _remainingFlagColourOptions.Except(FlagParts[0].flagColours).RandomPick() });
            }
            //Otherwise, just pick from all remaining options
            else
            {
                SetFlagPartColours(flagPart, new FlagColour[] { _remainingFlagColourOptions.RandomPick() });
            }
        }
    }

    private void CorrectQuadrisections()
    {
        //All quadrisections must have two colours defined; all processes above only generate one colour per array

        foreach(FlagPart flagPart in FlagParts.Where((x) => x.flagElement.Value == FlagElementType.Quadrisection))
        {
            //A quadrisection must have at least one colour different to the field colour
            if (flagPart.flagColours.Union(FlagParts[0].flagColours).Any())
            {
                FlagColour otherColour = _remainingFlagColourOptions.Except(flagPart.flagColours).RandomPick();
                flagPart.flagColours = new FlagColour[] { otherColour, flagPart.flagColours[0] };
            }
            //A quadrisection can use the same colour twice; make this a randomised choice
            else if (Random.Range(0.0f, 1.0f) <= 0.5f)
            {
                FlagColour otherColour = _remainingFlagColourOptions.Except(flagPart.flagColours).RandomPick();
                flagPart.flagColours = new FlagColour[] { otherColour, flagPart.flagColours[0] };
            }
            else
            {
                flagPart.flagColours = new FlagColour[] { flagPart.flagColours[0], flagPart.flagColours[0] };
            }
        }
    }

    private FlagPart PickFreePart(bool freeElement, bool freeColour)
    {
        return FlagParts.Where((x) => (freeElement && !x.flagElement.HasValue) || (freeColour && x.flagColours == null)).RandomPick();
    }

    private bool SetFlagPartElement(FlagPart flagPart, FlagElementType flagElement)
    {
        //Don't add the element if it's not an available option
        if (!_remainingFlagElementOptions.Contains(flagElement))
        {
            return false;
        }

        flagPart.flagElement = flagElement;

        //Elements are unique and cannot be used more than once each
        _remainingFlagElementOptions.Remove(flagElement);

        //Also, bends & saltires cannot co-exist
        switch (flagElement)
        {
            case FlagElementType.DexterBend:
            case FlagElementType.SinisterBend:
            case FlagElementType.Saltire:
                _remainingFlagElementOptions.Remove(FlagElementType.DexterBend);
                _remainingFlagElementOptions.Remove(FlagElementType.SinisterBend);
                _remainingFlagElementOptions.Remove(FlagElementType.Saltire);
                break;

            default:
                break;
        }

        return true;
    }

    private bool SetFlagPartColours(FlagPart flagPart, FlagColour[] colours)
    {
        //Don't add the colour selection if i's not an available option
        foreach(FlagColour colour in colours)
        {
            if (!_remainingFlagColourOptions.Contains(colour))
            {
                return false;
            }
        }

        flagPart.flagColours = colours;

        //Only 2 flag elements can share the same colour
        if (_twoColoursMet)
        {
            foreach (FlagColour colour in colours)
            {
                _remainingFlagColourOptions.Remove(colour);
            }
        }
        else
        {            
            foreach (FlagColour colour in colours)
            {
                if (FlagParts.Count((x) => x.flagColours != null && x.flagColours.Contains(colour)) == 2)
                {
                    _twoColoursMet = true;
                    break;                    
                }
            }

            if (_twoColoursMet)
            {
                foreach(FlagColour colour in FlagParts.Where((x) => x.flagColours != null).SelectMany((x) => x.flagColours))
                {
                    _remainingFlagColourOptions.Remove(colour);
                }
            }
        }

        return true;
    }
}
