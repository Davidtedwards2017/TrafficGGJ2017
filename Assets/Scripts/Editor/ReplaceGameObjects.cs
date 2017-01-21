using UnityEngine;
using UnityEditor;
using System.Collections;

public class ReplaceGameObjects : ScriptableWizard
{
	public bool copyValues = true;
	public GameObject ReplacementPrefab;
	public GameObject[] OldObjects;
	
	[MenuItem("Custom/Replace GameObjects")]
	
	
	static void CreateWizard()
	{
		ScriptableWizard.DisplayWizard("Replace GameObjects", typeof(ReplaceGameObjects), "Replace");
	}
	
	void OnWizardCreate()
	{
		foreach (GameObject oldObject in OldObjects)
		{
			GameObject newObject;
			newObject = (GameObject)PrefabUtility.InstantiatePrefab(ReplacementPrefab);
			
			newObject.transform.position = oldObject.transform.position;
			newObject.transform.rotation = oldObject.transform.rotation;
			newObject.transform.localScale = oldObject.transform.localScale;
			newObject.transform.parent = oldObject.transform.parent;
			
			DestroyImmediate(oldObject);
			
		}
		
	}
}