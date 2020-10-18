using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSkillMovement_Type4 : ShellMovementBase
{
    [SerializeField] protected float speed;
    public float maxDistance;
    [HideInInspector] public float distance;

    [SerializeField] protected LayerMask layer;

    [SerializeField] protected TrailRenderer trailRenderer;
    public float m_MaxDamage = 100f;                        // The amount of damage done if the explosion is centred on a tank.
    public float m_Radius = 5f;

    protected RaycastHit hit;
    protected float movement;
    protected bool canAffect = true;
    protected float countDownTrail;
    protected bool doDisable = false;

    protected override void Start()
    {
    }

    void OnEnable()
    {
        canAffect = true;
        doDisable = false;
        countDownTrail = trailRenderer.time;
    }

    public override void UpdateNormal()
    {
        if (doDisable)
        {
            DoDisable();
            return;
        }
        if (!canAffect) return;
        if (distance >= Time.deltaTime * speed)
        {
            movement = Time.deltaTime * speed;
            distance -= movement;
        }
        else
        {
            movement = distance;
            distance = 0f;
        }
        Vector3 startPoint = transform.position;
        Vector3 endPoint = startPoint + transform.forward * movement;
        while (Physics.Linecast(startPoint, endPoint, out hit, layer))
        {
            DoDamage();
            startPoint = hit.point;
        }
        transform.position = endPoint;

        if (distance <= 0)
        {
            canAffect = false;
            doDisable = true;
        }

    }

    private void DoDisable()
    {
        countDownTrail -= Time.deltaTime;
        if (countDownTrail <= 0f)
            gameObject.SetActive(false);
    }

    protected void DoDamage()
    {
        TankInfo target = hit.collider.GetComponent<TankInfo>();

        // If they don't have a rigidbody, go on to the next collider.
        if (!target)
            return;

        // If there is no TankHealth script attached to the gameobject, go on to the next collider.
        if (!target.tankHealth)
            return;

        float damage = CalculateDamage(target.rigidbody.position);
        // Deal this damage to the tank.
        target.tankHealth.TakeDamage(damage);

    }

    protected virtual float CalculateDamage(Vector3 targetPosition)
    {
        // Create a vector from the shell to the target.
        Vector3 explosionToTarget = targetPosition - transform.position;

        // Calculate the distance from the shell to the target.
        float explosionDistance = explosionToTarget.magnitude;

        // Calculate the proportion of the maximum distance (the explosionRadius) the target is away.
        float relativeDistance = (m_Radius - explosionDistance) / m_Radius;

        // Calculate damage as this proportion of the maximum possible damage.
        float damage = relativeDistance * m_MaxDamage;

        // Make sure that the minimum damage is always 0.
        damage = Mathf.Max(0f, damage);

        return damage;
    }
}
