using System.Collections;
using UnityEngine;

namespace colorKit
{
    public static class colorCompliments
    {
        public static Color complimentary(Color origColor, colorSpace csToUse)
        {
            switch (csToUse)
            {
                case colorSpace.RGB:
                    return complimentary_inRGB_colorSpace(origColor);
                case colorSpace.RYB:
                    return complimentary_inRYB_colorSpace(origColor);
                default:
                    return complimentary_inCMYK_colorSpace(origColor);
            }
        }

        static Color complimentary_inRGB_colorSpace(Color origColor)
        {
            float[] colorFloat_rGb = colorTypeConversion.color_to_array(origColor);
            float[] color255_rGb = colorFormatConversion._float_to_255(colorFloat_rGb);

            float[] result255_rGb = complimentary(color255_rGb, 255);
            float[] resultFloat_rGb = colorFormatConversion._255_to_float(result255_rGb);

            return colorTypeConversion.array_to_color(resultFloat_rGb);
        }

        static Color complimentary_inRYB_colorSpace(Color origColor)
        {
            float[] colorFloat_rGb = colorTypeConversion.color_to_array(origColor);
            float[] color255_rGb = colorFormatConversion._float_to_255(colorFloat_rGb);
            float[] color255_rYb = rgb2ryb_ryb2rgb.rgb255_to_ryb255(color255_rGb);

            float[] result255_rYb = complimentary(color255_rYb, 255);
            float[] result255_rGb = rgb2ryb_ryb2rgb.ryb255_to_rgb255(result255_rYb);
            float[] resultFloat_rGb = colorFormatConversion._255_to_float(result255_rGb);

            return colorTypeConversion.array_to_color(resultFloat_rGb);
        }

        static Color complimentary_inCMYK_colorSpace(Color origColor)
        {
            float[] colorFloat_rGb = colorTypeConversion.color_to_array(origColor);
            float[] color255_rGb = colorFormatConversion._float_to_255(colorFloat_rGb);
            float[] color255_CMYK = rgb2cmyk_cmyk2rgb.rgb255_to_cmyk255(color255_rGb);

            float[] result255_CMYK = complimentary(color255_CMYK, 255);
            float[] result255_rGb = rgb2cmyk_cmyk2rgb.cmyk255_to_rgb255(result255_CMYK);
            float[] resultFloat_rGb = colorFormatConversion._255_to_float(result255_rGb);

            return colorTypeConversion.array_to_color(resultFloat_rGb);
        }

        //-----BASE

        public static float[] complimentary(float[] color, int floatLimit) //for colors in float format floatLimit = 1 | for colors in 255 format floatLimit = 255
        {
            float[] compColor = new float[color.Length];
            for (int i = 0; i < compColor.Length; i++)
                compColor[i] = floatLimit - color[i];
            return compColor;
        }
    }
}