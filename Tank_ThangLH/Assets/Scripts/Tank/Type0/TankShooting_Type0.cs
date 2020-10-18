using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankShooting_Type0 : TankShootingBase<Rigidbody>
{
    protected override void CreateShell()
    {
        // Create an instance of the shell and store a reference to it's rigidbody.
        //Rigidbody shellInstance =
        //    Instantiate(m_Shell, m_FireTransform.position, m_FireTransform.rotation) as Rigidbody;

        Rigidbody shellInstance = shellPool.GetFromPool(m_Shell);
        shellInstance.transform.SetPositionAndRotation(m_FireTransform.position, m_FireTransform.rotation);

        // Set the shell's velocity to the launch force in the fire position's forward direction.
        shellInstance.velocity = m_CurrentLaunchForce * m_FireTransform.forward;
    }
}
