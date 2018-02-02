using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using colorKit;
using lerpKit;

namespace colorKit
{
    public enum desiredMixtureType { additive, subtractive };
    public enum colorSpace { RGB, RYB, CMYK };
    public enum mixingMethod { spaceAveraging, colorAveraging, colorComponentAveraging, eachAsPercentOfMax }
}

//DESCRIPTION: this script allows the colorKit to also be addressible as extension methods of Unity's Color class (and others)

public static class colorEXT
{
    //-----FUNCTION VERSIONS that use a dummy (non used) instance of 'Color'

    #region colorFormatConversion

    //Description: Change the Color's Format (255,float,hex)

    public static float[] _float_to_255(this float[] f, float[] colorFloat)
    {
        return colorFormatConversion._float_to_255(colorFloat);
    }

    public static string[] _float_to_hex(this float[] f, float[] colorFloat)
    {
        return colorFormatConversion._float_to_hex(colorFloat);
    }

    public static float[] _255_to_float(this float[] f, float[] color255)
    {
        return colorFormatConversion._255_to_float(color255);
    }

    public static string[] _255_to_hex(this float[] f, float[] color255)
    {
        return colorFormatConversion._255_to_hex(color255);
    }

    public static float[] _hex_to_float(this string[] s, string[] colorHex)
    {
        return colorFormatConversion._hex_to_float(colorHex);
    }

    public static float[] _hex_to_255(this string[] s, string[] colorHex)
    {
        return colorFormatConversion._hex_to_255(colorHex);
    }

    #endregion

    #region colorTypeConversion

    //Description: Change the Color's Data Type (Vectors, Arrays, Colors)

    //-----2 component ??? (Vector2 | Array)

    public static float[] vector2_to_array(this Vector2 v2, Vector2 vector2)
    {
        return colorTypeConversion.vector2_to_array(vector2);
    }

    public static Vector2 array_to_vector2(this float[] fa, float[] array)
    {
        return colorTypeConversion.array_to_vector2(array);
    }

    //-----3 Component Color (Vector3 | Array | Color)

    public static float[] vector3_to_array(this Vector3 v3, Vector3 vector3)
    {
        return colorTypeConversion.vector3_to_array(vector3);
    }

    public static Color vector3_to_color(this Vector3 v3, Vector3 vector3)
    {
        return colorTypeConversion.vector3_to_color(vector3);
    }

    public static Vector3 array_to_vector3(this float[] fa, float[] array)
    {
        return colorTypeConversion.array_to_vector3(array);
    }

    public static Color array_to_color(this float[] fa, float[] array)
    {
        return colorTypeConversion.array_to_color(array);
    }

    public static Vector3 color_to_vector3(this Color c, Color color)
    {
        return colorTypeConversion.color_to_vector3(color);
    }

    public static float[] color_to_array(this Color c, Color color)
    {
        return colorTypeConversion.color_to_array(color);
    }

    //-----4 Component Color (Vector4 | Array)

    public static float[] vector4_to_array(this Vector4 v4, Vector4 vector4)
    {
        return colorTypeConversion.vector4_to_array(vector4);
    }

    public static Vector4 array_to_vector4(this float[] fa, float[] array)
    {
        return colorTypeConversion.array_to_vector4(array);
    }

    #endregion

    #region rgb2ryb_ryb2rgb

    //Description: Convert RGB to RYB -and- RYB to RGB

    public static float[] rgb255_to_ryb255(this Color c, float[] rgb255)
    {
        return rgb2ryb_ryb2rgb.rgb255_to_ryb255(rgb255);
    }

    public static float[] ryb255_to_rgb255(this Color c, float[] ryb255)
    {
        return rgb2ryb_ryb2rgb.ryb255_to_rgb255(ryb255);
    }

    #endregion

    #region rgb2cmyk_cmyk2rgb

    //Description: Convert RGB to CMYK -and- CMYK to RGB

    public static float[] rgb255_to_cmyk255(this Color c, float[] rgb255)
    {
        return rgb2cmyk_cmyk2rgb.rgb255_to_cmyk255(rgb255);
    }
    public static float[] cmyk255_to_rgb255(this Color c, float[] cmyk255)
    {
        return rgb2cmyk_cmyk2rgb.cmyk255_to_rgb255(cmyk255);
    }

    #endregion

    #region colorDistances 

