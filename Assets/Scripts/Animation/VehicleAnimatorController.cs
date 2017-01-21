using System;
using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class VehicleAnimatorController : MonoBehaviour
{

    // Link to the animated sprite
    private tk2dSpriteAnimator anim;

    public tk2dSprite sprite;

    private Vehicle vehicle;

    // State variable to see if the vehicle is moving.
    private bool moving = true;

    public delegate void AnimationEventHandler(tk2dSpriteAnimator animator, tk2dSpriteAnimationClip clip, int frameNumber);
    public event AnimationEventHandler onAnimationEventTriggered;

    public Transform groundFxPoint;

    public delegate void ColorEventHandler(Color newColor);

    public event ColorEventHandler OnColorChanged;

    //private Player player;
    // Use this for initialization
    void Awake()
    {
        //player = GetComponentInParent<Player>();
        // This script must be attached to the sprite to work.
        anim = GetComponent<tk2dSpriteAnimator>();
        sprite = GetComponent<tk2dSprite>();
        vehicle = GetComponentInParent<Vehicle>();
        anim.AnimationEventTriggered += HandleAnimationEvent;
    }

    void Update()
    {
        //if (player.state.value != Player.State.Stun && player.movement.sqrMagnitude > 0f)
        //{
        //    if (player.movement.x > 0f)
        //    {
        //        //sprite.FlipX = true;
        //        sprite.FlipX = false;
        //    }
        //    else
        //    {
        //        //sprite.FlipX = false;
        //        sprite.FlipX = true;
        //    }

        //}
    }

    // This is called once the hit animation has completed playing
    // It returns to playing whatever animation was active before hit
    // was playing.
    void HitCompleteDelegate(tk2dSpriteAnimator sprite, tk2dSpriteAnimationClip clip)
    {
        //if (walking)
        //{
        //    anim.Play("Run");
        //    anim.AnimationEventTriggered += HandleAnimationEvent;
        //}
        //else {
        //    anim.Play("Idle");
        //}
    }

    void HandleAnimationEvent(tk2dSpriteAnimator animator, tk2dSpriteAnimationClip clip, int frameNumber)
    {
        if (onAnimationEventTriggered != null) onAnimationEventTriggered(animator, clip, frameNumber);
        //if(stepAnimator) stepAnimator.Play();

        if (groundFxPoint)
        {
            //GameObject stepFX = Instantiate(Prefabs.FX.Dust.footstep);
            //stepFX.transform.position = groundFxPoint.position;
            //stepFX.transform.forward = -player.transform.forward;
        }
    }

    // Update is called once per frame
    //public void HandleStateChange(Player.State state)
    //{
    //    switch (state)
    //    {
    //        case Player.State.Idle:
    //            PlayAnimation("Idle");
    //            break;
    //        case Player.State.Run:
    //            PlayAnimation("Run");
    //            break;
    //        case Player.State.Primary:
    //            PlayAnimation("Primary");
    //            //anim.AnimationCompleted = HitCompleteDelegate;
    //            break;
    //        case Player.State.Secondary:
    //            //if(Mathf.Abs(player.movement.x) > 0.01f) sprite.FlipX = !sprite.FlipX;
    //            PlayAnimation("Secondary");
    //            break;
    //        case Player.State.Special:
    //            //if(Mathf.Abs(player.movement.x) > 0.01f) sprite.FlipX = !sprite.FlipX;
    //            PlayAnimation("Special");
    //            break;
    //        case Player.State.Super:
    //            //if(Mathf.Abs(player.movement.x) > 0.01f) sprite.FlipX = !sprite.FlipX;
    //            PlayAnimation("Super");
    //            break;
    //        case Player.State.Stun:
    //            PlayAnimation("Stun");
    //            break;
    //        case Player.State.KO:
    //            PlayAnimation("KO");
    //            break;
    //        default:
    //            throw new ArgumentOutOfRangeException();
    //    }

        //if (Input.GetKey(KeyCode.A))
        //{
        //    // Only play the clip if it is not already playing.
        //    // Calling play will restart the clip if it is already playing.
        //    if (!anim.IsPlaying("Primary"))
        //    {
        //        PlayAnimation("Primary");

        //        // The delegate is used here to return to the previously
        //        // playing clip after the "hit" animation is done playing.
        //        anim.AnimationCompleted = HitCompleteDelegate;
        //    }
        //}

        //if (Input.GetKey(KeyCode.D))
        //{
        //    PlayAnimation("Run");
        //}

        //if (Input.GetKey(KeyCode.W))
        //{
        //    if (!anim.IsPlaying("Idle"))
        //    {
        //        anim.Play("Idle");
        //        // We dont have any reason for detecting when it completes
        //        anim.AnimationCompleted = null;
        //        walking = false;
        //    }
        //}
    //}

    void PlayAnimation(string name, bool mayTransitionToSameAnimation = false)
    {
        if (anim.IsPlaying(name) && !mayTransitionToSameAnimation) return;

        anim.Play(name);
    }

    public void SetColor(Color newColor)
    {
        sprite.color = newColor;

        if (OnColorChanged != null) OnColorChanged(newColor);
    }
}