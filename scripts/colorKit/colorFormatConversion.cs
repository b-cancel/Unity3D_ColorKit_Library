using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace colorKit
{
    //Description: Change the Color's(Array's) Format (255,float,hex)

    public static class colorFormatConversion
    {
        public static float[] _float_to_255(float[] colorFloat)
        {
            float[] color255 = new float[colorFloat.Length];
            for (int i = 0; i < color255.Length; i++)
                color255[i] = _float_to_255(colorFloat[i]);
            return color255;
        }

        public static string[] _float_to_hex(float[] colorFloat)
        {
            string[] colorHex = new string[colorFloat.Length];
            for (int i = 0; i < colorHex.Length; i++)
                colorHex[i] = _float_to_hex(colorFloat[i]);
            return colorHex;
        }

        public static float[] _255_to_float(float[] color255)
        {
            float[] colorFloat = new float[color255.Length];
            for (int i = 0; i < colorFloat.Length; i++)
                colorFloat[i] = _255_to_float(color255[i]);
            return colorFloat;
        }

        public static string[] _255_to_hex(float[] color255)
        {
            string[] colorHex = new string[color255.Length];
            for (int i = 0; i < colorHex.Length; i++)
                colorHex[i] = _255_to_hex(color255[i]);
            return colorHex;
        }

        public static float[] _hex_to_float(string[] colorHex)
        {
            float[] colorFloat = new float[colorHex.Length];
            for (int i = 0; i < colorFloat.Length; i++)
                colorFloat[i] = _hex_to_float(colorHex[i]);
            return colorFloat;
        }

        public static float[] _hex_to_255(string[] colorHex)
        {
            float[] color255 = new float[colorHex.Length];
            for (int i = 0; i < color255.Length; i++)
                color255[i] = _hex_to_255(colorHex[i]);
            return color255;
        }

        //-------------------------helpers of the functions above-------------------------

        static float _float_to_255(float numFloat)
        {
            return Mathf.Clamp(numFloat * 255, 0, 255);
        }

        static string _float_to_hex(float numFloat)
        {
            string hex = Convert.ToString((int)Mathf.Round(255 * numFloat), 16);
            return (hex.Length == 1) ? "0" + hex : hex;
        }

        static float _255_to_float(float num255)
        {
            return Mathf.Clamp(num255 / 255, 0, 1);
        }

        static string _255_to_hex(float num255)
        {
            string hex = Convert.ToString((int)Mathf.Round(num255), 16);
            return (hex.Length == 1) ? "0" + hex : hex;
        }

        static float _hex_to_float(string numHex)
        {
            return Mathf.Clamp(Mathf.Clamp(Convert.ToInt32(numHex, 16), 0, 255) / 255, 0, 1);
        }

        static float _hex_to_255(string numHex)
        {
            return Mathf.Clamp(Convert.ToInt32(numHex, 16), 0, 255);
        }
    }
}