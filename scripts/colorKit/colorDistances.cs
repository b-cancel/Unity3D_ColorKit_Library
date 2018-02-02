using System.Collections;
using UnityEngine;

namespace colorKit
{
    //Description: find the distance Between 2 Colors in 1D, 2D, 3D, and 4D Space

    //NOTE: Vector 4 distance works out to be really strange because we don't really have an accurate version of distance in 4 Dimensional Space

    public static class colorDistances
    {

        public static float distBetweenColors(Color color1, Color color2, colorSpace colorSpaceUsed)
        {
            switch (colorSpaceUsed)
            {
                case colorSpace.RGB:
                    return distBetweenColors_inRGB_colorSpace(color1, color2);
                case colorSpace.RYB:
                    return distBetweenColors_inRYB_colorSpace(color1, color2);
                default:
                    return distBetweenColors_inCMYK_colorSpace(color1, color2);
            } 
        }

        static float distBetweenColors_inRGB_colorSpace(Color color1, Color color2)
        {
            float[] color1_Float_rGb = colorTypeConversion.color_to_array(color1);
            float[] color1_255_rGb = colorFormatConversion._float_to_255(color1_Float_rGb);

            float[] color2_Float_rGb = colorTypeConversion.color_to_array(color2);
            float[] color2_255_rGb = colorFormatConversion._float_to_255(color2_Float_rGb);

            return distBetweenColors(color1_255_rGb, color2_255_rGb);
        }

        static float distBetweenColors_inRYB_colorSpace(Color color1, Color color2)
        {
            float[] color1_Float_rGb = colorTypeConversion.color_to_array(color1);
            float[] color1_255_rGb = colorFormatConversion._float_to_255(color1_Float_rGb);
            float[] color1_255_rYb = rgb2ryb_ryb2rgb.rgb255_to_ryb255(color1_255_rGb);

            float[] color2_Float_rGb = colorTypeConversion.color_to_array(color2);
            float[] color2_255_rGb = colorFormatConversion._float_to_255(color2_Float_rGb);
            float[] color2_255_rYb = rgb2ryb_ryb2rgb.rgb255_to_ryb255(color2_255_rGb);

            return distBetweenColors(color1_255_rYb, color2_255_rYb);
        }

        //NOTE: be warned that this version of 4D distance is strange and does not behave the way I would personally except it to
        static float distBetweenColors_inCMYK_colorSpace(Color color1, Color color2)
        {
            float[] color1_Float_rGb = colorTypeConversion.color_to_array(color1);
            float[] color1_255_rGb = colorFormatConversion._float_to_255(color1_Float_rGb);
            float[] color1_255_CMYK = rgb2cmyk_cmyk2rgb.rgb255_to_cmyk255(color1_255_rGb);

            float[] color2_Float_rGb = colorTypeConversion.color_to_array(color2);
            float[] color2_255_rGb = colorFormatConversion._float_to_255(color2_Float_rGb);
            float[] color2_255_CMYK = rgb2cmyk_cmyk2rgb.rgb255_to_cmyk255(color2_255_rGb);

            return distBetweenColors(color1_255_CMYK, color2_255_CMYK);
        }

        //-----BASE

        public static float distBetweenColors(float[] color1, float[] color2)
        {
            if ((color1.Length == color2.Length) && (color1.Length <= 4) && (color1.Length >= 1))
            {
                switch (color1.Length)
                {
                    case 1:
                        return Mathf.Abs(color1[0] - color2[0]);
                    case 2:
                        Vector2 color1Vect2 = colorTypeConversion.array_to_vector2(color1);
                        Vector2 color2Vect2 = colorTypeConversion.array_to_vector2(color2);

                        return Vector2.Distance(color1Vect2, color2Vect2);
                    case 3:
                        Vector3 color1Vect3 = colorTypeConversion.array_to_vector3(color1);
                        Vector3 color2Vect3 = colorTypeConversion.array_to_vector3(color2);

                        return Vector3.Distance(color1Vect3, color2Vect3);
                    case 4:
                        Vector4 color1Vect4 = colorTypeConversion.array_to_vector4(color1);
                        Vector4 color2Vect4 = colorTypeConversion.array_to_vector4(color2);

                        return Vector4.Distance(color1Vect4, color2Vect4);
                    default:
                        return 0;
                }
            }
            else
                return 0;
        }

    }
}