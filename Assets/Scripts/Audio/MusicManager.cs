using System;
using UnityEngine;
using System.Collections;

public class MusicManager : Singleton<MusicManager>
{

    public AudioSource audioSource;

    public AudioClip[] adventureMode;

    public AudioClip[] shmexMatchMode;

    void Start()
    {
        //GameController.state.OnChanged += HandleGameState;
        GameController.state.values[GameController.State.Playing].OnEnter += PlayAdventureMusic;
        GameController.state.values[GameController.State.Playing].OnExit += PlayMatchMusic;
    }

    public static void Play(AudioClip clip)
    {
        instance.audioSource.Stop();
        instance.audioSource.clip = clip;
        instance.audioSource.Play();

    }

    void PlayAdventureMusic()
    {
        Play(adventureMode.PickRandom());
    }

    void PlayMatchMusic()
    {
        //Debug.Log("Restarting match music");
        Play(shmexMatchMode.PickRandom());
    }

    void HandleGameState(GameController.State state)
    {
        
    }
}
