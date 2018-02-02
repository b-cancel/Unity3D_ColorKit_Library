using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace colorKit
{
    /// <summary>
    /// Description: Convert RGB to CMYK -and- CMYK to RGB
    /// 
    /// The function names indicate 
    ///     INPUT[color space][format of components in input]_2_OUTPUT[color space][format of components in output]
    /// If you pass an array of improper length, you will get an array of the expected length returned... but it will be filled with "-1"
    ///     
    /// This File Combines the Code From
    /// RGB to RYB: https://github.com/bahamas10/node-rgb2ryb/blob/master/rgb2ryb.js
    /// RYB to RGB: https://github.com/bahamas10/node-ryb2rgb/blob/master/ryb2rgb.js 
    /// 
    /// NOTE: we only accept and then convert in 255 format because it yeilds the most accurate results
    /// </summary>

    public static class rgb2ryb_ryb2rgb
    {

        //-------------------------RGB -> RYB-------------------------

        public static float[] rgb255_to_ryb255(float[] rgb255) //NOTE: all different format conversion types use this function
        {
            if (rgb255.Length != 3)
                return new float[] { -1, -1, -1 };
            else
            {
                float r = rgb255[0];
                float g = rgb255[1];
                float b = rgb255[2];

                // Remove the whiteness from the color.
                float w = Mathf.Min(r, g, b);
                r -= w;
                g -= w;
                b -= w;

                float mg = Mathf.Max(r, g, b);

                // Get the yellow out of the red+green.
                float y = Mathf.Min(r, g);
                r -= y;
                g -= y;

                // If this unfortunate conversion combines blue and green, then cut each in
                // half to preserve the value's maximum range.
                if ((b == 0) ? false : true && (g == 0) ? false : true)
                {
                    b /= 2.0f;
                    g /= 2.0f;
                }

                // Redistribute the remaining green.
                y += g;
                b += g;

                // Normalize to values.
                float my = Mathf.Max(r, y, b);
                if ((my == 0) ? false : true)
                {
                    float n = mg / my;
                    r *= n;
                    y *= n;
                    b *= n;
                }

                // Add the white back in.
                r += w;
                y += w;
                b += w;

                float[] ryb255 = new float[] { r, y, b };
                ryb255 = colorOtherOps.clamp(ryb255, 0, 255);
                return colorOtherOps.nanCheck(ryb255);
            }
        }

        //-------------------------RYB -> RGB-------------------------

        public static float[] ryb255_to_rgb255(float[] ryb255) //NOTE: all different format conversion types use this function
        {
            if (ryb255.Length != 3)
                return new float[] { -1, -1, -1 };
            else
            {
                float r = ryb255[0];
                float y = ryb255[1];
                float b = ryb255[2];

                // Remove the whiteness from the color.
                float w = Mathf.Min(r, y, b);
                r -= w;
                y -= w;
                b -= w;

                float my = Mathf.Max(r, y, b);

                // Get the green out of the yellow and blue
                float g = Mathf.Min(y, b);
                y -= g;
                b -= g;

                if ((b == 0) ? false : true && (g == 0) ? false : true)
                {
                    b *= 2.0f;
                    g *= 2.0f;
                }

                // Redistribute the remaining yellow.
                r += y;
                g += y;

                // Normalize to values.
                float mg = Mathf.Max(r, g, b);
                if ((mg == 0) ? false : true)
                {
                    float n = my / mg;
                    r *= n;
                    g *= n;
                    b *= n;
                }

                // Add the white back in.
                r += w;
                g += w;
                b += w;

                float[] rgb255 = new float[] { r, g, b };
                rgb255 = colorOtherOps.clamp(rgb255, 0, 255);
                return colorOtherOps.nanCheck(rgb255);
            }
        }
    }
}