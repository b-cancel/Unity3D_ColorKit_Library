using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace colorKit
{
    public static class colorMixing
    {
        //Ignore Quants == true
        public static Color mixColors(Color[] colors, colorSpace csToUse, mixingMethod mm)
        {
            float[] colorQuantities = new float[0]; //create it to meet requirements
            return mixColors(colors, colorQuantities, csToUse, mm, true);
        }

        //Ignore Quants == false
        public static Color mixColors(Color[] colors, float[] colorQuantities, colorSpace csToUse, mixingMethod mm)
        {
            return mixColors(colors, colorQuantities, csToUse, mm, false);
        }

        static Color mixColors(Color[] colors, float[] colorQuantities, colorSpace csToUse, mixingMethod mm, bool ignoreQuants)
        {
            switch (csToUse)
            {
                case colorSpace.RGB:
                    return mixColors_inRGB_colorSpace(colors, colorQuantities, mm, ignoreQuants);
                case colorSpace.RYB:
                    return mixColors_inRYB_colorSpace(colors, colorQuantities, mm, ignoreQuants);
                default:
                    return mixColors_inCMYK_colorSpace(colors, colorQuantities, mm, ignoreQuants);
            }
        }

        static Color mixColors_inRGB_colorSpace(Color[] colors, float[] colorQuantities, mixingMethod mm, bool ignoreQuants)
        {
            List<float[]> passedColors = new List<float[]>();

            for (int i = 0; i < colors.Length; i++)
            {
                float[] colorFloat_RGB = colorTypeConversion.color_to_array(colors[i]);
                float[] color255_RGB = colorFormatConversion._float_to_255(colorFloat_RGB);
                passedColors.Add(color255_RGB);
            }

            float[] result255_RGB = (ignoreQuants == false) ? mixingMethods.mixColors(passedColors, colorQuantities, mm) : mixingMethods.mixColors(passedColors, mm); //actual Color Mixing

            float[] resultFloat_RGB = colorFormatConversion._255_to_float(result255_RGB);

            return colorTypeConversion.array_to_color(resultFloat_RGB);
        }

        static Color mixColors_inRYB_colorSpace(Color[] colors, float[] colorQuantities, mixingMethod mm, bool ignoreQuants)
        {
            List<float[]> passedColors = new List<float[]>();

            for (int i = 0; i < colors.Length; i++)
            {
                float[] colorFloat_RGB = colorTypeConversion.color_to_array(colors[i]);
                float[] color255_RGB = colorFormatConversion._float_to_255(colorFloat_RGB);
                float[] color255_RYB = rgb2ryb_ryb2rgb.rgb255_to_ryb255(color255_RGB);
                passedColors.Add(color255_RYB);
            }

            float[] result255_RYB = (ignoreQuants == false) ? mixingMethods.mixColors(passedColors, colorQuantities, mm) : mixingMethods.mixColors(passedColors, mm); //actual Color Mixing
            float[] result255_RGB = rgb2ryb_ryb2rgb.ryb255_to_rgb255(result255_RYB);
            float[] resultFloat_RGB = colorFormatConversion._255_to_float(result255_RGB);

            return colorTypeConversion.array_to_color(resultFloat_RGB);
        }

        static Color mixColors_inCMYK_colorSpace(Color[] colors, float[] colorQuantities, mixingMethod mm, bool ignoreQuants)
        {
            List<float[]> passedColors = new List<float[]>();

            for (int i = 0; i < colors.Length; i++)
            {
                float[] colorFloat_RGB = colorTypeConversion.color_to_array(colors[i]);
                float[] color255_RGB = colorFormatConversion._float_to_255(colorFloat_RGB);
                float[] color255_CMYK = rgb2cmyk_cmyk2rgb.rgb255_to_cmyk255(color255_RGB);
                passedColors.Add(color255_CMYK);
            }

            float[] result255_CMYK = (ignoreQuants == false) ? mixingMethods.mixColors(passedColors, colorQuantities, mm) : mixingMethods.mixColors(passedColors, mm); //actual Color Mixing
            float[] result255_RGB = rgb2cmyk_cmyk2rgb.cmyk255_to_rgb255(result255_CMYK);
            float[] resultFloat_RGB = colorFormatConversion._255_to_float(result255_RGB);

            return colorTypeConversion.array_to_color(resultFloat_RGB);
        }
    }
}