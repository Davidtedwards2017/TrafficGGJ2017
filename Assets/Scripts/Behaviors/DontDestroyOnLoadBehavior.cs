﻿using UnityEngine;
using System.Collections;

public class DontDestroyOnLoadBehavior : MonoBehaviour {

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad(this.gameObject);
	}
}
