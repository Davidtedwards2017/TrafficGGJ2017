using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using System.ComponentModel;
using System.Reflection;
using System.Text.RegularExpressions;

public static class Extensions {

	public static bool IsSubsetOf<T>(this List<T> coll1, List<T> coll2)
	{
		bool isSubset = !coll1.Except(coll2).Any();
		return isSubset;
	}
	
	public static bool IsSubsetOf<T>(this T[] coll1, T[]coll2)
	{
		bool isSubset = !coll1.Except(coll2).Any();
		return isSubset;
	}	

//	public static T RandomElement<T>(this IEnumerable<T> source)
//	{
//		//return source.RandomElements(1).Single();
//		
//	}
//	
//	public static Text RandomElement<T>(this List<T> source) {
//		return source.Shuffle()[0];
//	}
	
//	public static IEnumerable<T> RandomElements<T>(this IEnumerable<T> source, int count)
//	{
//		return source.Shuffle().Take(count);
//	}
//	
//	public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
//	{
//		return source.Shuffle();
//	}

	public static T PickRandom<T>(this IEnumerable<T> source)
	{
		return source.PickRandom(1).Single();
	}
	
	public static IEnumerable<T> PickRandom<T>(this IEnumerable<T> source, int count)
	{
		return source.Shuffle().Take(count);
	}
	
	public static T PopAt<T>(this List<T> list, int index)
	{
		T r = list[index];
		list.RemoveAt(index);
		return r;
	}		
	
	public static T PopRandom<T>(this List<T> list)
	{
		
		T r = list.PickRandom();
		list.Remove(r);
		return r;
	}	
				
	/// <summary>
	///SomeEnum parsedState;
	///if (parsedState.TryParse<SomeEnum>(aString, out parsedState))
	///{
	///	/* The parsed string matches an enum state, proceed successfully here. */
	///}
	///else
	///{
	///	/* The parsed  string does not match any enum state, handle failure here. */
	///}
	/// </summary>
	/// <returns><c>true</c>, if parse was tryed, <c>false</c> otherwise.</returns>
	/// <param name="theEnum">The enum.</param>
	/// <param name="valueToParse">Value to parse.</param>
	/// <param name="returnValue">Return value.</param>
	/// <typeparam name="T">The 1st type parameter.</typeparam>
	public static bool TryParse<T>(this Enum theEnum, string valueToParse, out T returnValue)
	{
		returnValue = default(T);    
		if (Enum.IsDefined(typeof(T), valueToParse))
		{
			TypeConverter converter = TypeDescriptor.GetConverter(typeof(T));
			returnValue = (T)converter.ConvertFromString(valueToParse);
			return true;
		}
		return false;
	}	
		
	private static Regex UpperCamelCaseRegex = new Regex(@"(?<!^)((?<!\d)\d|(?(?<=[A-Z])[A-Z](?=[a-z])|[A-Z]))", RegexOptions.Compiled);
	
	public static string AsUpperCamelCaseName(this Enum e)
	{
		return UpperCamelCaseRegex.Replace(e.ToString(), " $1");
		
	}		
		
	public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
	{
		return source.OrderBy(x => System.Guid.NewGuid());
	}

	public static void Shuffle<T>(this List<T> list)
	{
		for(var i=0; i < list.Count; i++)
			list.Swap(i, UnityEngine.Random.Range(i, list.Count));
	}
	
	public static void Swap<T>(this List<T> list, int i, int j)
	{
		var temp = list[i];
		list[i] = list[j];
		list[j] = temp;
	}
	
	public static void DestroyChildren(this Transform root) {
		int childCount = root.childCount;
		for (int i=0; i<childCount; i++) {
			GameObject.Destroy(root.GetChild(0).gameObject);
		}
	}	
	