    //Description: find the distance Between 2 Colors in 1D, 2D, 3D, and 4D Space

    //NOTE: Vector 4 distance works out to be really strange because we don't really have an accurate version of distance in 4 Dimensional Space

    public static float distBetweenColors(this Color c, Color color1, Color color2, colorSpace colorSpaceUsed)
    {
        return colorDistances.distBetweenColors(color1, color2, colorSpaceUsed);
    }

    public static float distBetweenColors(this Color c, float[] color1, float[] color2)
    {
        return colorDistances.distBetweenColors(color1, color2);
    }

    #endregion

    #region colorCompliments

    //get the complement / inverse of a color

    public static Color complimentary(this Color c, Color origColor, colorSpace csToUse)
    {
        return colorCompliments.complimentary(origColor, csToUse);
    }

    public static float[] complimentary(this Color c, float[] color, int floatLimit)
    {
        return colorCompliments.complimentary(color, floatLimit);
    }

    #endregion

    #region colorLerping

    //Allows you to interpolate between 2 colors

    public static Color colorLerp(this Color c, Color start, Color end, float lerpValue, colorSpace csToUse)
    {
        return colorLerping.colorLerp(start, end, lerpValue, csToUse);
    }

    public static float[] colorLerp(this Color c, float[] start, float[] end, float lerpValue)
    {
        return colorLerping.colorLerp(start, end, lerpValue);
    }

    #endregion

    #region colorLerpHelper

    public static float calcGuideDistance(this Color c, Color startColor, Color currColor, Color endColor, colorSpace CS, guideDistance GD)
    {
        return colorLerpHelper.calcGuideDistance(startColor, currColor, endColor, CS, GD);
    }

    public static float calcLerpValue(this Color c, Color startColor, Color currColor, Color endColor, float guideDistance, float guideTime, unitOfTime UOT_GD, updateLocation UL, colorSpace CS)
    {
        return colorLerpHelper.calcLerpValue(startColor, currColor, endColor, guideDistance, guideTime, UOT_GD, UL, CS);
    }

    public static float calcLerpValue(this Color c, Color startColor, Color currColor, Color endColor, float lerpVelocity_DperF, colorSpace CS)
    {
        return colorLerpHelper.calcLerpValue(startColor, currColor, endColor, lerpVelocity_DperF, CS);
    }

    #endregion

    #region colorMixing

    public static Color mixColors(this Color c, Color[] colors, colorSpace csToUse, mixingMethod mm)
    {
        return colorMixing.mixColors(colors, csToUse, mm);
    }

    public static Color mixColors(this Color c, Color[] colors, float[] colorQuantities, colorSpace csToUse, mixingMethod mm)
    {
        return colorMixing.mixColors(colors, colorQuantities, csToUse, mm);
    }

    #endregion

    #region mixingMethods

    public static float[] mixColors(this Color c, List<float[]> colors, mixingMethod mm)
    {
        return mixingMethods.mixColors(colors, mm);
    }

    public static float[] mixColors(this Color c, List<float[]> colors, float[] colorQuantities, mixingMethod mm)
    {
        return mixingMethods.mixColors(colors, colorQuantities, mm);
    }

    #endregion

    #region colorOtherOps

    //-------------------------Print Functions-------------------------

    //---4 component

    public static void print(this Vector4 v4, Vector4 vect4)
    {
        colorOtherOps.print(vect4);
    }

    public static void print(this Vector4 v4, string printLabel, Vector4 vect4)
    {
        colorOtherOps.print(vect4, printLabel);
    }

    //---3 component

    public static void print(this Vector3 v3, Vector3 vect3)
    {
        colorOtherOps.print(vect3);
    }

    public static void print(this Vector3 v3, string printLabel, Vector3 vect3)
    {
        colorOtherOps.print(vect3, printLabel);
    }

    public static void print(this Color c, Color color)
    {
        colorOtherOps.print(color);
    }

    public static void print(this Color c, string printLabel, Color color)
    {
        colorOtherOps.print(color, printLabel);
    }

    //---2 component

    public static void print(this Vector2 v2, Vector2 vect2)
    {
        colorOtherOps.print(vect2);
    }

    public static void print(this Vector2 v2, string printLabel, Vector2 vect2)
    {
        colorOtherOps.print(vect2, printLabel);
    }

    //-----BASE

