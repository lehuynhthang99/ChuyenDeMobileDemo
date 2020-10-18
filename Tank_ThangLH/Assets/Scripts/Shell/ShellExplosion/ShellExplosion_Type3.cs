using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellExplosion_Type3 : ShellExplosion
{
    [SerializeField] protected float burnDamagePercentMaxDamge = 5;
    [SerializeField] protected float burnDuration = 5f;
    [SerializeField] protected float burnDelay = 0.5f;


    

    protected override void AddEffect(TankInfo tank)
    {
        if (damage > 0f)
            tank.tankEffects.AddEffect(new Effect_Burn(burnDuration, burnDelay, damage * burnDamagePercentMaxDamge / m_MaxDamage));
    }
}
