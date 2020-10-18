using System;
using UnityEngine;

public class ShellExplosion : BaseMonoBehaviour
{
    public LayerMask m_TankMask;                        // Used to filter what the explosion affects, this should be set to "Players".

    public ParticleSystem prefabExplosion;
    protected ParticleSystem m_ExplosionParticles;         // Reference to the particles that will play on explosion.
    protected AudioSource m_ExplosionAudio;                // Reference to the audio that will play on explosion.

    public float m_MaxDamage = 100f;                    // The amount of damage done if the explosion is centred on a tank.
    public float m_ExplosionForce = 1000f;              // The amount of force added to a tank at the centre of the explosion.
    public float m_MaxLifeTime = 2f;                    // The time in seconds before the shell is removed.
    public float m_ExplosionRadius = 5f;                // The maximum distance away from the explosion tanks can be and are still affected.

    private float countDownMaxLifeTime;
    protected float damage;

    public override void Awake()
    {
        base.Awake();
        m_ExplosionParticles = Instantiate(prefabExplosion) as ParticleSystem;
        m_ExplosionAudio = m_ExplosionParticles.GetComponent<AudioSource>();
        m_ExplosionParticles.gameObject.SetActive(false);
    }

    protected virtual void OnEnable()
    {
        // If it isn't destroyed by then, destroy the shell after it's lifetime.
        countDownMaxLifeTime = m_MaxLifeTime;
    }

    public override void UpdateNormal()
    {
        //countDown Bullet Life span
        if (countDownMaxLifeTime > 0f)
            countDownMaxLifeTime -= Time.deltaTime;
        else
        {
            m_ExplosionParticles.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }

    }


    protected virtual void OnTriggerEnter(Collider other)
    {
        CreateObject();
        // Collect all the colliders in a sphere from the shell's current position to a radius of the explosion radius.
        Collider[] colliders = Physics.OverlapSphere(transform.position, m_ExplosionRadius, m_TankMask);

        // Go through all the colliders...
        for (int i = 0; i < colliders.Length; i++)
        {
            // ... and find their rigidbody.
            TankInfo target = colliders[i].GetComponent<TankInfo>();

            // If they don't have a rigidbody, go on to the next collider.
            if (!target)
                continue;

            // Add an explosion force.
            target.rigidbody.AddExplosionForce(m_ExplosionForce, transform.position, m_ExplosionRadius);

            // If there is no TankHealth script attached to the gameobject, go on to the next collider.
            if (!target.tankHealth)
                continue;

            // Calculate the amount of damage the target should take based on it's distance from the shell.
            damage = CalculateDamage(target.rigidbody.position);

            // Deal this damage to the tank.
            target.tankHealth.TakeDamage(damage);

            AddEffect(target);
        }

        // Unparent the particles from the shell.
        //m_ExplosionParticles.transform.parent = null;

        m_ExplosionParticles.gameObject.transform.SetPositionAndRotation(transform.position, transform.rotation);
        m_ExplosionParticles.gameObject.SetActive(true);
        // Play the particle system.
        m_ExplosionParticles.Play();

        // Play the explosion sound effect.
        m_ExplosionAudio.Play();

        //// Once the particles have finished, destroy the gameobject they are on.
        //ParticleSystem.MainModule mainModule = m_ExplosionParticles.main;
        //Destroy(m_ExplosionParticles.gameObject, mainModule.duration);

        // Destroy the shell.
        gameObject.SetActive(false);
    }

    protected virtual void AddEffect(TankInfo tank) { }

    protected virtual void CreateObject() { }

    protected virtual float CalculateDamage(Vector3 targetPosition)
    {
        // Create a vector from the shell to the target.
        Vector3 explosionToTarget = targetPosition - transform.position;

        // Calculate the distance from the shell to the target.
        float explosionDistance = explosionToTarget.magnitude;

        // Calculate the proportion of the maximum distance (the explosionRadius) the target is away.
        float relativeDistance = (m_ExplosionRadius - explosionDistance) / m_ExplosionRadius;

        // Calculate damage as this proportion of the maximum possible damage.
        float takingDamage = relativeDistance * m_MaxDamage;

        // Make sure that the minimum damage is always 0.
        takingDamage = Mathf.Max(0f, takingDamage);

        return takingDamage;
    }
}