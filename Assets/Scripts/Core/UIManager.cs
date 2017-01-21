using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIManager : Singleton<UIManager> {

	public Camera uiCamera;
	public Canvas uiCanvas;

	public Slider timeSlider;
    public Text matchTimeText;

	public Text player1ScoreText;
	public Text player2ScoreText;

	public CanvasGroup menuGroup;

	public Button initialButton;

    public CanvasGroupFader pauseFader;

	CoroutineManager.Item matchTimerUISequence = new CoroutineManager.Item();
	// Use this for initialization
	void Start () {
		
	}

	void Initialize() {
		//Debug.Log(("UI Manager initialized").Colored(Colors.aqua));

		//for(int n = 0; n < Player.players.Length; n++) {
		//	//Debug.Log("UI Manager is trying to assign to Player number " + n + ".");
		//	if(Player.players[n] == null) continue;
		//	Player.players[n].score.OnValueChanged += UpdateUI;
		//}

		matchTimerUISequence.value = MatchTimerUISequence();

		UpdateUI();

		//Debug.Log("Turned on match timer slider");
	}

	void Uninitialize() {


		matchTimerUISequence.value = null;

		//Debug.Log("Turned off match timer slider");
	}

	void UpdateUI() {
		//player1ScoreText.text = "" + Player.players[0].score.value;
		//player2ScoreText.text = "" + Player.players[1].score.value;
	}

	IEnumerator MatchTimerUISequence() {

		while(true) {
            //timeSlider.MatchValues(GameController.instance.matchTimer);
            matchTimeText.text = GameController.instance.matchTimer.value.ToString("F0");
			yield return null;
		}
	}

	void ShowMenu() {
		menuGroup.SetInteractive(true, 0.25f);
		EventSystem.current.SetSelectedGameObject(initialButton.gameObject);
	}

	void HideMenu() {
		menuGroup.SetInteractive(false, 0.25f);
		EventSystem.current.SetSelectedGameObject(null);
	}

	void OnEnable() {
        GameController.state.values[GameController.State.MainMenu].OnEnter += ShowMenu;
        GameController.state.values[GameController.State.MainMenu].OnExit += HideMenu;

        GameController.state.values[GameController.State.MatchStart].OnEnter += Initialize;
        GameController.state.values[GameController.State.MatchEnd].OnExit += Uninitialize;

        GameController.OnPause += pauseFader.FadeIn;
        GameController.OnUnPause += pauseFader.FadeOut;
    }

	void OnDisable() {

	}
}
