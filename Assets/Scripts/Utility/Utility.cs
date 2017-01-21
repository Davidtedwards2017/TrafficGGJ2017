using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System;


public static class Utility {

	public static bool MouseIsMoving () {
		if(Mathf.Abs (Input.GetAxis("Mouse X")) > 0f) return true;
		if(Mathf.Abs (Input.GetAxis("Mouse Y")) > 0f) return true;
		return false;
	}	

	public static void SetLayerOnAllRecursive(GameObject obj, int layer) {
		if(obj == null) {
			Debug.Log("No ship model in start function.");
			return;
		}
		obj.layer = layer;
		foreach (Transform child in obj.transform) {
			SetLayerOnAllRecursive(child.gameObject, layer);
		}
	}
	
	public static bool UnscaledTimer(ref float seconds) {
		seconds -= Time.unscaledDeltaTime;
		if(seconds < 0f) return false;
		return true;
	}	
	
	public static string ConvertToTimerString(float timeRemaining) {

		int minutes = Mathf.FloorToInt(timeRemaining / 60f);
		int seconds = Mathf.FloorToInt(timeRemaining - minutes * 60);
		return String.Format("{0:0}:{1:00}", minutes, seconds);

        
	}

    //Dictionary of Enum to int
    public class Counter<T> : Dictionary<T, int>
    {
        public Counter()
        {
            if (!typeof(T).IsEnum) throw new ArgumentException("Type must be an enum");

            foreach (var jk in Enum.GetValues(typeof(T)))
            {
                Add((T)jk, 0);
            }
        }
    }
    //Dictionary of Enum to MinMaxInt that auto populates Enum keys
    public class MinMaxIntCounter<T> : Dictionary<T, MinMaxInt>
    {
        public MinMaxIntCounter(int min, int max, int value)
        {
            if (!typeof(T).IsEnum) throw new ArgumentException("Type must be an enum");

            foreach (var jk in Enum.GetValues(typeof(T)))
            {
                Add((T)jk, new MinMaxInt(min, max, value));
            }
        }
    }

    //Dictionary of Enum to MinMaxInt that auto populates Enum keys
    public class MinMaxFloatCounter<T> : Dictionary<T, MinMaxFloat>
    {
        public MinMaxFloatCounter(float min, float max, float value)
        {
            if (!typeof(T).IsEnum) throw new ArgumentException("Type must be an enum");

            foreach (var jk in Enum.GetValues(typeof(T)))
            {
                Add((T)jk, new MinMaxFloat(min, max, value));
            }
        }
    }

    public class TranslateDictionary<TKey, TValue>  where TValue : class
	{
		public bool Initialized { get; set; }
		private Dictionary<TKey, TValue> Dictionary = new Dictionary<TKey, TValue>();
		public TValue this[TKey key]
		{
			get
			{
				if (!Initialized)
				{
					if (!Dictionary.ContainsKey(key))
					{
						Dictionary.Add(key, Activator.CreateInstance<TValue>());
					}
				} 
					
				TValue value;
				
				return Dictionary.TryGetValue(key, out value) ? value : null;
				
				//return Dictionary[key];
				
			}
			set
			{
				Dictionary[key] = value;
			}
		}
		
		public TValue RandomElement() {
			return Dictionary.Values.PickRandom(); 
		}
	}

	
	public static void DestroyAllGameObjects()
	{
		GameObject[] GameObjects = (GameObject.FindObjectsOfType<GameObject>() as GameObject[]);
		
		for (int i = 0; i < GameObjects.Length; i++)
		{
			GameObject.Destroy(GameObjects[i]);
		}
	}		
	
	
	public static void SetColor (Color color, Transform transform, bool setChildren = true)
	{
		if (setChildren)
		{
			Renderer[] renderers = transform.GetComponentsInChildren<Renderer>();
			for (int i = 0; i < renderers.Length; i++)
			{
				renderers[i].material.SetColor("_EmissionColor", color);
			}
		} else {
			transform.GetComponent<Renderer>().material.SetColor("_EmissionColor", color);
		}
	}
	
    //Adds a UI panel as a child to another panel, getting rid of any position, rotation or scale values
    //This allows you to quickly add instantiated UI panels to parents with Layout Group components so that
    //they appear correctly.
	public static void AddChildUI(Transform parent, Transform child) {
		child.transform.SetParent(parent, false);
        child.transform.Reset();
	}

    public static bool RaycastPointerToLayers(out RaycastHit hit, params string[] layersToCheck) {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        int layerMask = 0;

        if (layersToCheck.Length > 0)
        {
            for (int n = 0; n < layersToCheck.Length; n++)
            {
                layerMask |= (1 <<LayerMask.NameToLayer(layersToCheck[n]));
            }
        }

        if(Physics.Raycast(ray, out hit, 3000, layerMask)) {
            return true;	
        }
        return false;
    }
}

public static class KeyValuePair
{
	public static KeyValuePair<TKey, TValue> Create<TKey, TValue>(TKey key, TValue value)
	{
		return new KeyValuePair<TKey, TValue>(key, value);
	}
}	
