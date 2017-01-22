using UnityEngine;
using System.Collections;

public class MenuUI : UiController {

    protected override void Awake()
    {
        base.Awake();
        GameController.state.values[GameController.State.MainMenu].OnEnter += Show;
        GameController.state.values[GameController.State.MainMenu].OnExit += Hide;
    }
}
