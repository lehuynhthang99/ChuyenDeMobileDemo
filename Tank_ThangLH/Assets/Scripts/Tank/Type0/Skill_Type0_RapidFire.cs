using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Type0_RapidFire : SkillBase
{
    [SerializeField] protected byte numberShots;
    protected byte countDownNumberShots;

    [SerializeField] protected float delayBetweenShots;
    protected float countDownDelayBetweenShots = 0f;

    

    public Rigidbody m_Shell;                   // Prefab of the shell.
    public Transform m_FireTransform;           // A child of the tank where the shells are spawned.
    public AudioSource m_ShootingAudio;         // Reference to the audio source used to play the shooting audio. NB: different to the movement audio source.
    public AudioClip m_FireClip;                // Audio that plays when each shot is fired.
    public float m_LaunchForce = 30f;        // The force given to the shell if the fire button is held for the max charge time.

    protected ObjectPooling<Rigidbody> shellPool = new ObjectPooling<Rigidbody>();

    public override void Awake()
    {
        base.Awake();
        countDownNumberShots = numberShots;
        m_ShootingAudio.clip = m_FireClip;
        countDownDelayNextSkill = delayNextSkill;
        countDownDelayBetweenShots = 0f;
        shellPool.GrowPool(m_Shell, numberShots);
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        countDownNumberShots = numberShots;
        m_ShootingAudio.clip = m_FireClip;
        countDownDelayBetweenShots = 0f;
        countDownDelayNextSkill = delayNextSkill;
    }

    // Update is called once per frame
    public override void UpdateNormal()
    {
        if (isButton && canActive)
            ActivateSkill();
        if (isActive && countDownNumberShots > 0)
            DoSkill();
        else if (!canActive)
            DelayNextSkill();
    }

    protected virtual void Fire()
    {
        // Create an instance of the shell and store a reference to it's rigidbody.
        Rigidbody shellInstance = shellPool.GetFromPool(m_Shell);
        shellInstance.transform.SetPositionAndRotation(m_FireTransform.position, m_FireTransform.rotation);

        // Set the shell's velocity to the launch force in the fire position's forward direction.
        shellInstance.velocity = m_LaunchForce * m_FireTransform.forward;

        m_ShootingAudio.Play();
    }

    protected override void ActivateSkill()
    {
        base.ActivateSkill();
        tankInfo.tankShooting.enabled = false;
    }

    protected override void DoSkill()
    {
        countDownDelayBetweenShots -= Time.deltaTime;
        if (countDownDelayBetweenShots <= 0f)
        {
            countDownDelayBetweenShots = delayBetweenShots;
            Fire();
            countDownNumberShots--;
            if (countDownNumberShots == 0)
            {
                countDownNumberShots = numberShots;
                isActive = false;
                tankInfo.tankShooting.enabled = true;
                countDownDelayBetweenShots = 0f;
                countDownDelayNextSkill = delayNextSkill;
            }
        }
    }

    public override void UpdateFixed()
    {
        shellPool.UpdateFixed();
    }
}
