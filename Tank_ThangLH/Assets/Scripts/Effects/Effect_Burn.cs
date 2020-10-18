using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect_Burn : EffectBase
{
    protected float damagePerDelay;

    public Effect_Burn(float _inDuration, float _inDelay, float _inDamgePerDelay) : base(_inDuration, _inDelay)
    {
        
        damagePerDelay = _inDamgePerDelay;
        type = EffectType.Burn;
    }

    public override void DoAffect(TankInfo tankInfo)
    {
        tankInfo.tankHealth.TakeDamage(damagePerDelay);
    }
}

