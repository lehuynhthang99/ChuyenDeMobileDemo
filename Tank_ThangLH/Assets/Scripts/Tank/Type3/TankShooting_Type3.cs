using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankShooting_Type3 : TankShootingBase<Rigidbody>
{
    protected float rotation;
    // Start is called before the first frame update
    protected override void OnEnable()
    {
        base.OnEnable();
        ShellMovement_Type3 tmp = m_Shell.GetComponent<ShellMovement_Type3>();
        rotation = -tmp.rotation / 2;
    }
    
    protected override void CreateShell()
    {
        // Create an instance of the shell and store a reference to it's rigidbody.
        Rigidbody shellInstance = shellPool.GetFromPool(m_Shell);
        shellInstance.transform.SetPositionAndRotation(m_FireTransform.position, m_FireTransform.rotation * Quaternion.Euler(Vector3.up * rotation));

        // Set the shell's velocity to the launch force in the fire position's forward direction.
        shellInstance.velocity = m_CurrentLaunchForce * m_FireTransform.forward;
    }
}
