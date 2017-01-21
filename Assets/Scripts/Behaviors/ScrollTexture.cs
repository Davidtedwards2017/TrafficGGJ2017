using UnityEngine;
using System.Collections;

public class ScrollTexture : MonoBehaviour {
	
	public Vector2 scrollSpeed = Vector2.one;
	private Material mat;
	public bool unscaledTime = false;
	
	// Use this for initialization
	void Start () {
		mat = GetComponent<Renderer>().material;
	}
	
	// Update is called once per frame
	void Update () {
		
		if(!unscaledTime) {
			mat.mainTextureOffset += scrollSpeed * Time.deltaTime;
		} else {
			mat.mainTextureOffset += scrollSpeed * Time.unscaledDeltaTime;
		}
	}
}
