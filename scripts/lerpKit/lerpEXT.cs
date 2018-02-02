using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using lerpKit;

namespace lerpKit
{
    public enum updateLocation { fixedUpdate, Update };
    public enum unitOfTime { frames, seconds };
    public enum guideDistance { distBetween_Other, distBetween_StartAndEnd, distBetween_CurrAndEnd, distBetween_StartAndCurr };
}

public static class lerpEXT{

    //-----FUNCTION VERSIONS that use a dummy (non used) instance of 'whatever type you are lerping between'

    #region Calculate Guide Distance

    public static float calcGuideDistance(this float f, guideDistance GD, float startValue, float currValue, float endValue)
    {
        return lerpHelper.calcGuideDistance(startValue, currValue, endValue, GD);
    }

    public static float calcGuideDistance(this Vector2 v2, guideDistance GD, Vector2 startVect2, Vector2 currVector2, Vector2 endVector2)
    {
        return lerpHelper.calcGuideDistance(startVect2, currVector2, endVector2, GD);
    }

    public static float calcGuideDistance(this Vector3 v3, guideDistance GD, Vector3 startVect3, Vector3 currVector3, Vector3 endVector3)
    {
        return lerpHelper.calcGuideDistance(startVect3, currVector3, endVector3, GD);
    }

    public static float calcGuideDistance(this Vector4 v4, guideDistance GD, Vector4 startVect4, Vector4 currVector4, Vector4 endVector4)
    {
        return lerpHelper.calcGuideDistance(startVect4, currVector4, endVector4, GD);
    }

    public static float calcGuideDistance(this float[] fa, guideDistance GD, float[] startValues, float[] currValues, float[] endValues)
    {
        return lerpHelper.calcGuideDistance(startValues, currValues, endValues, GD);
    }

    public static float calcGuideDistance(this Color c, Color startColor, Color currColor, Color endColor, guideDistance GD)
    {
        return lerpHelper.calcGuideDistance(startColor, currColor, endColor, GD);
    }

    #endregion

    #region Calculate Lerp Value (with Velocity)

    public static float calcLerpValue(this float f, float startValue, float currValue, float endValue, float lerpVelocity_DperF)
    {
        return lerpHelper.calcLerpValue(startValue, currValue, endValue, lerpVelocity_DperF);
    }

    public static float calcLerpValue(this Vector2 v2, Vector2 startVector2, Vector2 currVector2, Vector2 endVector2, float lerpVelocity_DperF)
    {
        return lerpHelper.calcLerpValue(startVector2, currVector2, endVector2, lerpVelocity_DperF);
    }

    public static float calcLerpValue(this Vector3 v3, Vector3 startVector3, Vector3 currVector3, Vector3 endVector3, float lerpVelocity_DperF)
    {
        return lerpHelper.calcLerpValue(startVector3, currVector3, endVector3, lerpVelocity_DperF);
    }

    public static float calcLerpValue(this Vector4 v4, Vector4 startVector4, Vector4 currVector4, Vector4 endVector4, float lerpVelocity_DperF)
    {
        return lerpHelper.calcLerpValue(startVector4, currVector4, endVector4, lerpVelocity_DperF);
    }

    public static float calcLerpValue(this float[] fa, float[] startValues, float[] currValues, float[] endValues, float lerpVelocity_DperF)
    {
        return lerpHelper.calcLerpValue(startValues, currValues, endValues, lerpVelocity_DperF);
    }

    public static float calcLerpValue(this Color c, Color startColor, Color currColor, Color endColor, float lerpVelocity_DperF)
    {
        return lerpHelper.calcLerpValue(startColor, currColor, endColor, lerpVelocity_DperF);
    }

    #endregion

    #region Calculate Lerp Value (with Distance, Time, Unit of Time, and Update Location)

    public static float calcLerpValue(this float f, float startValue, float currValue, float endValue, float guideDistance, float guideTime, unitOfTime UOT_GD, updateLocation UL)
    {
        return lerpHelper.calcLerpValue(startValue, currValue, endValue, guideDistance, guideTime, UOT_GD, UL);
    }

    public static float calcLerpValue(this Vector2 v2, Vector2 startVector2, Vector2 currVector2, Vector2 endVector2, float guideDistance, float guideTime, unitOfTime UOT_GD, updateLocation UL)
    {
        return lerpHelper.calcLerpValue(startVector2, currVector2, endVector2, guideDistance, guideTime, UOT_GD, UL);
    }

    public static float calcLerpValue(this Vector3 v3, Vector3 startVector3, Vector3 currVector3, Vector3 endVector3, float guideDistance, float guideTime, unitOfTime UOT_GD, updateLocation UL)
    {
        return lerpHelper.calcLerpValue(startVector3, currVector3, endVector3, guideDistance, guideTime, UOT_GD, UL);
    }

    public static float calcLerpValue(this Vector4 v4, Vector4 startVector4, Vector4 currVector4, Vector4 endVector4, float guideDistance, float guideTime, unitOfTime UOT_GD, updateLocation UL)
    {
        return lerpHelper.calcLerpValue(startVector4, currVector4, endVector4, guideDistance, guideTime, UOT_GD, UL);
    }

    public static float calcLerpValue(this float[] fa, float[] startValues, float[] currValues, float[] endValues, float guideDistance, float guideTime, unitOfTime UOT_GD, updateLocation UL)
    {
        return lerpHelper.calcLerpValue(startValues, currValues, endValues, guideDistance, guideTime, UOT_GD, UL);
    }

