using UnityEngine;
using System.Collections;

public class DestroyOnLevelLoadBehavior : MonoBehaviour {
	
	void OnLevelWasLoaded(int level) {
		Destroy(gameObject);
	}
}
