using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

/// Manages game state and player input.
public class GameController : Singleton<GameController>
{

    [Flags]
    public enum State { None = 1, Splash = 16, MainMenu = 2, Playing = 4, EndPlaying = 8 };

    public static StateManager<State> state = StateManager<State>.CreateNew();

    public string displayState = "";

    static bool _controlsEnabled = false;

    public static bool controlsEnabled
    {
        get
        {
            return _controlsEnabled;
        }
        set
        {
            _controlsEnabled = value;

        }
    }

    public delegate void EventHandler();

    public static event EventHandler OnPause;
    public static event EventHandler OnUnPause;

    public MinMaxEventFloat matchTimer = new MinMaxEventFloat(0f, 60f, 60f);

    public MinMaxEventFloat Destruction = new MinMaxEventFloat(0, 1, 0);
    CoroutineManager.Item matchTimerSequence = new CoroutineManager.Item();

    public float GameTime;

    [Space(10)]
    public bool paused = false;

    public static bool isQuitting = false;

    void Awake()
    {

        Initialize();
        state.value = State.Splash;
    }

    public static CoroutineManager.Item CurrentSequence = new CoroutineManager.Item();

    /// <summary>
    /// Call any necessary Initialize functions in other classes. The order is important.
    /// </summary>
    static void Initialize()
    {
        state.values[State.Splash].OnEnter += OnEnterSpashScreen;

        state.values[State.MainMenu].OnEnter += OnEnterStateMainMenu;

        state.values[State.Playing].OnEnter += OnEnterPlayingState;
        state.values[State.Playing].OnExit += OnExitPlayingState;

        state.values[State.EndPlaying].OnEnter += OnEnterEndPlayingState;
        state.values[State.EndPlaying].OnExit += OnExitEndPlayingState;

        state.OnChanged += instance.UpdateStateString;
    }

    // Use this for initialization
    void Start()
    {
#if !UNITY_EDITOR
        Cursor.visible = false;
#endif

        // Listen for controller connection events
        //ReInput.ControllerConnectedEvent += OnControllerConnected;

        //OnControllerConnected(null);
    }


    void UpdateStateString(State value)
    {
        Debug.Log(state.value + " || " + Time.time);
        displayState = state.value.AsUpperCamelCaseName();
    }

    // Update is called once per frame
    void Update()
    {
        InputManager();
    }

    IEnumerator MatchTimerSequence()
    {
        while (matchTimer.value > 0f)
        {
            matchTimer.value -= Time.deltaTime;
            yield return null;
        }
    }


    public static void OnEnterSpashScreen()
    {
        CurrentSequence.value = instance.SplashScreenSequence();
    }

    IEnumerator SplashScreenSequence()
    {
        yield return SplashScreen.instance.DisplaySequence();
        state.value = State.MainMenu;
    }

    public static void OnEnterStateMainMenu()
    {
        CurrentSequence.value = instance.StartMenuSequence();
        instance.Destruction.SetToMin();
    }


    public IEnumerator StartMenuSequence()
    {
        yield return new WaitForSeconds(1.0f);
        yield return WaitForAnyInput();

        state.value = State.Playing;
    }

    private IEnumerator WaitForAnyInput()
    {
        while(!Input.anyKey)
        {
            yield return new WaitForEndOfFrame();
        }
    }

    public static void OnEnterPlayingState()
    {
        CurrentSequence.value = instance.PlayingStateSequence();
        instance.Destruction.OnValueMax += instance.ShouldEndGame;
    }

    public void ShouldEndGame()
    {
        state.value = State.EndPlaying;
    }

    public static void OnExitPlayingState()
    {

        instance.Destruction.OnValueMax -= instance.ShouldEndGame;
        CurrentSequence.value = null;
    }

    public static void OnEnterEndPlayingState()
    {
        //state.value = State.MainMenu;
        //AudioManager.PlayAudio(AudioManager.instance.victory);


        CurrentSequence.value = instance.EndGameStateSequence();
    }

    public IEnumerator EndGameStateSequence()
    {
        yield return new WaitForSeconds(1.0f);
        yield return WaitForAnyInput();

        VehicleFactory.instance.Reset();

        state.value = State.MainMenu;
    }

    public IEnumerator PlayingStateSequence()
    {
        while(true)
        {
            yield return new WaitForSeconds(0.1f);
            GameTime += 0.1f;
            VehicleFactory.instance.IncreseDifficulty();
        }
    }

    public static void OnExitEndPlayingState()
    {

    }

    public void InputManager()
    {

        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }

        //		if(Input.GetKey(KeyCode.LeftControl) && Input.GetButtonUp("DeveloperMode")) {
        //			if(DataCore.developerMode == false) { 
        //				DataCore.developerMode = true;
        ////				if(!UIManager.instance.UI_Canvas.gameObject.activeInHierarchy) {
        ////					UIManager.instance.UI_Canvas.gameObject.SetActive(true);
        ////				}
        //				DeveloperConsole.instance.inputField.ActivateInputField();
        //				DeveloperConsole.instance.inputField.Select();

        //			} else {
        //				DataCore.developerMode = false;
        //				DeveloperConsole.instance.inputField.DeactivateInputField();
        //			}
        //		} 

        if (Input.GetKey("escape") && (Input.GetKey("left ctrl")))
        {
            Application.Quit();
        }

        //if (!DataCore.developerMode)
        //{

        //    if (ReInput.players.GetPlayer(0).GetButtonDown("Pause"))
        //    {
        //        TogglePause();
        //    }
        //}
    }

    public void TogglePause()
    {
        if (paused)
        {
            UnPause();
        }
        else
        {
            Pause();
        }

        //AudioManager.PlayAudio(AudioManager.instance.pause);
    }

    public void Pause()
    {
        paused = true;
        Time.timeScale = 0.0f;
        if (OnPause != null) OnPause();
    }

    public void UnPause()
    {
        paused = false;
        Time.timeScale = 1.0f;
        if (OnUnPause != null) OnUnPause();
    }

    public void OnApplicationQuit()
    {
        isQuitting = true;
    }
}