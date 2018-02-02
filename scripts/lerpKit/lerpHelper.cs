using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace lerpKit
{
    public static class lerpHelper
    {

        //-------------------------CALCULATE GUIDE DISTANCE-------------------------

        public static float calcGuideDistance(float startValue, float currValue, float endValue, guideDistance GD)
        {
            //NOTE: guideDistance.other has no definition for anything but color

            if (GD == guideDistance.distBetween_StartAndCurr)
                return Mathf.Abs(startValue - currValue);
            else if (GD == guideDistance.distBetween_StartAndEnd)
                return Mathf.Abs(startValue - endValue);
            else //guideDistance.distBetween_CurrAndEnd
                return Mathf.Abs(currValue - endValue);
        }

        public static float calcGuideDistance(Vector2 startVect2, Vector2 currVector2, Vector2 endVector2, guideDistance GD)
        {
            //NOTE: guideDistance.other has no definition for anything but color

            if (GD == guideDistance.distBetween_StartAndCurr)
                return Vector2.Distance(startVect2, currVector2);
            else if (GD == guideDistance.distBetween_StartAndEnd)
                return Vector2.Distance(startVect2, endVector2);
            else //guideDistance.distBetween_CurrAndEnd
                return Vector2.Distance(currVector2, endVector2);
        }

        public static float calcGuideDistance(Vector3 startVect3, Vector3 currVector3, Vector3 endVector3, guideDistance GD)
        {
            //NOTE: guideDistance.other has no definition for anything but color

            if (GD == guideDistance.distBetween_StartAndCurr)
                return Vector3.Distance(startVect3, currVector3);
            else if (GD == guideDistance.distBetween_StartAndEnd)
                return Vector3.Distance(startVect3, endVector3);
            else //guideDistance.distBetween_CurrAndEnd
                return Vector3.Distance(currVector3, endVector3);
        }

        public static float calcGuideDistance(Vector4 startVect4, Vector4 currVector4, Vector4 endVector4, guideDistance GD)
        {
            //NOTE: guideDistance.other has no definition for anything but color

            if (GD == guideDistance.distBetween_StartAndCurr)
                return Vector4.Distance(startVect4, currVector4);
            else if (GD == guideDistance.distBetween_StartAndEnd)
                return Vector4.Distance(startVect4, endVector4);
            else //guideDistance.distBetween_CurrAndEnd
                return Vector4.Distance(currVector4, endVector4);
        }

        public static float calcGuideDistance(float[] startValues, float[] currValues, float[] endValues, guideDistance GD)
        {
            //NOTE: guideDistance.other has no definition for anything but color

            if (GD == guideDistance.distBetween_StartAndCurr)
                return euclideanDistance(startValues, currValues);
            else if (GD == guideDistance.distBetween_StartAndEnd)
                return euclideanDistance(startValues, endValues);
            else //guideDistance.distBetween_CurrAndEnd
                return euclideanDistance(currValues, endValues);
        }

        public static float calcGuideDistance(Color startColor, Color currColor, Color endColor, guideDistance GD)
        {

            if (GD == guideDistance.distBetween_StartAndCurr)
                return distance(startColor, currColor);
            else if (GD == guideDistance.distBetween_StartAndEnd)
                return distance(startColor, endColor);
            else if (GD == guideDistance.distBetween_CurrAndEnd)
                return distance(currColor, endColor);
            else
                return 441.672956f; // maxDistanceInRGBColorSpace
        }

        //-------------------------CALCULATE LERP VALUE (using Guide Distance, Guide Time, Unit of Time, and Update Location)-------------------------

        public static float calcLerpValue(float startValue, float currValue, float endValue, float guideDistance, float guideTime, unitOfTime UOT_GD, updateLocation UL)
        {
            return calcLerpValue(startValue, currValue, endValue, calcLerpVelocity(guideDistance, guideTime, UOT_GD,UL));
        }

        public static float calcLerpValue(Vector2 startVector2, Vector2 currVector2, Vector2 endVector2, float guideDistance, float guideTime, unitOfTime UOT_GD, updateLocation UL)
        {
            return calcLerpValue(startVector2, currVector2, endVector2, calcLerpVelocity(guideDistance, guideTime, UOT_GD, UL));
        }

        public static float calcLerpValue(Vector3 startVector3, Vector3 currVector3, Vector3 endVector3, float guideDistance, float guideTime, unitOfTime UOT_GD, updateLocation UL)
        {
            return calcLerpValue(startVector3, currVector3, endVector3, calcLerpVelocity(guideDistance, guideTime, UOT_GD, UL));
        }

        public static float calcLerpValue(Vector4 startVector4, Vector4 currVector4, Vector4 endVector4, float guideDistance, float guideTime, unitOfTime UOT_GD, updateLocation UL)
        {
            return calcLerpValue(startVector4, currVector4, endVector4, calcLerpVelocity(guideDistance, guideTime, UOT_GD, UL));
        }

        public static float calcLerpValue(float[] startValues, float[] currValues, float[] endValues, float guideDistance, float guideTime, unitOfTime UOT_GD, updateLocation UL)
        {
            return calcLerpValue(startValues, currValues, endValues, calcLerpVelocity(guideDistance, guideTime, UOT_GD, UL));
        }

        public static float calcLerpValue(Color startColor, Color currColor, Color endColor, float guideDistance, float guideTime, unitOfTime UOT_GD, updateLocation UL)
        {
            return calcLerpValue(startColor, currColor, endColor, calcLerpVelocity(guideDistance, guideTime, UOT_GD, UL));
        }

        //-------------------------CALCULATE LERP VALUE (using Guide Velocity)-------------------------

        public static float calcLerpValue(float startValue, float currValue, float endValue, float lerpVelocity_DperF)
        {
            //---calc distance left to travel
            float distToFinish = Mathf.Abs(currValue - endValue);

            //--- calc lerp value based on this
            return Mathf.Clamp((lerpVelocity_DperF / distToFinish), 0, 1);
        }

        public static float calcLerpValue(Vector2 startVector2, Vector2 currVector2, Vector2 endVector2, float lerpVelocity_DperF)
        {
            //---calc distance left to travel
            float distToFinish = Vector2.Distance(currVector2, endVector2);

            //--- calc lerp value based on this
            return Mathf.Clamp((lerpVelocity_DperF / distToFinish), 0, 1);
        }

        public static float calcLerpValue(Vector3 startVector3, Vector3 currVector3, Vector3 endVector3, float lerpVelocity_DperF)
        {
            //---calc distance left to travel
            float distToFinish = Vector3.Distance(currVector3, endVector3);

            //--- calc lerp value based on this
            return Mathf.Clamp((lerpVelocity_DperF / distToFinish), 0, 1);
        }

        public static float calcLerpValue(Vector4 startVector4, Vector4 currVector4, Vector4 endVector4, float lerpVelocity_DperF)
        {
            //---calc distance left to travel
            float distToFinish = Vector4.Distance(currVector4, endVector4);

            //--- calc lerp value based on this
            return Mathf.Clamp((lerpVelocity_DperF / distToFinish), 0, 1);
        }

        public static float calcLerpValue(float[] startValues, float[] currValues, float[] endValues, float lerpVelocity_DperF)
        {
            //---calc distance left to travel
            float distToFinish = euclideanDistance(currValues, endValues);

            //--- calc lerp value based on this
            return Mathf.Clamp((lerpVelocity_DperF / distToFinish), 0, 1);
        }

        public static float calcLerpValue(Color startColor, Color currColor, Color endColor, float lerpVelocity_DperF)
        {
            //---calc distance left to travel
            float distToFinish = distance(currColor, endColor);

            //--- calc lerp value based on this
            return Mathf.Clamp((lerpVelocity_DperF / distToFinish), 0, 1);
        }

        //-------------------------HELPER FUNCTIONS-------------------------

        static float calcLerpVelocity(float guideDistance, float timeToTravel_GD, unitOfTime UOT_GD, updateLocation UL)
        {
            return calcLerpVelocity(guideDistance, timeToFrames(timeToTravel_GD, UOT_GD, UL));
        }

        static float calcLerpVelocity(float guideDistance, float framesToTravel_GD)
        {
            return guideDistance / framesToTravel_GD;
        }

        static float timeToFrames(float time, unitOfTime UOT, updateLocation UL)
        {
            if (UOT == unitOfTime.frames)
                return time;
            else //unitOfTime.seconds
                return secondsToFrames(time, UL);
        }

        static float secondsToFrames(float seconds, updateLocation UL)
        {
            if (UL == updateLocation.fixedUpdate)
                return (seconds / Time.fixedDeltaTime);
            else //updateLocation.Update
                return (seconds / Time.deltaTime);
        }

        static float euclideanDistance(float[] pointA, float[] pointB)
        {
            if (pointA.Length == pointB.Length)
            {
                float sum = 0;
                for (int dim = 0; dim < pointA.Length; dim++)
                    sum += Mathf.Pow((pointA[dim] - pointB[dim]), 2);
                return Mathf.Sqrt(sum); //distance is always positive
            }
            else
                return -1;
        }

        //-------------------------HELPER FUNCTIONS IN OTHER APIs I HAVE WRITTEN-------------------------

        public static float distance(Color color1, Color color2) //found in colorKit, colorDistances class [THIS IS MODIFIED]
        {
            float[] color1_Float_rGb = color_to_array(color1); 
            float[] color1_255_rGb = _float_to_255(color1_Float_rGb);

            float[] color2_Float_rGb = color_to_array(color2);
            float[] color2_255_rGb = _float_to_255(color2_Float_rGb);

            return distance(color1_255_rGb, color2_255_rGb);
        }

        static float distance(float[] color1, float[] color2) //found in colorKit, colorDistances class [THIS IS MODIFIED]
        {
            Vector3 color1Vect3 = array_to_vector3(color1);
            Vector3 color2Vect3 = array_to_vector3(color2);
            return Vector3.Distance(color1Vect3, color2Vect3);
        }

        public static Vector3 array_to_vector3(float[] array) //found in extraKit, formatConversion class [THIS IS MODIFIED]
        {
                return new Vector3(array[0], array[1], array[2]);
        }

        static float[] color_to_array(Color color) //found in extraKit, formatConversion class
        {
            return new float[] { color.r, color.g, color.b };
        }

        static float[] _float_to_255(float[] colorFloat) //found in extraKit, formatConversion class
        {
            float[] color255 = new float[colorFloat.Length];
            for (int i = 0; i < color255.Length; i++)
                color255[i] = _float_to_255(colorFloat[i]);
            return color255;
        }

        static float _float_to_255(float numFloat) //found in extraKit, formatConversion class
        {
            return Mathf.Clamp(numFloat * 255, 0, 255);
        }
    }
}