using UnityEngine;
using System.Collections;

/// <summary>
/// Inherit from this class to make any monobehavior include a singleton getter.
/// </summary>
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
	protected static T _instance;
	
	/**
      Returns the instance of this singleton.
   */
	public static T instance
	{
		get
		{
			if(_instance == null)
			{
				_instance = (T) FindObjectOfType(typeof(T));
				
				if (_instance == null && !GameController.isQuitting)
				{
					//Debug.LogError("An instance of " + typeof(T) + " is needed in the scene, but there is none.");
				}
			}
			
			return _instance;
		}
	}
	
	public void InitInstance() {
		if(_instance == null)
		{
			_instance = (T) FindObjectOfType(typeof(T));
			
			if (_instance == null && !GameController.isQuitting)
			{
				Debug.LogError("An instance of " + typeof(T) + 
				               " is needed in the scene, but there is none.");
			}
		}
		
	
	}
}
