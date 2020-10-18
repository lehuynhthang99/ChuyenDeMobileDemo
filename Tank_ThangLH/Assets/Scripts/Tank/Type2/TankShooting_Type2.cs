using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankShooting_Type2 : TankShootingBase<Rigidbody>
{
    public float bulletWaitingTime = 0.5f;
    private float countDownBulletWaitingTime;
    private float bulletRotation;

    // Start is called before the first frame update
    protected override void OnEnable()
    {
        base.OnEnable();
        countDownDelayTime = delayTime;
        countDownBulletWaitingTime = bulletWaitingTime;
        bulletRotation = 0f;
        isFire = 5;
    }

    // Update is called once per frame
    public override void UpdateNormal()
    {
        // The slider should have a default value of the minimum launch force.
        m_AimSlider.value = m_MinLaunchForce;

        if (isFire == 5)
        {

            // If the max force has been exceeded and the shell hasn't yet been launched...
            if (m_CurrentLaunchForce >= m_MaxLaunchForce && !m_Fired)
            {
                // ... use the max force and launch the shell.
                m_CurrentLaunchForce = m_MaxLaunchForce;
                StartLaunchingShells();
            }

            // Otherwise, if the fire button has just started being pressed...
            else if (isButtonDown)
                StartCharging();
            
            // Otherwise, if the fire button is being held and the shell hasn't been launched yet...
            else if (isButton && !m_Fired)
                Charging();
            
            // Otherwise, if the fire button is released and the shell hasn't been launched yet...
            else if (isButtonUp && !m_Fired)
                // ... launch the shell.
                StartLaunchingShells();
        }
        else if (isFire == 0)
        {
            DelayNextShot();
        }
        else
        {
            //Change bullet rotate for the next shell
            countDownBulletWaitingTime -= Time.deltaTime;
            if(countDownBulletWaitingTime<=0f)
            {
                switch (isFire)
                {
                    case 4:
                        bulletRotation = -10;
                        break;
                    case 3:
                        bulletRotation = 10;
                        break;
                    case 2:
                        bulletRotation = -20;
                        break;
                    case 1:
                        bulletRotation = 20;
                        break;
                    default:
                        break;
                }
                Fire();
                isFire -= 1;
                countDownBulletWaitingTime = bulletWaitingTime;
            }
        }
    }

    protected override void StartLaunchingShells()
    {
        Fire();
        m_AimSlider.gameObject.SetActive(false);
        isFire -= 1;
    }

    protected override void CreateShell()
    {
        Rigidbody shellInstance = shellPool.GetFromPool(m_Shell);
        shellInstance.transform.SetPositionAndRotation(m_FireTransform.position, m_FireTransform.rotation * Quaternion.Euler(Vector3.up * bulletRotation));

        // Set the shell's velocity to the launch force in the fire position's forward direction.
        shellInstance.velocity = m_CurrentLaunchForce * (Quaternion.AngleAxis(bulletRotation, Vector3.up) * m_FireTransform.forward);

    }

    protected override void DelayNextShot()
    {
        countDownDelayTime -= Time.deltaTime;
        if (countDownDelayTime <= 0f)
        {
            isFire = 5;
            countDownDelayTime = delayTime;
            bulletRotation = 0f;
            m_AimSlider.gameObject.SetActive(true);
        }
        m_CurrentLaunchForce = m_MinLaunchForce;
    }
}
