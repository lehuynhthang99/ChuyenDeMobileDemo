using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetUpTank : BaseMonoBehaviour
{
    [SerializeField] protected TankSetting tankSetting;
    [SerializeField] protected TankInfo tankInfo;

    public override void Awake()
    {
        base.Awake();

        TankHealth tankHealth = tankInfo.tankHealth;
        tankHealth.m_StartingHealth = tankSetting.StartingHealth;
        tankHealth.m_FullHealthColor = tankSetting.FullHealthColor;
        tankHealth.m_ZeroHealthColor = tankSetting.ZeroHealthColor;
        tankHealth.m_ExplosionPrefab = tankSetting.ExplosionPrefab;

        TankShootingBase tankShooting = tankInfo.tankShooting;
        tankShooting.m_ChargingClip = tankSetting.ChargingClip;
        tankShooting.m_FireClip = tankSetting.FireClip;
        tankShooting.m_MinLaunchForce = tankSetting.MinLaunchForce;
        tankShooting.m_MaxLaunchForce = tankSetting.MaxLaunchForce;
        tankShooting.m_MaxChargeTime = tankSetting.MaxChargeTime;
        tankShooting.delayTime = tankSetting.DelayTime;

        TankMovement tankMovement = tankInfo.tankMovement;
        tankMovement.m_Speed = tankSetting.Speed;
        tankMovement.m_TurnSpeed = tankSetting.TurnSpeed;
        tankMovement.m_EngineIdling = tankSetting.EngineIdling;
        tankMovement.m_EngineDriving = tankSetting.EngineDriving;
        tankMovement.m_PitchRange = tankSetting.PitchRange;
    }
}
    
