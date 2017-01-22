using UnityEngine;
using System.Collections;

public class EnableDisableOnStateBehavior : MonoBehaviour
{
    [SerializeField, EnumFlag]
    public GameController.State stateToMatch;

    void Awake()
    {
        if (!enabled) return;
        GameController.state.OnChanged += EnableCheck;
        EnableCheck(GameController.state.value);
    }

    //Blank start function to allow enable
    void Start()
    {

    }

    void EnableCheck(GameController.State state)
    {
        //Debug.Log("GameController state is " + GameController.state.value + " || state to match is: " + (int)stateToMatch, this);
        //Debug.LogFormat("GameController state is {0} || state to match is {1}", GameController.state.value, (int)stateToMatch);
        //Debug.Log(HasFlag(stateToMatch, state));

        gameObject.SetActive(HasFlag(stateToMatch, state));
    }

    public static GameController.State SetFlag(GameController.State a, GameController.State b)
    {
        return a | b;
    }

    public static GameController.State UnsetFlag(GameController.State a, GameController.State b)
    {
        return a & (~b);
    }

    // Works with "None" as well
    public static bool HasFlag(GameController.State a, GameController.State b)
    {
        return (a & b) == b;
    }

    public static GameController.State ToogleFlag(GameController.State a, GameController.State b)
    {
        return a ^ b;
    }
}