    public static void print(this float[] fa, float[] array)
    {
        colorOtherOps.print(array);
    }

    public static void print(this float[] fa, string printLabel, float[] array)
    {
        colorOtherOps.print(array, printLabel);
    }

    //-------------------------Error Correction-------------------------

    public static float[] nanCheck(this float[] fa, float[] array)
    {
        return colorOtherOps.nanCheck(array);
    }

    public static float[] clamp(this float[] fa, float[] array, float min, float max)
    {
        return colorOtherOps.clamp(array, min, max);
    }

    #endregion

    //-----FUNCTION VERSIONS that use the instance of whatever type they extend (the instance will be of the same type as the first parameter)

    #region colorFormatConversion

    //Description: Change the Color's Format (255,float,hex)

    public static float[] _float_to_255(this float[] colorFloat)
    {
        return colorFormatConversion._float_to_255(colorFloat);
    }

    public static string[] _float_to_hex(this float[] colorFloat)
    {
        return colorFormatConversion._float_to_hex(colorFloat);
    }

    public static float[] _255_to_float(this float[] color255)
    {
        return colorFormatConversion._255_to_float(color255);
    }

    public static string[] _255_to_hex(this float[] color255)
    {
        return colorFormatConversion._255_to_hex(color255);
    }

    public static float[] _hex_to_float(this string[] colorHex)
    {
        return colorFormatConversion._hex_to_float(colorHex);
    }

    public static float[] _hex_to_255(this string[] colorHex)
    {
        return colorFormatConversion._hex_to_255(colorHex);
    }

    #endregion

    #region colorTypeConversion

    //Description: Change the Color's Data Type (Vectors, Arrays, Colors)

    //-----2 component ??? (Vector2 | Array)

    public static float[] vector2_to_array(this Vector2 vector2)
    {
        return colorTypeConversion.vector2_to_array(vector2);
    }

    public static Vector2 array_to_vector2(this float[] array)
    {
        return colorTypeConversion.array_to_vector2(array);
    }

    //-----3 Component Color (Vector3 | Array | Color)

    public static float[] vector3_to_array(this Vector3 vector3)
    {
        return colorTypeConversion.vector3_to_array(vector3);
    }

    public static Color vector3_to_color(this Vector3 vector3)
    {
        return colorTypeConversion.vector3_to_color(vector3);
    }

    public static Vector3 array_to_vector3(this float[] array)
    {
        return colorTypeConversion.array_to_vector3(array);
    }

    public static Color array_to_color(this float[] array)
    {
        return colorTypeConversion.array_to_color(array);
    }

    public static Vector3 color_to_vector3(this Color color)
    {
        return colorTypeConversion.color_to_vector3(color);
    }

    public static float[] color_to_array(this Color color)
    {
        return colorTypeConversion.color_to_array(color);
    }

    //-----4 Component Color (Vector4 | Array)

    public static float[] vector4_to_array(this Vector4 vector4)
    {
        return colorTypeConversion.vector4_to_array(vector4);
    }

    public static Vector4 array_to_vector4(this float[] array)
    {
        return colorTypeConversion.array_to_vector4(array);
    }

    #endregion

    #region rgb2ryb_ryb2rgb

    //Description: Convert RGB to RYB -and- RYB to RGB

    public static float[] rgb255_to_ryb255(this float[] rgb255)
    {
        return rgb2ryb_ryb2rgb.rgb255_to_ryb255(rgb255);
    }

    public static float[] ryb255_to_rgb255(this float[] ryb255)
    {
        return rgb2ryb_ryb2rgb.ryb255_to_rgb255(ryb255);
    }

    #endregion

    #region rgb2cmyk_cmyk2rgb

    //Description: Convert RGB to CMYK -and- CMYK to RGB

    public static float[] rgb255_to_cmyk255(this float[] rgb255)
    {
        return rgb2cmyk_cmyk2rgb.rgb255_to_cmyk255(rgb255);
    }
    public static float[] cmyk255_to_rgb255(this float[] cmyk255)
    {
        return rgb2cmyk_cmyk2rgb.cmyk255_to_rgb255(cmyk255);
    }

    #endregion

    #region colorDistances 

    //Description: find the distance Between 2 Colors in 1D, 2D, 3D, and 4D Space

    //NOTE: Vector 4 distance works out to be really strange because we don't really have an accurate version of distance in 4 Dimensional Space

