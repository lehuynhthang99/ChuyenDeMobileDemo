using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Type4_LaserPenetrate : SkillBase
{
    [SerializeField] protected LaserSkillMovement_Type4 laser;
    public Transform m_FireTransform;           // A child of the tank where the shells are spawned.
    public AudioSource m_ShootingAudio;         // Reference to the audio source used to play the shooting audio. NB: different to the movement audio source.
    public AudioClip m_FireClip;
    public float angleBullets = 20f;

    protected ObjectPooling<LaserSkillMovement_Type4> laserPool = new ObjectPooling<LaserSkillMovement_Type4>();

    public override void Awake()
    {
        base.Awake();
        m_ShootingAudio.clip = m_FireClip;
        countDownDelayNextSkill = delayNextSkill;
        laserPool.GrowPool(laser, 3);
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
            LaserSkillMovement_Type4 laserInstance = laserPool.GetFromPool(laser);
            laserInstance.transform.SetPositionAndRotation(m_FireTransform.position, m_FireTransform.rotation * Quaternion.Euler(Vector3.up * i * angleBullets));

            // Set the shell's velocity to the launch force in the fire position's forward direction.
            laserInstance.distance = laserInstance.maxDistance;
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
        laserPool.UpdateFixed();
    }
}
