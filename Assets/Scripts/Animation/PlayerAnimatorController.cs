using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

    Dictionary<DataTypes.Direction, bool> lanesOpen = new Dictionary<DataTypes.Direction, bool>();

    void Awake()
    {
        //squidAnimator = GetComponent<tk2dAnimatedSprite>();

        northFacingTentacle.AnimationCompleted += HandleAnimationComplete;
        southFacingTentacle.AnimationCompleted += HandleAnimationComplete;
        westFacingTentacle.AnimationCompleted += HandleAnimationComplete;
        eastFacingTentacle.AnimationCompleted += HandleAnimationComplete;

        //northFacingTentacle.AnimationEventTriggered += HandleAnimationComplete;
        //southFacingTentacle.AnimationEventTriggered += HandleAnimationComplete;
        //westFacingTentacle.AnimationEventTriggered += HandleAnimationComplete;
        //eastFacingTentacle.AnimationEventTriggered += HandleAnimationComplete;

        lanesOpen = new Dictionary<DataTypes.Direction, bool>
        {
            { DataTypes.Direction.North, false },
                        { DataTypes.Direction.South, false },
                                    { DataTypes.Direction.West, false },
                                                { DataTypes.Direction.East, false },
        };
    }

    void HandleAnimationComplete(tk2dSpriteAnimator anim, tk2dSpriteAnimationClip clip)
    {

        if(anim == northFacingTentacle)
        {
            if(lanesOpen[DataTypes.Direction.North])
            {
                PlayAnimation(anim, "ArmN_Wave");
            } else
            {
                PlayAnimation(anim, "ArmN_Stop");
            }
        }
        if (anim == southFacingTentacle)
        {
            if (lanesOpen[DataTypes.Direction.South])
            {
                PlayAnimation(anim, "ArmS_Wave");
            }
            else
            {
                PlayAnimation(anim, "ArmS_Stop");
            }
        }
        if (anim == westFacingTentacle)
        {
            if (lanesOpen[DataTypes.Direction.West])
            {
                PlayAnimation(anim, "ArmW_Wave");
            }
            else
            {
                PlayAnimation(anim, "ArmW_Stop");
            }
        }
        if (anim == eastFacingTentacle)
        {
            if (lanesOpen[DataTypes.Direction.East])
            {
                PlayAnimation(anim, "ArmE_Wave");
            }
            else
            {
                PlayAnimation(anim, "ArmE_Stop");
            }
        }

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
        lanesOpen[direction] = isOn;

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

        if(tentacle != null && isOn) PlayAnimation(tentacle, animName);

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