    public static float distBetweenColors(this Color color1, Color color2, colorSpace colorSpaceUsed)
    {
        return colorDistances.distBetweenColors(color1, color2, colorSpaceUsed);
    }

    public static float distBetweenColors(this float[] color1, float[] color2)
    {
        return colorDistances.distBetweenColors(color1, color2);
    }

    #endregion

    #region colorCompliments

    //get the complement / inverse of a color

    public static Color complimentary(this Color color, colorSpace csToUse)
    {
        return colorCompliments.complimentary(color, csToUse);
    }

    public static float[] complimentary(this float[] color, int floatLimit)
    {
        return colorCompliments.complimentary(color, floatLimit);
    }

    #endregion

    #region colorLerping

    //Allows you to interpolate between 2 colors

    public static Color colorLerp(this Color startColor, Color endColor, float lerpValue, colorSpace csToUse)
    {
        return colorLerping.colorLerp(startColor, endColor, lerpValue, csToUse);
    }

    public static float[] colorLerp(this float[] startValues, float[] endValues, float lerpValue)
    {
        return colorLerping.colorLerp(startValues, endValues, lerpValue);
    }

    #endregion

    #region colorLerpHelper

    public static float calcGuideDistance(this Color startColor, Color currColor, Color endColor, colorSpace CS, guideDistance GD)
    {
        return colorLerpHelper.calcGuideDistance(startColor, currColor, endColor, CS, GD);
    }

    public static float calcLerpValue(this Color startColor, Color currColor, Color endColor, float guideDistance, float guideTime, unitOfTime UOT_GD, updateLocation UL, colorSpace CS)
    {
        return colorLerpHelper.calcLerpValue(startColor, currColor, endColor, guideDistance, guideTime, UOT_GD, UL, CS);
    }

    public static float calcLerpValue(this Color startColor, Color currColor, Color endColor, float lerpVelocity_DperF, colorSpace CS)
    {
        return colorLerpHelper.calcLerpValue(startColor, currColor, endColor, lerpVelocity_DperF, CS);
    }

    #endregion

    #region colorMixing

    public static Color mixColors(this Color[] colors, colorSpace csToUse, mixingMethod mm)
    {
        return colorMixing.mixColors(colors, csToUse, mm);
    }

    public static Color mixColors(this Color[] colors, float[] colorQuantities, colorSpace csToUse, mixingMethod mm)
    {
        return colorMixing.mixColors(colors, colorQuantities, csToUse, mm);
    }

    #endregion

    #region mixingMethods

    public static float[] mixColors(this List<float[]> colors, mixingMethod mm)
    {
        return mixingMethods.mixColors(colors, mm);
    }

    public static float[] mixColors(this List<float[]> colors, float[] colorQuantities, mixingMethod mm)
    {
        return mixingMethods.mixColors(colors, colorQuantities, mm);
    }

    #endregion

    #region colorOtherOps

    //-------------------------Print Functions-------------------------

    //---4 component

    public static void print(this Vector4 vect4)
    {
        colorOtherOps.print(vect4);
    }

    public static void print(this Vector4 vect4, string printLabel)
    {
        colorOtherOps.print(vect4, printLabel);
    }

    //---3 component

    public static void print(this Vector3 vect3)
    {
        colorOtherOps.print(vect3);
    }

    public static void print(this Vector3 vect3, string printLabel)
    {
        colorOtherOps.print(vect3, printLabel);
    }

    public static void print(this Color color)
    {
        colorOtherOps.print(color);
    }

    public static void print(this Color color, string printLabel)
    {
        colorOtherOps.print(color, printLabel);
    }

    //---2 component

    public static void print(this Vector2 vect2)
    {
        colorOtherOps.print(vect2);
    }

    public static void print(this Vector2 vect2, string printLabel)
    {
        colorOtherOps.print(vect2, printLabel);
    }

    //-----BASE

    public static void print(this float[] array)
    {
        colorOtherOps.print(array);
    }

    public static void print(this float[] array, string printLabel)
    {
        colorOtherOps.print(array, printLabel);
    }

    //-------------------------Error Correction-------------------------

    public static float[] nanCheck(this float[] array)
    {
        return colorOtherOps.nanCheck(array);
    }

    public static float[] clamp(this float[] array, float min, float max)
    {
        return colorOtherOps.clamp(array, min, max);
    }

    #endregion

}