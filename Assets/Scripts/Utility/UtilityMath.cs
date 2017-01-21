using System;
using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

public static class UtilityMath {

	public static float Remap (this float value, float from1, float to1, float from2, float to2)
	{
		return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
	}
	
//	public static float RemapInverse (this float value, float from1, float to1, float from2, float to2) {
//		//return Mathf.Abs(value - to1) / (to1 - from1) * (to2 - from2) + from2;
//		
//		return AICore._IsItMax(value, to1, from1) * (to2 - from2) + from2;
//	}
	
	public static float SignedAngleBetween(Vector3 a, Vector3 b, Vector3 n){
		// angle in [0,180]
		float angle = Vector3.Angle(a,b);
		float sign = Mathf.Sign(Vector3.Dot(n,Vector3.Cross(a,b)));
		
		// angle in [-179,180]
		float signed_angle = angle * sign;
		
		// angle in [0,360] (not used but included here for completeness)
		//float angle360 =  (signed_angle + 180) % 360;
		
		return signed_angle;
	}
		
	/// <summary>
	/// flexible function to force natural growth of player power
	/// <summary>
	public static float GeneralizedLogisticFunction (float t, float lowerAsympotote = 0, float upperAsymptote = 1, float growthRate = 1, float maximumGrowthPosition = 1, float Q = 1)
	{
		float A = lowerAsympotote;
		float K = upperAsymptote;
		float B = growthRate;
		float v = maximumGrowthPosition;
		
		return A + (K - A) / Mathf.Pow(1f + Q * Mathf.Exp(-B * t), 1f / v);
	}

    public static Vector2 PointOnCircle(Vector3 center, float radius, float angle = 0f)
    {

        //Quaternion.AngleAxis(angle, Vector3.up);

        Vector2 pos;
        pos.x = center.x + radius * Mathf.Sin(angle * Mathf.Deg2Rad);
        pos.y = center.y + radius * Mathf.Cos(angle * Mathf.Deg2Rad);
        return pos;
        


    }
}
