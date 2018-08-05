using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//last updated: 8/5/18

namespace lerpKit
{
    //  This toolKit can help you pass an appropiate 't' value for most functions that have a lerp (or linear interpolation) function
    //      and allow you to properly perform a linear interpolation
    //  Like: Mathf.Lerp, Vector2.Lerp, Vector3.Lerp, Vector4.Lerp, Color.Lerp, and Color32.Lerp
    //  But Not: 
    //  (1) Material.Lerp: it seems like you can do this by messing with only 1. Material.color 2. Material.mainTextureOffset and 3. Material.mainTextureScale
    //  (2) Quaternion.Lerp: this has no simple definition of "distance" and therefore I can not generate a lerpVelocity
    //  (3) Math.LerpAngle: i was not able to figure out how this work internally because in my version of unity its possible to get values greater than 360 and smaller than 0, which seems to go against its definition
    //  NOTE: Mathf.MoveTowards, Vector2.MoveTowards, Vector3.MoveTowards, Vector4.MoveTowards, and Mathf.MoveTowardsAngle might be better suited to your needs

    public static class lerpHelper
    {
        //-------------------------CALCULATE GUIDE DISTANCE-------------------------

        /// <summary>
        /// You only need to fill in the values that guide distance is asking for
        /// the other parameter must be filled in but will not affect the result
        /// NOTE: guideDistance.other has no definition for anything but color
        /// EX: IF (your passed GD == guideDistance.distBetween_StartAndEnd) -> currValue(s) will not be used
        ///     because GD is only asking for a startValue(s) and endValue(s)
        /// </summary>
        public static float calcGuideDistance(float startValue, float currValue, float endValue, guideDistance GD)
        {
            if (GD == guideDistance.distBetween_StartAndCurr)
                return Mathf.Abs(startValue - currValue);
            else if (GD == guideDistance.distBetween_StartAndEnd)
                return Mathf.Abs(startValue - endValue);
            else //guideDistance.distBetween_CurrAndEnd
                return Mathf.Abs(currValue - endValue);
        }

        /// <summary>
        /// You only need to fill in the values that guide distance is asking for
        /// the other parameter must be filled in but will not affect the result
        /// NOTE: guideDistance.other has no definition for anything but color
        /// EX: IF (your passed GD == guideDistance.distBetween_StartAndEnd) -> currValue(s) will not be used
        ///     because GD is only asking for a startValue(s) and endValue(s)
        /// </summary>
        public static float calcGuideDistance(Vector2 startVect2, Vector2 currVector2, Vector2 endVector2, guideDistance GD)
        {
            if (GD == guideDistance.distBetween_StartAndCurr)
                return Vector2.Distance(startVect2, currVector2);
            else if (GD == guideDistance.distBetween_StartAndEnd)
                return Vector2.Distance(startVect2, endVector2);
            else //guideDistance.distBetween_CurrAndEnd
                return Vector2.Distance(currVector2, endVector2);
        }

        /// <summary>
        /// You only need to fill in the values that guide distance is asking for
        /// the other parameter must be filled in but will not affect the result
        /// NOTE: guideDistance.other has no definition for anything but color
        /// EX: IF (your passed GD == guideDistance.distBetween_StartAndEnd) -> currValue(s) will not be used
        ///     because GD is only asking for a startValue(s) and endValue(s)
        /// </summary>
        public static float calcGuideDistance(Vector3 startVect3, Vector3 currVector3, Vector3 endVector3, guideDistance GD)
        {
            if (GD == guideDistance.distBetween_StartAndCurr)
                return Vector3.Distance(startVect3, currVector3);
            else if (GD == guideDistance.distBetween_StartAndEnd)
                return Vector3.Distance(startVect3, endVector3);
            else //guideDistance.distBetween_CurrAndEnd
                return Vector3.Distance(currVector3, endVector3);
        }

        /// <summary>
        /// You only need to fill in the values that guide distance is asking for
        /// the other parameter must be filled in but will not affect the result
        /// NOTE: guideDistance.other has no definition for anything but color
        /// EX: IF (your passed GD == guideDistance.distBetween_StartAndEnd) -> currValue(s) will not be used
        ///     because GD is only asking for a startValue(s) and endValue(s)
        /// </summary>
        public static float calcGuideDistance(Vector4 startVect4, Vector4 currVector4, Vector4 endVector4, guideDistance GD)
        {
            if (GD == guideDistance.distBetween_StartAndCurr)
                return Vector4.Distance(startVect4, currVector4);
            else if (GD == guideDistance.distBetween_StartAndEnd)
                return Vector4.Distance(startVect4, endVector4);
            else //guideDistance.distBetween_CurrAndEnd
                return Vector4.Distance(currVector4, endVector4);
        }

        /// <summary>
        /// You only need to fill in the values that guide distance is asking for
        /// the other parameter must be filled in but will not affect the result
        /// NOTE: guideDistance.other has no definition for anything but color
        /// EX: IF (your passed GD == guideDistance.distBetween_StartAndEnd) -> currValue(s) will not be used
        ///     because GD is only asking for a startValue(s) and endValue(s)
        /// </summary>
        public static float calcGuideDistance(float[] startValues, float[] currValues, float[] endValues, guideDistance GD)
        {
            if (GD == guideDistance.distBetween_StartAndCurr)
                return euclideanDistance(startValues, currValues);
            else if (GD == guideDistance.distBetween_StartAndEnd)
                return euclideanDistance(startValues, endValues);
            else //guideDistance.distBetween_CurrAndEnd
                return euclideanDistance(currValues, endValues);
        }

