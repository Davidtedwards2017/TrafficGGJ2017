using UnityEngine;
using System.Collections;

public class EnableDisableOnStateBehavior : MonoBehaviour
{

    public GameController.State stateToMatch;

    void Awake()
    {
        if (!enabled) return;
        //GameController.state.OnChanged += EnableCheck;
        GameController.state.values[stateToMatch].OnEnter += Enable;
        GameController.state.values[stateToMatch].OnExit += Disable;
        EnableCheck();
    }

    //empty Start function to allow disabling in inspector
    void Start()
    {
        
    }

    void EnableCheck()
    {
        //Debug.Log("GameController state is " + GameController.state.value + " || state to match is: " + stateToMatch);
        gameObject.SetActive(GameController.state.value == stateToMatch);
    }

    void Enable()
    {
        //Debug.Log("GameController state is " + GameController.state.value + " || state to match is: " + stateToMatch + " || Enabling");
        gameObject.SetActive(true);
    }

    void Disable()
    {
        //Debug.Log("GameController state is " + GameController.state.value + " || state to match is: " + stateToMatch + " || Disabling");
        gameObject.SetActive(false);
    }
}