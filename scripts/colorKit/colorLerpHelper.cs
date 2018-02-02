using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using lerpKit;

namespace colorKit
{
    public static class colorLerpHelper
    {
        public static float calcGuideDistance(Color startColor, Color currColor, Color endColor, colorSpace CS, guideDistance GD)
        {

            switch (CS)
            {
                case colorSpace.RGB:

                    if (GD == guideDistance.distBetween_StartAndCurr)
                        return colorDistances.distBetweenColors(startColor, currColor, colorSpace.RGB);
                    else if (GD == guideDistance.distBetween_StartAndEnd)
                        return colorDistances.distBetweenColors(startColor, endColor, colorSpace.RGB);
                    else if (GD == guideDistance.distBetween_CurrAndEnd)
                        return colorDistances.distBetweenColors(currColor, endColor, colorSpace.RGB);
                    else
                        return 441.672956f; // maxDistanceInRGBColorSpace
                case colorSpace.RYB:

                    if (GD == guideDistance.distBetween_StartAndCurr)
                        return colorDistances.distBetweenColors(startColor, currColor, colorSpace.RYB);
                    else if (GD == guideDistance.distBetween_StartAndEnd)
                        return colorDistances.distBetweenColors(startColor, endColor, colorSpace.RYB);
                    else if (GD == guideDistance.distBetween_CurrAndEnd)
                        return colorDistances.distBetweenColors(currColor, endColor, colorSpace.RYB);
                    else
                        return 441.672956f; //maxDistanceInRYBColorSpace
                default:

                    if (GD == guideDistance.distBetween_StartAndCurr)
                        return colorDistances.distBetweenColors(startColor, currColor, colorSpace.CMYK);
                    else if (GD == guideDistance.distBetween_StartAndEnd)
                        return colorDistances.distBetweenColors(startColor, endColor, colorSpace.CMYK);
                    else if (GD == guideDistance.distBetween_CurrAndEnd)
                        return colorDistances.distBetweenColors(currColor, endColor, colorSpace.CMYK);
                    else
                        return 255; //maxDistanceInCMYKColorSpace (because we have no accurate representation for 4D distance)
            }
        }
        
        public static float calcLerpValue(Color startColor, Color currColor, Color endColor, float guideDistance, float guideTime, unitOfTime UOT_GD, updateLocation UL, colorSpace CS)
        {
            return calcLerpValue(startColor, currColor, endColor, calcLerpVelocity(guideDistance, guideTime, UOT_GD, UL), CS);
        }

        public static float calcLerpValue(Color startColor, Color currColor, Color endColor, float lerpVelocity_DperF, colorSpace CS)
        {
            //---calc distance left to travel
            float distToFinish = 0;
            switch (CS)
            {
                case colorSpace.RGB:
                    distToFinish = colorDistances.distBetweenColors(currColor, endColor, colorSpace.RGB);
                    break;
                case colorSpace.RYB:
                    distToFinish = colorDistances.distBetweenColors(currColor, endColor, colorSpace.RYB);
                    break;
                default:
                    distToFinish = colorDistances.distBetweenColors(currColor, endColor, colorSpace.CMYK);
                    break;
            }

            //--- calc lerp value based on this
            return Mathf.Clamp((lerpVelocity_DperF / distToFinish), 0, 1);
        }

        //-------------------------CALCULATE LERP VELOCITY-------------------------(NOTE: these functions are also found in colorKit)

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
    }
}