        /// <summary>
        /// You only need to fill in the values that guide distance is asking for
        /// the other parameter must be filled in but will not affect the result
        /// NOTE: guideDistance.other has no definition for anything but color
        /// EX: IF (your passed GD == guideDistance.distBetween_StartAndEnd) -> currValue(s) will not be used
        ///     because GD is only asking for a startValue(s) and endValue(s)
        /// </summary>
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

        public static float calcLerpValue(float currValue, float endValue, float guideDistance, float guideTime, unitOfTime UOT_GD, updateLocation UL)
        {
            return calcLerpValue(currValue, endValue, calcLerpVelocity(guideDistance, guideTime, UOT_GD, UL));
        }

        public static float calcLerpValue(Vector2 currVector2, Vector2 endVector2, float guideDistance, float guideTime, unitOfTime UOT_GD, updateLocation UL)
        {
            return calcLerpValue(currVector2, endVector2, calcLerpVelocity(guideDistance, guideTime, UOT_GD, UL));
        }

        public static float calcLerpValue(Vector3 currVector3, Vector3 endVector3, float guideDistance, float guideTime, unitOfTime UOT_GD, updateLocation UL)
        {
            return calcLerpValue(currVector3, endVector3, calcLerpVelocity(guideDistance, guideTime, UOT_GD, UL));
        }

        public static float calcLerpValue(Vector4 currVector4, Vector4 endVector4, float guideDistance, float guideTime, unitOfTime UOT_GD, updateLocation UL)
        {
            return calcLerpValue(currVector4, endVector4, calcLerpVelocity(guideDistance, guideTime, UOT_GD, UL));
        }

        public static float calcLerpValue(float[] currValues, float[] endValues, float guideDistance, float guideTime, unitOfTime UOT_GD, updateLocation UL)
        {
            return calcLerpValue(currValues, endValues, calcLerpVelocity(guideDistance, guideTime, UOT_GD, UL));
        }

        public static float calcLerpValue(Color currColor, Color endColor, float guideDistance, float guideTime, unitOfTime UOT_GD, updateLocation UL)
        {
            return calcLerpValue(currColor, endColor, calcLerpVelocity(guideDistance, guideTime, UOT_GD, UL));
        }

        //-------------------------CALCULATE LERP VALUE (using Guide Velocity)-------------------------

        public static float calcLerpValue(float currValue, float endValue, float lerpVelocity_DperF)
        {
            //---calc distance left to travel
            float distToFinish = Mathf.Abs(currValue - endValue);

            //--- calc lerp value based on this
            return Mathf.Clamp((lerpVelocity_DperF / distToFinish), 0, 1);
        }

        public static float calcLerpValue(Vector2 currVector2, Vector2 endVector2, float lerpVelocity_DperF)
        {
            //---calc distance left to travel
            float distToFinish = Vector2.Distance(currVector2, endVector2);

            //--- calc lerp value based on this
            return Mathf.Clamp((lerpVelocity_DperF / distToFinish), 0, 1);
        }

        public static float calcLerpValue(Vector3 currVector3, Vector3 endVector3, float lerpVelocity_DperF)
        {
            //---calc distance left to travel
            float distToFinish = Vector3.Distance(currVector3, endVector3);

            //--- calc lerp value based on this
            return Mathf.Clamp((lerpVelocity_DperF / distToFinish), 0, 1);
        }

        public static float calcLerpValue(Vector4 currVector4, Vector4 endVector4, float lerpVelocity_DperF)
        {
            //---calc distance left to travel
            float distToFinish = Vector4.Distance(currVector4, endVector4);

            //--- calc lerp value based on this
            return Mathf.Clamp((lerpVelocity_DperF / distToFinish), 0, 1);
        }

        public static float calcLerpValue(float[] currValues, float[] endValues, float lerpVelocity_DperF)
        {
            //---calc distance left to travel
            float distToFinish = euclideanDistance(currValues, endValues);

            //--- calc lerp value based on this
            return Mathf.Clamp((lerpVelocity_DperF / distToFinish), 0, 1);
        }

        public static float calcLerpValue(Color currColor, Color endColor, float lerpVelocity_DperF)
        {
            //---calc distance left to travel
            float distToFinish = distance(currColor, endColor);

            //--- calc lerp value based on this
            return Mathf.Clamp((lerpVelocity_DperF / distToFinish), 0, 1);
        }

        //-------------------------VELOCITY FUNCTIONS-------------------------

        public static float calcLerpVelocity(float guideDistance, float timeToTravel_GD, unitOfTime UOT_GD, updateLocation UL)
        {
            return calcLerpVelocity(guideDistance, timeToFrames(timeToTravel_GD, UOT_GD, UL));
        }

        public static float calcLerpVelocity(float guideDistance, float framesToTravel_GD) //frames of this type (update if we are in update... fixed update if we are in fixed upate)
        {
            return guideDistance / framesToTravel_GD;
        }

        //-------------------------HELPER FUNCTIONS-------------------------

        public static float timeToFrames(float time, unitOfTime UOT, updateLocation UL) //Prefably calculate velocity and dont use this function directly
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

        //-------------------------HELPER FUNCTIONS IN OTHER APIs I HAVE WRITTEN [WITH MODIFICATIONS]-------------------------

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

        static Vector3 array_to_vector3(float[] array) //found in extraKit, formatConversion class [THIS IS MODIFIED]
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