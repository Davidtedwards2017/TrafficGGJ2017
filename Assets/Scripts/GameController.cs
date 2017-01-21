using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(LineRenderer))]
/// Manages game state and player input.
public class GameController : Singleton<GameController>
{

    public enum State { MainMenu, Adventure, MatchStart, Match, MatchEnd };

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

    CoroutineManager.Item matchTimerSequence = new CoroutineManager.Item();

    [Space(10)]
    public bool paused = false;

    public static bool isQuitting = false;

    void Awake()
    {

        Initialize();
    }

    /// <summary>
    /// Call any necessary Initialize functions in other classes. The order is important.
    /// </summary>
    static void Initialize()
    {
        state.values[State.MainMenu].OnEnter += OnEnterStateMainMenu;
        state.values[State.MatchStart].OnEnter += OnEnterStateMatchStart;

        state.values[State.Match].OnEnter += OnEnterStateMatch;
        state.values[State.Match] += instance.EnterStateMatchSequence();
        state.values[State.Match].OnExit += OnExitStateMatch;

        state.values[State.MatchEnd].OnEnter += OnEnterStateMatchEnd;
        state.values[State.MatchEnd].OnExit += OnExitStateMatchEnd;

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

    //// This will be called when a controller is connected
    //void OnControllerConnected(ControllerStatusChangedEventArgs args)
    //{
    //    if (args != null && args.controllerType != ControllerType.Joystick) return; // skip if this isn't a Joystick

    //    if (Rewired.ReInput.controllers.Joysticks.Count > 0 && Rewired.ReInput.controllers.Joysticks[0] != null &&
    //        Rewired.ReInput.players.SystemPlayer.controllers.joystickCount == 0)
    //    {
    //        Rewired.ReInput.players.SystemPlayer.controllers.AddController(Rewired.ReInput.controllers.Joysticks[0], false);
    //    }
    //}

    void UpdateStateString(State value)
    {
        Debug.Log(state.value + " || " + Time.time);
        displayState = state.value.AsUpperCamelCaseName();
    }

    public void NewGame()
    {
        state.value = State.Adventure;
    }

    public void BeginMatch()
    {
        if (state.value == State.Match) return;
        state.value = State.MatchStart;

        matchTimer.OnValueMin += EndMatch;
    }

    public void EndMatch()
    {
        state.value = State.MatchEnd;
        matchTimer.OnValueMin -= EndMatch;

        state.value = State.Adventure;
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


    public static void OnEnterStateMainMenu()
    {
        //StartCoroutine(RestartProcess());
    }

    public IEnumerator RestartProcess()
    {

        yield return null;
        state.value = State.MatchStart;
    }
    public static void OnEnterStateMatchStart()
    {

        instance.StartCoroutine(instance.EnterStateMatchSequence());
        instance.matchTimer.SetToMax();
        AudioManager.PlayAudio(AudioManager.instance.confirm);
    }

    public static void OnEnterStateMatch()
    {
        instance.matchTimerSequence.value = instance.MatchTimerSequence();
    }

    IEnumerator EnterStateMatchSequence()
    {

        Debug.Log("GameController Match Start process || " + Time.time);

        //AnnouncerManager.AnnounceMatchStart();

        yield return new WaitForSeconds(0.5f);

        state.value = State.Match;

    }

    public static void OnExitStateMatch()
    {
        instance.matchTimerSequence.value = null;
    }

    public static void OnEnterStateMatchEnd()
    {
        //state.value = State.MainMenu;
        AudioManager.PlayAudio(AudioManager.instance.victory);
        state.value = State.Adventure;
        instance.matchTimer.OnValueMin -= instance.EndMatch;
    }

    public static void OnExitStateMatchEnd()
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

        AudioManager.PlayAudio(AudioManager.instance.pause);
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