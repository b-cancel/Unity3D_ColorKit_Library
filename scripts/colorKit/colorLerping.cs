using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace colorKit
{
    /// <summary>
    /// These functions are used to lerp between two colors by thinking of them as 2 points in 3 dimensional space. 
    /// All the Colors in the rGb and rYb color space now create a sort of color cube and we simply travel between two points on that cube.
    /// 
    /// NOTE: cmyk has 4 components BUT NOT all of the same type
    /// In essense k is a combination of cmy which is why we cant lerp WELL from color to color within the cmyk color space
    /// EX: black to white -> 255 apart... cyan to magenta -> 255 apart although black and white is on complete ends of the color space spectrum
    /// Eventhough we cant lerp well Im still lerping with the Vector4.Distance && Vector4.Lerp Functions
    /// 
    /// NOTE: I always lerp using the 255 version of the color because since the numbers are larger the errors make less of a difference
    /// </summary>

    public static class colorLerping
    {
        public static Color colorLerp(Color start, Color end, float lerpValue, colorSpace csToUse) //value between 0 and 1
        {
            switch (csToUse)
            {
                case colorSpace.RGB: //NOTE: this works exaclty the same as Color.Lerp()
                    return colorLerp_inRGB_colorSpace(start, end, lerpValue);
                case colorSpace.RYB:
                    return colorLerp_inRYB_colorSpace(start, end, lerpValue);
                default:
                    return colorLerp_inCMYK_colorSpace(start, end, lerpValue);
            }
        }

        //NOTE: this works exaclty the same as Color.Lerp()
        static Color colorLerp_inRGB_colorSpace(Color start, Color end, float lerpValue) //value between 0 and 1
        {
            float[] startFloat_rGb = colorTypeConversion.color_to_array(start);
            float[] start255_rGb = colorFormatConversion._float_to_255(startFloat_rGb);

            float[] endFloat_rGb = colorTypeConversion.color_to_array(end);
            float[] end255_rGb = colorFormatConversion._float_to_255(endFloat_rGb);

            float[] result255_rGb = colorLerp(start255_rGb, end255_rGb, lerpValue);
            float[] resultFloat = colorFormatConversion._255_to_float(result255_rGb);

            return colorTypeConversion.array_to_color(resultFloat);
        }

        static Color colorLerp_inRYB_colorSpace(Color start, Color end, float lerpValue) //value between 0 and 1
        {
            float[] startFloat_rGb = colorTypeConversion.color_to_array(start);
            float[] start255_rGb = colorFormatConversion._float_to_255(startFloat_rGb);
            float[] start255_rYb = rgb2ryb_ryb2rgb.rgb255_to_ryb255(start255_rGb);

            float[] endFloat_rGb = colorTypeConversion.color_to_array(end);
            float[] end255_rGb = colorFormatConversion._float_to_255(endFloat_rGb);
            float[] end255_rYb = rgb2ryb_ryb2rgb.rgb255_to_ryb255(end255_rGb);

            float[] result255_rYb = colorLerp(start255_rYb, end255_rYb, lerpValue);

            float[] result255_rGb = rgb2ryb_ryb2rgb.ryb255_to_rgb255(result255_rYb); //255 rgb
            float[] resultFloat = colorFormatConversion._255_to_float(result255_rGb);

            return colorTypeConversion.array_to_color(resultFloat);
        }

        static Color colorLerp_inCMYK_colorSpace(Color start, Color end, float lerpValue) //value between 0 and 1
        {
            float[] startFloat_rGb = colorTypeConversion.color_to_array(start);
            float[] start255_rGb = colorFormatConversion._float_to_255(startFloat_rGb);
            float[] start255_CMYK = rgb2cmyk_cmyk2rgb.rgb255_to_cmyk255(start255_rGb);

            float[] endFloat_rGb = colorTypeConversion.color_to_array(end);
            float[] end255_rGb = colorFormatConversion._float_to_255(endFloat_rGb);
            float[] end255_CMYK = rgb2cmyk_cmyk2rgb.rgb255_to_cmyk255(end255_rGb);

            float[] result255_CMYK = colorLerp(start255_CMYK, end255_CMYK, lerpValue);

            float[] result255_rGb = rgb2cmyk_cmyk2rgb.cmyk255_to_rgb255(result255_CMYK); //255 rgb
            float[] resultFloat = colorFormatConversion._255_to_float(result255_rGb);

            return colorTypeConversion.array_to_color(resultFloat);
        }

        //-----BASE

        public static float[] colorLerp(float[] start, float[] end, float lerpValue) //value between 0 and 1
        {
            if (start.Length == end.Length)
            {
                lerpValue = Mathf.Clamp01(lerpValue);

                if (start.Length == 3)
                {
                    Vector3 startVect = colorTypeConversion.array_to_vector3(start);
                    Vector3 endVect = colorTypeConversion.array_to_vector3(end);

                    Vector3 resultVect = Vector3.Lerp(startVect, endVect, lerpValue);

                    return colorTypeConversion.vector3_to_array(resultVect);
                }
                else if (start.Length == 4)
                {
                    Vector4 startVect = colorTypeConversion.array_to_vector4(start);
                    Vector4 endVect = colorTypeConversion.array_to_vector4(end);

                    Vector4 resultVect = Vector4.Lerp(startVect, endVect, lerpValue);

                    return colorTypeConversion.vector4_to_array(resultVect);
                }
                else
                    return start;
            }
            else
                return start;
        }
    }
}