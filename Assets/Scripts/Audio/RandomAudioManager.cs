using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class RandomAudioManager : MonoBehaviour {

	public AudioSource audioSource;
	
	public List<AudioClip> audioClips;
	
	void Awake() {
		if(!audioSource) audioSource = GetComponent<AudioSource>();
	}
	
	public void PlayRandom() {
		if(audioClips.Count < 1) return;
		audioSource.PlayOneShot(audioClips.PickRandom());
	}
}
