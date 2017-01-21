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
        GameController.state.values[GameController.State.Adventure].OnEnter += PlayAdventureMusic;
        GameController.state.values[GameController.State.Adventure].OnExit += PlayMatchMusic;
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
        switch (state)
        {
            case GameController.State.MainMenu:
                break;
            case GameController.State.Adventure:
                Play(adventureMode.PickRandom());
                break;
            case GameController.State.MatchStart:
                Play(shmexMatchMode.PickRandom());
                break;
            case GameController.State.Match:
                break;
            case GameController.State.MatchEnd:
                break;                
        }
    }
}
