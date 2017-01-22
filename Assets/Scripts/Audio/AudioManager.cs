using UnityEngine;
using System.Collections;

public class AudioManager : Singleton<AudioManager> {
	
	public float volume = 1;
	
	public int audioSourceIndex = 0;
	public AudioSource[] audioSources;
	
	[Header("Menu")]
	public AudioClip select;
	public AudioClip confirm;
	public AudioClip cancel;
	
	[Header("General")]
	public AudioClip victory;
	public AudioClip pause;

    [Header("Impacts")]
    public AudioClip[] crashes;

    [Header("Player")]
    public AudioClip whistle;
    public AudioClip whistlelong;


    // Use this for initialization
    void Awake () {
		for(int a = 0; a < audioSources.Length; a++) {
			audioSources[a].volume = volume;
		}
	}
	
	public static void PlayAudio(AudioClip audioClip, float pitch = 1f) {
		if(audioClip == null) return;
		AudioSource audioSource = instance.audioSources [instance.audioSourceIndex];
		audioSource.pitch = pitch;
	    audioSource.volume = instance.volume;
		audioSource.PlayOneShot(audioClip);
		instance.audioSourceIndex++;
		if(instance.audioSourceIndex == instance.audioSources.Length) instance.audioSourceIndex = 0;
	}
	
	//public IEnumerator PlayAudioRepeated(AudioClip audioClip, int repeats, float delay) {
	//	int r = 0;
		
	//	while(r < repeats) {
	//		PlayAudio(audioClip);
	//		r++;
	//		yield return new WaitForUnscaledSeconds(delay);
	//	}
	//}	
	
	//public IEnumerator PlayAudioDelayed(AudioClip audioClip, float delay) {
	//	yield return new WaitForUnscaledSeconds(delay);
		
	//	PlayAudio(audioClip);
	//}																																																																																															
}