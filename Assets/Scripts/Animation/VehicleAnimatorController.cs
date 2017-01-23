using System;
using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using DG.Tweening;

public class VehicleAnimatorController : MonoBehaviour
{

    // Link to the animated sprite
    //private tk2dSpriteAnimator anim;
    public tk2dSprite sprite;

    public tk2dSpriteAnimator anim;

    public Vehicle vehicle;

    // State variable to see if the vehicle is moving.
    private bool moving = true;

    public delegate void AnimationEventHandler(tk2dSpriteAnimator animator, tk2dSpriteAnimationClip clip, int frameNumber);
    public event AnimationEventHandler onAnimationEventTriggered;

    public Transform groundFxPoint;

    public delegate void ColorEventHandler(Color newColor);

    public event ColorEventHandler OnColorChanged;

    //public Shader crashShader;
    public Material crashMaterial;

    public float MaxAngerIntensity = 0.3f;
    public Wiggle wiggle;

    public MeshRenderer meshRenderer;

    bool crashed = false;

    //private Player player;
    // Use this for initialization
    void Awake()
    {
        //player = GetComponentInParent<Player>();
        // This script must be attached to the sprite to work.
        if(anim == null) anim = GetComponent<tk2dSpriteAnimator>();
        if(sprite == null) sprite = GetComponent<tk2dSprite>();
        if(vehicle == null) vehicle = GetComponentInParent<Vehicle>();
        if (wiggle == null) wiggle = GetComponent<Wiggle>();
        if (meshRenderer == null) meshRenderer = GetComponent<MeshRenderer>();
        //anim.AnimationEventTriggered += HandleAnimationEvent;
    }

    void Update()
    {
        //if(crashed)
        //{
        //    meshRenderer.material.shader = crashShader;
        //}
    }

    public void PlayIdle()
    {
        PlayAnimation(vehicle.vehicleName + "_Idle");
    }

    public void PlayDrive()
    {
        PlayAnimation(vehicle.vehicleName + "_Drive");
    }

    internal void Initialize(DataTypes.Direction direction)
    {
        sprite.FlipX = (direction == DataTypes.Direction.East || direction == DataTypes.Direction.North);
    }

    public void HandlePatience(float patience)
    {
        Color newColor = Color.Lerp(Color.red, Color.white, patience);

        sprite.color = newColor;

        //wiggle.Intensity = 0.1f + 0.6f * (1 / patience);

        wiggle.Intensity = Mathf.Lerp(0, MaxAngerIntensity, 1 / patience + 0.01f);
    }

    internal void HandleCrash()
    {
        crashed = true;
        //meshRenderer.material.shader = crashShader;
        anim.enabled = false;
        if (crashMaterial) meshRenderer.material = crashMaterial;

    }

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