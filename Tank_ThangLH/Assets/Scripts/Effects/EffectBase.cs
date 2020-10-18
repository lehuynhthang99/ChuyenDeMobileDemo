using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EffectType
{
    Base,
    Burn,
    PoisonZone
};



[Serializable]
public abstract class EffectBase : IEquatable<EffectBase>
{
    public const float FOREVER = 3000f;

    static public ushort idInit = 0;
    public ushort id;
    public EffectType type;
    public float duration;

    protected float delay;
    protected float countDownDelay = 0f;


    public EffectBase()
    { }

    public EffectBase(ushort _inId)
    {
        id = _inId;
    }

    public EffectBase(float _inDuration, float _inDelay)
    {
        delay = _inDelay;
        id = idInit;
        if (idInit == 65535)
            idInit = 0;
        else idInit++;
        type = EffectType.Base;
        duration = _inDuration;
    }

    public virtual void DoOnDisable(TankInfo tank) { }
    public virtual void DoUpdate(TankInfo tankInfo, float deltaTime)
    {
        countDownDelay -= deltaTime;
        duration -= deltaTime;
        if (countDownDelay <= 0f)
        {
            countDownDelay = delay;
            DoAffect(tankInfo);
        }
    }
    public virtual void DoAffect(TankInfo tankInfo) { }

    public bool Equals(EffectBase other)
    {
        if (id == other.id)
            return true;
        else return false;
    }
}
