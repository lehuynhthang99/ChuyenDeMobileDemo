using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankShooting_Type1 : TankShootingBase<Rigidbody>
{
    [SerializeField] protected float angleBullet;

    protected override void CreateShell()
    {
        for (int i = -1; i < 2; i++)
        {
            Rigidbody shellInstance = shellPool.GetFromPool(m_Shell);
            shellInstance.transform.SetPositionAndRotation(m_FireTransform.position, m_FireTransform.rotation * Quaternion.Euler(Vector3.up * i * angleBullet));

            // Set the shell's velocity to the launch force in the fire position's forward direction.
            shellInstance.velocity = m_CurrentLaunchForce * (Quaternion.AngleAxis(i * angleBullet, Vector3.up) * m_FireTransform.forward);
        }
    }
}