	public static Vector3 ToXZ(this Vector2 vector2) {

		Vector3 vector3 = new Vector3(vector2.x, 0f, vector2.y);
		return vector3;
	}	
	/// <summary>
	/// Sets the specified group's alpha to one.
	/// </summary>
	/// <param name="group">Group.</param>
	public static void Show(this CanvasGroup group) {
		group.alpha = 1f;
	}
	
	/// <summary>
	/// Sets the specified group's alpha to zero.
	/// </summary>
	/// <param name="group">Group.</param>
	public static void Hide(this CanvasGroup group) {
		group.alpha = 0f;
	}
	
	public static void SetInteractive(this CanvasGroup group, bool value, float duration = 0f) {

		if(value == true) {
			if(duration == 0f) {
				group.alpha = 1f;
				group.interactable = true;
				group.blocksRaycasts = true;	
			} else {                               
				GameController.instance.StartCoroutine(FadeIn(group, duration));
			}
		} else {
			if(duration == 0f) {
				group.alpha = 0f;
				group.interactable = false;
				group.blocksRaycasts = false;	
			} else {                
                GameController.instance.StartCoroutine(FadeOut(group, duration));
			}			
		}		
	}	
	
	public static IEnumerator FadeIn(CanvasGroup group, float duration) {
				
		float timeElapsed = (group.alpha / 1.0f) * duration;
		
		while(timeElapsed <= duration) {
			group.alpha = Interpolate.Linear(0.135f, 1.0f, timeElapsed, duration); 
			timeElapsed += Time.unscaledDeltaTime;
			yield return null;
		}
		
		group.interactable = true;
		group.blocksRaycasts = true;
		group.alpha = 1f;
	}	
	
	public static IEnumerator FadeOut(CanvasGroup group, float duration) {
	
		group.interactable = false;
		group.blocksRaycasts = false;
				
		float timeElapsed = AICore.IsItMin(group.alpha, 0f, 1.0f) * duration;
	
		while(timeElapsed <= duration) {
			group.alpha = 1.0f - Interpolate.Linear(0.135f, 1.0f, timeElapsed, duration);
            timeElapsed += Time.unscaledDeltaTime;
            yield return null;
		}
		group.alpha = 0f;
	}

	public static string UpperCaseWords(this string value) {

			char[] array = value.ToCharArray();
			// Handle the first letter in the string.
			if (array.Length >= 1)
			{
				if (char.IsLower(array[0]))
				{
					array[0] = char.ToUpper(array[0]);
				}
			}
			// Scan through the letters, checking for spaces.
			// ... Uppercase the lowercase letters following spaces.
			for (int i = 1; i < array.Length; i++)
			{
				if (array[i - 1] == ' ')
				{
					if (char.IsLower(array[i]))
					{
						array[i] = char.ToUpper(array[i]);
					}
				}
			}
			return new string(array);
		
	}

    public static void MatchValues(this Slider slider, MinMaxInt minMaxInt)
    {
        slider.minValue = minMaxInt.min;
        slider.maxValue = minMaxInt.max;
        slider.value = minMaxInt.value;
    }

    public static void MatchValues(this Slider slider, MinMaxFloat minMaxFloat)
    {
        slider.minValue = minMaxFloat.min;
        slider.maxValue = minMaxFloat.max;
        slider.value = minMaxFloat.value;
    }


    //Resets a transform's local transformation values
    public static void Reset(this Transform transform)
    {
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        transform.localScale = Vector3.one;
    }

    //	/* Return 1 if arr2[] is a subset of arr1[] */
    //	public static bool isSubset(int[] arr1, int[] arr2)
    //	{
    //
    //		for (int i = 0; i < arr2.length; i++)
    //		{
    //			for (j = 0; j<m; j++)
    //			{
    //				if(arr2[i] == arr1[j])
    //					break;
    //			}
    //			
    //			/* If the above inner loop was not broken at all then
    //           arr2[i] is not present in arr1[] */
    //			if (j == m)
    //				return false;
    //		}
    //		
    //		/* If we reach here then all elements of arr2[] 
    //      are present in arr1[] */
    //		return true;
    //	}	


}
