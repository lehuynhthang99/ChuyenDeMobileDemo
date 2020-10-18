using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankShooting_Type4 : TankShootingBase<LaserMovement_Type4>
{
    protected override void CreateShell()
    {
        // Create an instance of the shell and store a reference to it's rigidbody.
        LaserMovement_Type4 laserInstance = shellPool.GetFromPool(m_Shell);
        laserInstance.transform.SetPositionAndRotation(m_FireTransform.position, m_FireTransform.rotation);

        // Set the shell's velocity to the launch force in the fire position's forward direction.
        laserInstance.distance = m_CurrentLaunchForce / m_MaxLaunchForce * laserInstance.maxDistance;
    }
}
