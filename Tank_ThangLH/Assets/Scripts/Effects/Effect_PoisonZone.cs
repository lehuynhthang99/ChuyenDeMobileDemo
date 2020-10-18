using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect_PoisonZone : EffectBase
{
    protected float damagePerDelay;

    public Effect_PoisonZone(float _inDelay, float _inDamgePerDelay) : base(EffectBase.FOREVER, _inDelay)
    {
        damagePerDelay = _inDamgePerDelay;
        type = EffectType.PoisonZone;
    }

    public override void DoAffect(TankInfo tankInfo)
    {
        tankInfo.tankHealth.TakeDamage(damagePerDelay);
    }
}
