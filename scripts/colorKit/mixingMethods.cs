using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace colorKit
{
    //NOTE: the mixingMethods dont care about... (1) what color space the colors are NOW in (2) what format the colors are in

    public static class mixingMethods
    {
        //IF (true) --> use Vector4.Distance()... ELSE (false) --> use some other approximation
        static bool useVect4Dist = true; //might be used later

        //-------------------------Originally From Color Mixing-------------------------

        //Ignore Quants == true
        public static float[] mixColors(List<float[]> colors, mixingMethod mm)
        {
            float[] colorQuantities = new float[0]; //create it to meet requirements
            return mixColors(colors, colorQuantities, mm, true);
        }

        //Ignore Quants == false
        public static float[] mixColors(List<float[]> colors, float[] colorQuantities, mixingMethod mm)
        {
            return mixColors(colors, colorQuantities, mm, false);
        }

        //BASE CODE
        static float[] mixColors(List<float[]> colors, float[] colorQuantities, mixingMethod mm, bool ignoreQuants)
        {
            if (
                //COLOR QUANTITIES SHOULD BE POSITIVE... but I WILL NOT CHECK THIS...
                //COLORS SHOULD BE IN THE SAME COLOR SPACE (rgb,ryb,cmyk) -AND- FORMAT (255,float)... but I CANNOT CHECK THIS...
                (colors[0].Length == 3 || colors[0].Length == 4) //colors can only be in these formats
                && (colors.Count > 1) //we need 2 or more colors to MIX them
                && ((ignoreQuants == false) && (colors.Count == colorQuantities.Length)))
            {
                switch (mm)
                {
                    case mixingMethod.spaceAveraging:
                        if (ignoreQuants)
                            return spaceAveraging(colors);
                        else
                            return spaceAveraging(colors, colorQuantities);
                    case mixingMethod.colorComponentAveraging:
                        if (ignoreQuants)
                            return colorComponentAveraging(colors);
                        else
                            return colorComponentAveraging(colors, colorQuantities);
                    case mixingMethod.eachAsPercentOfMax:
                        if (ignoreQuants)
                            return eachAsPercentOfMax(colors);
                        else
                            return eachAsPercentOfMax(colors, colorQuantities);
                    default: //colorAveraging
                        if (ignoreQuants)
                            return colorAveraging(colors);
                        else
                            return colorAveraging(colors, colorQuantities);
                }
            }
            else
            {
                if (colors.Count == 1)
                    return colors[0];
                else
                    return new float[colors[0].Length];
            }
        }

        //-------------------------All Mixing Methods-------------------------

        //----------Tested Mixing Methods (Used in Demo)----------

        //-----Space Averaging

        //Ignore Quants == true
        static float[] spaceAveraging(List<float[]> colors)
        {
            float[] colorQuantities = new float[0]; //create it to meet requirements
            return spaceAveraging(colors, colorQuantities, true);
        }

        //Ignore Quants == false
        static float[] spaceAveraging(List<float[]> colors, float[] colorQuants)
        {
            return spaceAveraging(colors, colorQuants, false);
        }

        //BASE CODE [CHECKED]
        static float[] spaceAveraging(List<float[]> colors, float[] colorQuants, bool ignoreQuants)
        {
            //NOTE: this algorithm is based on space averaging at a CORE so we don't have multiple ways to determine 4D distance
            //so we just stick to Unity's weird 4D distance

            //start with 1 color in the mix (this must happen)
            float[] mixedColor = colors[0];
            float mixedColorQuant = (ignoreQuants) ? 1 : colorQuants[0];

            for (int color = 1; color < colors.Count; color++) //loop through all of our colors and add them to the mix
            {
                //we MIGHT add this newColor to the mix
                float[] newColor = colors[color];
                float newColorQuant = (ignoreQuants) ? 1 : colorQuants[color];

                if(Mathf.Approximately(newColorQuant, 0) == false)  //we have some newColor to add to the mix
                {
                    //calculate new color
                    //NOTE: this does Unity's strange 4D Distance to Lerp (IF your colors have 4 components or are 4D)
                    mixedColor = mixColorsGivenRatios(mixedColor, mixedColorQuant, newColor, newColorQuant);
                    mixedColorQuant += newColorQuant;
                }
            }

            return mixedColor;
        }

        //-----Color Averaging

        //Ignore Quants == true
        static float[] colorAveraging(List<float[]> colors)
        {
            float[] colorQuantities = new float[0]; //create it to meet requirements
            return colorAveraging(colors, colorQuantities, true);
        }

        //Ignore Quants == false
        static float[] colorAveraging(List<float[]> colors, float[] colorQuants)
        {
            return colorAveraging(colors, colorQuants, false);
        }

        //BASE CODE
        static float[] colorAveraging(List<float[]> colors, float[] colorQuants, bool ignoreQuants)
        {
            bool _3DSpace = (colors[0].Length == 3) ? true : false; //might be used later

            float[] mixedColor = new float[colors[0].Length];
            float mixedColorQuant = 0;

            //SUM
            for (int color = 0; color < colors.Count; color++) //loop through all the colors
            {
                float[] newColor = colors[color];
                float newColorQuant = (ignoreQuants) ? 1 : colorQuants[color];

                if (Mathf.Approximately(newColorQuant, 0) == false)
                {
                    for (int component = 0; component < mixedColor.Length; component++) //loop through every component of this particular color
                        mixedColor[component] += (newColor[component] * newColorQuant);

                    mixedColorQuant += newColorQuant;
                }
            }

            //AVERAGE
            for (int i = 0; i < mixedColor.Length; i++)
            {
                if (mixedColorQuant != 0)
                    mixedColor[i] = mixedColor[i] / mixedColorQuant;
                else
                    mixedColor[i] = 0;
            }

            return mixedColor;
        }

        //-----Color Component Averaging

        //Ignore Quants == true
        static float[] colorComponentAveraging(List<float[]> colors)
        {
            float[] colorQuantities = new float[0];
            return colorComponentAveraging(colors, colorQuantities, true);
        }

        //Ignore Quants == false
        static float[] colorComponentAveraging(List<float[]> colors, float[] colorQuantities)
        {
            return colorComponentAveraging(colors, colorQuantities, false);
        }

        //BASE CODE
        static float[] colorComponentAveraging(List<float[]> colors, float[] colorQuants, bool ignoreQuants)
        {
            bool _3DSpace = (colors[0].Length == 3) ? true : false; //might be used later

            float[] mixedColor = new float[colors[0].Length];
            float[] mixedColorCompQuants = new float[colors[0].Length];

            //SUM
            for (int color = 0; color < colors.Count; color++) //loop through all the colors
            {
                float[] newColor = colors[color];
                float newColorQuant = (ignoreQuants) ? 1 : colorQuants[color];

                if(Mathf.Approximately(newColorQuant, 0) == false)
                {
                    for (int component = 0; component < mixedColor.Length; component++) //loop through all the components of this particular color
                    {
                        if (newColor[component] != 0)
                        {
                            mixedColorCompQuants[component] += newColorQuant;
                            mixedColor[component] += (newColor[component] * newColorQuant);
                        }
                    }
                }  
            }

            //AVERAGE
            for (int i = 0; i < mixedColor.Length; i++)
            {
                if (mixedColorCompQuants[i] != 0)
                    mixedColor[i] = mixedColor[i] / mixedColorCompQuants[i];
                else
                    mixedColor[i] = 0;
            }

            return mixedColor;
        }

        //----------Experimental Untested(Unused in Demo)----------

        //Ignore Quants == true
        static float[] eachAsPercentOfMax(List<float[]> colors)
        {
            float[] colorQuantities = new float[0]; //create it to meet requirements
            return eachAsPercentOfMax(colors, colorQuantities, true);
        }

        //Ignore Quants == false
        static float[] eachAsPercentOfMax(List<float[]> colors, float[] colorQuantities)
        {
            return eachAsPercentOfMax(colors, colorQuantities, false);
        }

        //BASE CODE [CHECKED]
        static float[] eachAsPercentOfMax(List<float[]> colors, float[] colorQuantities, bool ignoreQuants)
        {
            float[] mixedColor = new float[colors[0].Length];

            //get total of all the colors
            for (int color = 0; color < colors.Count; color++) //loop through all the colors
            {
                //we MIGHT add this newColor to the mix
                float[] newColor = colors[color];
                float newColorQuant = (ignoreQuants) ? 1 : colorQuantities[color];

                if (Mathf.Approximately(newColorQuant, 0) == false) //we have some newColor to add to the mix
                    for (int comp = 0; comp < newColor.Length; comp++) //loop through all the components of this particular color
                        mixedColor[comp] += (newColor[comp] * newColorQuant);
            }

            // Calculate the max of all sums for each color component
            float maxComponent = 0;
            for (int comp = 0; comp < mixedColor.Length; comp++)
                maxComponent = Mathf.Max(maxComponent, mixedColor[comp]);

            // Now calculate each channel as a percentage of the max
            for (int comp = 0; comp < mixedColor.Length; comp++)
                mixedColor[comp] = Mathf.Floor(mixedColor[comp] / maxComponent * 255);

            return mixedColor;
        }

        //-------------------------the function that makes color quantities affect the final colors-------------------------

        //(tested)
        static float[] mixColorsGivenRatios(float[] color1, float color1Quant, float[] color2, float color2Quant)
        {
            float ratio = 0;
            if (color1Quant != color2Quant)
                ratio = color2Quant / (color1Quant + color2Quant);
            else
                ratio = .5f;
            return colorLerping.colorLerp(color1, color2, ratio);
        }
    }
}