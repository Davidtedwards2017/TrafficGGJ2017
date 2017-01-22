using UnityEngine;
using System.Collections;

public class PlayingUI : UiController {

    protected override void Awake()
    {
        base.Awake();
        GameController.state.values[GameController.State.Playing].OnEnter += Show;
        GameController.state.values[GameController.State.Playing].OnExit += Hide;
    }
}
