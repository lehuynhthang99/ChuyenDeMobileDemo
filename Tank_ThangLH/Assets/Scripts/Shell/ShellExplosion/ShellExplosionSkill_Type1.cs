using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellExplosionSkill_Type1 : ShellExplosion
{
    [SerializeField] protected GameObject poisonZone;
    protected GameObject instancePoisonZone;

    public override void Awake()
    {
        base.Awake();
        instancePoisonZone = Instantiate(poisonZone);
        instancePoisonZone.SetActive(false);

    }

    protected override void CreateObject()
    {
        instancePoisonZone.SetActive(true);
        instancePoisonZone.transform.SetPositionAndRotation(transform.position, transform.rotation);
    }

   
}