    public static float calcLerpValue(this Color c, Color startColor, Color currColor, Color endColor, float guideDistance, float guideTime, unitOfTime UOT_GD, updateLocation UL)
    {
        return lerpHelper.calcLerpValue(startColor, currColor, endColor, guideDistance, guideTime, UOT_GD, UL);
    }

    #endregion

    public static float distance(this Color c, Color color1, Color color2)
    {
        return lerpHelper.distance(color1, color2);
    }

    //-----FUNCTION VERSIONS that use the instance of whatever type they extend (the instance will be of the same type as the first parameter)

    #region Calculate Guide Distance

    public static float calcGuideDistance(this float startValue, float currValue, float endValue, guideDistance GD)
    {
        return lerpHelper.calcGuideDistance(startValue, currValue, endValue, GD);
    }

    public static float calcGuideDistance(this Vector2 startVect2, Vector2 currVector2, Vector2 endVector2, guideDistance GD)
    {
        return lerpHelper.calcGuideDistance(startVect2, currVector2, endVector2, GD);
    }

    public static float calcGuideDistance(this Vector3 startVect3, Vector3 currVector3, Vector3 endVector3, guideDistance GD)
    {
        return lerpHelper.calcGuideDistance(startVect3, currVector3, endVector3, GD);
    }

    public static float calcGuideDistance(this Vector4 startVect4, Vector4 currVector4, Vector4 endVector4, guideDistance GD)
    {
        return lerpHelper.calcGuideDistance(startVect4, currVector4, endVector4, GD);
    }

    public static float calcGuideDistance(this float[] startValues, float[] currValues, float[] endValues, guideDistance GD)
    {
        return lerpHelper.calcGuideDistance(startValues, currValues, endValues, GD);
    }

    public static float calcGuideDistance(this Color startColor, Color currColor, Color endColor, guideDistance GD)
    {
        return lerpHelper.calcGuideDistance(startColor, currColor, endColor, GD);
    }

    #endregion

    #region Calculate Lerp Value (with Velocity)

    public static float calcLerpValue(this float startValue, float currValue, float endValue, float lerpVelocity_DperF)
    {
        return lerpHelper.calcLerpValue(startValue, currValue, endValue, lerpVelocity_DperF);
    }

    public static float calcLerpValue(this Vector2 startVector2, Vector2 currVector2, Vector2 endVector2, float lerpVelocity_DperF)
    {
        return lerpHelper.calcLerpValue(startVector2, currVector2, endVector2, lerpVelocity_DperF);
    }

    public static float calcLerpValue(this Vector3 startVector3, Vector3 currVector3, Vector3 endVector3, float lerpVelocity_DperF)
    {
        return lerpHelper.calcLerpValue(startVector3, currVector3, endVector3, lerpVelocity_DperF);
    }

    public static float calcLerpValue(this Vector4 startVector4, Vector4 currVector4, Vector4 endVector4, float lerpVelocity_DperF)
    {
        return lerpHelper.calcLerpValue(startVector4, currVector4, endVector4, lerpVelocity_DperF);
    }

    public static float calcLerpValue(this float[] startValues, float[] currValues, float[] endValues, float lerpVelocity_DperF)
    {
        return lerpHelper.calcLerpValue(startValues, currValues, endValues, lerpVelocity_DperF);
    }

    public static float calcLerpValue(this Color startColor, Color currColor, Color endColor, float lerpVelocity_DperF)
    {
        return lerpHelper.calcLerpValue(startColor, currColor, endColor, lerpVelocity_DperF);
    }

    #endregion

    #region Calculate Lerp Value (with Distance, Time, Unit of Time, and Update Location)

    public static float calcLerpValue(this float startValue, float currValue, float endValue, float guideDistance, float guideTime, unitOfTime UOT_GD, updateLocation UL)
    {
        return lerpHelper.calcLerpValue(startValue, currValue, endValue, guideDistance, guideTime, UOT_GD, UL);
    }

    public static float calcLerpValue(this Vector2 startVector2, Vector2 currVector2, Vector2 endVector2, float guideDistance, float guideTime, unitOfTime UOT_GD, updateLocation UL)
    {
        return lerpHelper.calcLerpValue(startVector2, currVector2, endVector2, guideDistance, guideTime, UOT_GD, UL);
    }

    public static float calcLerpValue(this Vector3 startVector3, Vector3 currVector3, Vector3 endVector3, float guideDistance, float guideTime, unitOfTime UOT_GD, updateLocation UL)
    {
        return lerpHelper.calcLerpValue(startVector3, currVector3, endVector3, guideDistance, guideTime, UOT_GD, UL);
    }

    public static float calcLerpValue(this Vector4 startVector4, Vector4 currVector4, Vector4 endVector4, float guideDistance, float guideTime, unitOfTime UOT_GD, updateLocation UL)
    {
        return lerpHelper.calcLerpValue(startVector4, currVector4, endVector4, guideDistance, guideTime, UOT_GD, UL);
    }

    public static float calcLerpValue(this float[] startValues, float[] currValues, float[] endValues, float guideDistance, float guideTime, unitOfTime UOT_GD, updateLocation UL)
    {
        return lerpHelper.calcLerpValue(startValues, currValues, endValues, guideDistance, guideTime, UOT_GD, UL);
    }

    public static float calcLerpValue(this Color startColor, Color currColor, Color endColor, float guideDistance, float guideTime, unitOfTime UOT_GD, updateLocation UL)
    {
        return lerpHelper.calcLerpValue(startColor, currColor, endColor, guideDistance, guideTime, UOT_GD, UL);
    }

    #endregion

    public static float distance(this Color color1, Color color2)
    {
        return lerpHelper.distance(color1, color2);
    }
}
