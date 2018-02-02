using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace colorKit
{
    //Description: Change the Color's Data Type (Vectors, Arrays, Colors)

    public static class colorTypeConversion
    {
        //-----2 component ??? (Vector2 | Array)

        public static float[] vector2_to_array(Vector2 vector2)
        {
            return new float[] { vector2[0], vector2[1] };
        }

        public static Vector2 array_to_vector2(float[] array)
        {
            if (array.Length == 2)
                return new Vector2(array[0], array[1]);
            else
                return Vector2.zero;
        }

        //-----3 Component Color (Vector3 | Array | Color)

        public static float[] vector3_to_array(Vector3 vector3)
        {
            return new float[] { vector3[0], vector3[1], vector3[2] };
        }

        public static Color vector3_to_color(Vector3 vector3)
        {
            return new Color(vector3.x, vector3.y, vector3.z);
        }

        public static Vector3 array_to_vector3(float[] array)
        {
            if (array.Length == 3)
                return new Vector3(array[0], array[1], array[2]);
            else
                return Vector3.zero;
        }

        public static Color array_to_color(float[] array)
        {
            if (array.Length == 3)
                return new Color(array[0], array[1], array[2]);
            else
                return Color.black;
        }

        public static Vector3 color_to_vector3(Color color)
        {
            return new Vector3(color.r, color.g, color.b);
        }

        public static float[] color_to_array(Color color)
        {
            return new float[] { color.r, color.g, color.b };
        }

        //-----4 Component Color (Vector4 | Array)

        public static float[] vector4_to_array(Vector4 vector4)
        {
            return new float[] { vector4[0], vector4[1], vector4[2], vector4[3] };
        }

        public static Vector4 array_to_vector4(float[] array)
        {
            if (array.Length == 4)
                return new Vector4(array[0], array[1], array[2], array[3]);
            else
                return Vector4.zero;
        }
    }
}