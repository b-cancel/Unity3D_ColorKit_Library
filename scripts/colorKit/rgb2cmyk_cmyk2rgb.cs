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
    /// NOTE: we only accept and then convert in 255 format because it yeilds the most accurate results
    /// NOTE: converting white and black to a different color space can cause errors which is why we added the if and else if statements on both functions
    /// </summary>

    public static class rgb2cmyk_cmyk2rgb
    {

        //-------------------------RGB -> CMKY-------------------------

        public static float[] rgb255_to_cmyk255(float[] rgb255)
        {
            if (rgb255.Length != 3)
                return new float[] { -1, -1, -1 };
            else
            {
                float[] cmykFloat = rgb255_to_cmykFloat(rgb255);
                return colorFormatConversion._float_to_255(cmykFloat);
            }
        }

        static float[] rgb255_to_cmykFloat(float[] rgb255)
        {
            if (rgb255.Length != 3)
                return new float[] { -1, -1, -1 };
            else
            {
                if (rgb255[0] == 0 && rgb255[1] == 0 && rgb255[2] == 0) //black
                    return new float[] { 0, 0, 0, 1 }; //black
                else if (rgb255[0] == 255 && rgb255[1] == 255 && rgb255[2] == 255) //white
                    return new float[] { 0, 0, 0, 0 }; //white
                else
                {
                    float cyan = 255 - rgb255[0];
                    float magenta = 255 - rgb255[1];
                    float yellow = 255 - rgb255[2];
                    float black = Mathf.Min(cyan, magenta, yellow);
                    cyan = ((cyan - black) / (255 - black));
                    magenta = ((magenta - black) / (255 - black));
                    yellow = ((yellow - black) / (255 - black));

                    // And return back the cmyk typed accordingly.
                    float[] cmykFloat = new float[] { cyan, magenta, yellow, black };
                    cmykFloat = colorOtherOps.clamp(cmykFloat, 0, 1);
                    return colorOtherOps.nanCheck(cmykFloat);
                }
            }
        }

        //-------------------------CMKY -> RGB-------------------------

        public static float[] cmyk255_to_rgb255(float[] cmyk255)
        {
            if (cmyk255.Length != 4)
                return new float[] { -1, -1, -1 };
            else
            {
                float[] cmykFloat = colorFormatConversion._255_to_float(cmyk255);
                return cmykFloat_to_rgb255(cmykFloat);
            }
        }

        static float[] cmykFloat_to_rgb255(float[] cmykFloat) //NOTE: all different format conversion types use this function
        {
            if (cmykFloat.Length != 4)
                return new float[] { -1, -1, -1 };
            else
            {
                if (cmykFloat[0] == 0 && cmykFloat[1] == 0 && cmykFloat[2] == 0 && cmykFloat[3] == 1) //black
                    return new float[] { 0, 0, 0 }; //black
                else if (cmykFloat[0] == 0 && cmykFloat[1] == 0 && cmykFloat[2] == 0 && cmykFloat[3] == 0) //white
                    return new float[] { 255, 255, 255 }; //white
                else
                {
                    float red = (float)((cmykFloat[0] * (255 - cmykFloat[3])) + cmykFloat[3]);
                    float green = (float)((cmykFloat[1] * (255 - cmykFloat[3])) + cmykFloat[3]);
                    float blue = (float)((cmykFloat[2] * (255 - cmykFloat[3])) + cmykFloat[3]);
                    red = 255 - red; //ORIGINAL: Math.round((1.0 - R) * 255.0 + 0.5)
                    green = 255 - green;
                    blue = 255 - blue;

                    // And return back the rgb typed accordingly.
                    float[] rgb255 = new float[] { red, green, blue };
                    rgb255 = colorOtherOps.clamp(rgb255, 0, 255);
                    return colorOtherOps.nanCheck(rgb255);
                }
            }
        }
    }
}