using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameConfigs/TankSetting")]
public class TankSetting : ScriptableObject
{
    //TankHealth
    [Header("Tank Health")]
    [SerializeField] private float startingHealth = 100f;
    [SerializeField] private Color fullHealthColor = Color.green;       // The color the health bar will be when on full health.
    [SerializeField] private Color zeroHealthColor = Color.red;
    [SerializeField] private GameObject explosionPrefab;                // A prefab that will be instantiated in Awake, then used whenever the tank dies.

    //TankShooting
    [Header("Tank Shooting")]
    [Space(20)]
    [SerializeField] private AudioClip chargingClip;            // Audio that plays when each shot is charging up.
    [SerializeField] private AudioClip fireClip;
    [SerializeField] private float minLaunchForce = 15f;        // The force given to the shell if the fire button is not held.
    [SerializeField] private float maxLaunchForce = 30f;        // The force given to the shell if the fire button is held for the max charge time.
    [SerializeField] private float maxChargeTime = 0.75f;
    [SerializeField] private float delayTime = 1f;


    //TankMovement
    [Header("Tank Movement")]
    [Space(20)]
    [SerializeField] private float speed = 12f;                 // How fast the tank moves forward and back.
    [SerializeField] private float turnSpeed = 180f;
    [SerializeField] private AudioClip engineIdling;            // Audio to play when the tank isn't moving.
    [SerializeField] private AudioClip engineDriving;
    [SerializeField] private float pitchRange = 0.2f;


    public float StartingHealth { get => startingHealth; }
    public Color FullHealthColor { get => fullHealthColor; }
    public Color ZeroHealthColor { get => zeroHealthColor; }
    public GameObject ExplosionPrefab { get => explosionPrefab; }

    public AudioClip ChargingClip { get => chargingClip; }
    public AudioClip FireClip { get => fireClip; }
    public float MinLaunchForce { get => minLaunchForce; }
    public float MaxLaunchForce { get => maxLaunchForce; }
    public float MaxChargeTime { get => maxChargeTime; }
    public float DelayTime { get => delayTime; }

    public float Speed { get => speed; }
    public float TurnSpeed { get => turnSpeed; }
    public AudioClip EngineIdling { get => engineIdling; }
    public AudioClip EngineDriving { get => engineDriving; }
    public float PitchRange { get => pitchRange; }
    
}
