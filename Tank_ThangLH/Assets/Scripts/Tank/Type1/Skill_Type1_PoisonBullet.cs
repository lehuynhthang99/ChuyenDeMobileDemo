using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Type1_PoisonBullet : SkillBase
{
    [SerializeField] protected float angleBullet;
    public Rigidbody m_Shell;                   // Prefab of the shell.
    public int numberShells = 6;
    public Transform m_FireTransform;           // A child of the tank where the shells are spawned.
    public AudioSource m_ShootingAudio;         // Reference to the audio source used to play the shooting audio. NB: different to the movement audio source.
    public AudioClip m_FireClip;                // Audio that plays when each shot is fired.
    public float m_LaunchForce = 25f;        // The force given to the shell if the fire button is held for the max charge time.

    protected ObjectPooling<Rigidbody> shellPool = new ObjectPooling<Rigidbody>();

    public override void Awake()
    {
        base.Awake();
        m_ShootingAudio.clip = m_FireClip;
        countDownDelayNextSkill = delayNextSkill;
        shellPool.GrowPool(m_Shell, numberShells);
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        m_ShootingAudio.clip = m_FireClip;
        countDownDelayNextSkill = delayNextSkill;
    }

    protected virtual void Fire()
    {
        // Create an instance of the shell and store a reference to it's rigidbody.
        for (int i = -1; i < 2; i++)
        {
            Rigidbody shellInstance = shellPool.GetFromPool(m_Shell);
            shellInstance.transform.SetPositionAndRotation(m_FireTransform.position, m_FireTransform.rotation * Quaternion.Euler(Vector3.up * i * angleBullet));

            // Set the shell's velocity to the launch force in the fire position's forward direction.
            shellInstance.velocity = m_LaunchForce * (Quaternion.AngleAxis(i * angleBullet, Vector3.up) * m_FireTransform.forward);
        }

        m_ShootingAudio.Play();
    }

    protected override void ActivateSkill()
    {
        base.ActivateSkill();
        tankInfo.tankShooting.enabled = false;
    }

    protected override void DoSkill()
    {
        Fire();
        isActive = false;
        tankInfo.tankShooting.enabled = true;
        countDownDelayNextSkill = delayNextSkill;
    }

    public override void UpdateFixed()
    {
        shellPool.UpdateFixed();
    }
}

