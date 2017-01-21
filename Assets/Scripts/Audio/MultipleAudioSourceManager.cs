using UnityEngine;
using System.Collections;

public class MultipleAudioSourceManager : MonoBehaviour {

	public AudioSource[] audioSources;
	public int audioSourceIndex;
	
	public float volume = 1.0f;

	void Awake () {
		for(int a = 0; a < audioSources.Length; a++) {
			audioSources[a].volume = volume;
		}
	}
	
	public void PlayAudio(AudioClip audioClip) {
		if(audioClip == null) return;
		audioSources[audioSourceIndex].PlayOneShot(audioClip);
		audioSourceIndex++;
		if(audioSourceIndex == audioSources.Length) audioSourceIndex = 0;
	}
	
}
