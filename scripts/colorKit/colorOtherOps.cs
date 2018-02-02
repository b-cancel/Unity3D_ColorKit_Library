using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace colorKit
{
    public static class colorOtherOps
    {
        //-------------------------Print Functions-------------------------

        //---4 component

        public static void print(Vector4 vect4)
        {
            print(vect4,"");
        }

        public static void print(Vector4 vect4, string printLabel)
        {
            print(colorTypeConversion.vector4_to_array(vect4), printLabel);
        }

        //---3 component

        public static void print(Vector3 vect3)
        {
            print(vect3, "");
        }

        public static void print(Vector3 vect3, string printLabel)
        {
            print(colorTypeConversion.vector3_to_array(vect3), printLabel);
        }

        public static void print(Color color)
        {
            print(color, "");
        }

        public static void print(Color color, string printLabel)
        {
            print(colorTypeConversion.color_to_array(color), printLabel);
        }

        //---2 component

        public static void print(Vector2 vect2)
        {
            print(vect2, "");
        }

        public static void print(Vector2 vect2, string printLabel)
        {
            print(colorTypeConversion.vector2_to_array(vect2), printLabel);
        }

        //-----BASE

        public static void print(float[] array)
        {
            print(array, "");
        }

        public static void print(float[] array, string printLabel)
        {
            string text = printLabel + " (";

            for (int i = 0; i < array.Length; i++)
            {
                if (i != (array.Length - 1))
                    text += array[i] + ", ";
                else
                    text += array[i];
            }

            UnityEngine.MonoBehaviour.print(text + ")");
        }

        //-------------------------Error Correction-------------------------

        public static float[] nanCheck(float[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if(float.IsNaN(array[i]) || float.IsInfinity(array[i]))
                {
                    array[i] = 0;
                    if (float.IsNaN(array[i]))
                        UnityEngine.MonoBehaviour.print("is NAN");
                    else if (float.IsInfinity(array[i]))
                        UnityEngine.MonoBehaviour.print("is Inf or Neg Inf");
                }
            }

            return array;
        }

        public static float[] clamp(float[] array, float min, float max)
        {
            for (int i = 0; i < array.Length; i++)
                array[i] = Mathf.Clamp(array[i], min, max);
            return array;
        }
    }
}