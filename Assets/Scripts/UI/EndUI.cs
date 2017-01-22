using UnityEngine;
using System.Collections;

public class EndUI : UiController {

    protected override void Awake()
    {
        base.Awake();
        GameController.state.values[GameController.State.EndPlaying].OnEnter += Show;
        GameController.state.values[GameController.State.EndPlaying].OnExit += Hide;
    }
}
