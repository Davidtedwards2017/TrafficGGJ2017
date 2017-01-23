using UnityEngine;
using System.Collections;

[RequireComponent(typeof(tk2dAnimatedSprite))]
public class PlayerAnimatorController : MonoBehaviour {

    public tk2dSpriteAnimator squidAnimator;

    public tk2dSpriteAnimator northFacingTentacle;
    public tk2dSpriteAnimator southFacingTentacle;
    public tk2dSpriteAnimator westFacingTentacle;
    public tk2dSpriteAnimator eastFacingTentacle;

    public delegate void AnimationEventHandler(tk2dSpriteAnimator animator, tk2dSpriteAnimationClip clip, int frameNumber);
    public event AnimationEventHandler onAnimationEventTriggered;

    public AudioSource audioSource;
    public AudioClip whistleBlow;

    void Awake()
    {
        //squidAnimator = GetComponent<tk2dAnimatedSprite>();
    }

    void Start()
    {
        MessageController.StartListening("DirectionInputChanged", OnDirectionChanged);
    }

    void OnDirectionChanged(object[] args)
    {
        DataTypes.Direction direction = (DataTypes.Direction)args[0];
        bool isOn = (bool)args[1];

        Wave(direction, isOn);
    }

    public void Wave(DataTypes.Direction direction, bool isOn)
    {
        string animName = "";
        tk2dSpriteAnimator tentacle = null;

        switch (direction)
        {
            case DataTypes.Direction.North:
                animName = isOn ? "ArmN_Wave" : "ArmN_Stop";
                tentacle = northFacingTentacle;
                break;
            case DataTypes.Direction.South:
                animName = isOn ? "ArmS_Wave" : "ArmS_Stop";
                tentacle = southFacingTentacle;
                break;
            case DataTypes.Direction.West:
                animName = isOn ? "ArmW_Wave" : "ArmW_Stop";
                tentacle = westFacingTentacle;
                break;
            case DataTypes.Direction.East:
                animName = isOn ? "ArmE_Wave" : "ArmE_Stop";
                tentacle = eastFacingTentacle;
                break;
        }

        if(tentacle != null) PlayAnimation(tentacle, animName);

        //if (Random.value > 0.25f) audioSource.PlayOneShot(whistleBlow);
        if(isOn) audioSource.PlayOneShot(whistleBlow);
    }

    void PlayAnimation(tk2dSpriteAnimator anim, string name, bool mayTransitionToSameAnimation = false)
    {
        //Debug.Log(anim);

        if (anim.IsPlaying(name) && !mayTransitionToSameAnimation) return;

        anim.Play(name);
    }

}
