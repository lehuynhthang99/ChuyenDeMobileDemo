using UnityEngine;
using UnityEngine.UI;

public abstract class TankShootingBase : BaseMonoBehaviour
{
    public GameObject shellPrefab;
    public int numberShells = 10;
    public Transform m_FireTransform;           // A child of the tank where the shells are spawned.
    public Slider m_AimSlider;                  // A child of the tank that displays the current launch force.
    public AudioSource m_ShootingAudio;         // Reference to the audio source used to play the shooting audio. NB: different to the movement audio source.
    [HideInInspector] public AudioClip m_ChargingClip;            // Audio that plays when each shot is charging up.
    [HideInInspector] public AudioClip m_FireClip;                // Audio that plays when each shot is fired.
    [HideInInspector] public float m_MinLaunchForce = 15f;        // The force given to the shell if the fire button is not held.
    [HideInInspector] public float m_MaxLaunchForce = 30f;        // The force given to the shell if the fire button is held for the max charge time.
    [HideInInspector] public float m_MaxChargeTime = 0.75f;       // How long the shell can charge for before it is fired at max force.

    [HideInInspector] public float delayTime = 1f;
    protected float countDownDelayTime;
    protected int isFire;

    protected float m_CurrentLaunchForce;         // The force that will be given to the shell when the fire button is released.
    protected float m_ChargeSpeed;                // How fast the launch force increases, based on the max charge time.
    protected bool m_Fired;                       // Whether or not the shell has been launched with this button press.
    protected bool isButtonDown, isButtonUp, isButton;

    protected virtual void OnEnable()
    {
        // When the tank is turned on, reset the launch force and the UI
        m_AimSlider.gameObject.SetActive(true);
        m_CurrentLaunchForce = m_MinLaunchForce;
        m_AimSlider.value = m_MinLaunchForce;
        countDownDelayTime = delayTime;
        isFire = 1;
        isButtonDown = isButtonUp = isButton = false;
    }


    protected virtual void Start()
    {
        // The rate that the launch force charges up is the range of possible forces by the max charge time.
        m_ChargeSpeed = (m_MaxLaunchForce - m_MinLaunchForce) / m_MaxChargeTime;
    }


    public override void UpdateNormal()
    {
        // The slider should have a default value of the minimum launch force.
        m_AimSlider.value = m_MinLaunchForce;

        if (isFire == 1)
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

        else
            DelayNextShot();
    }

    protected virtual void DelayNextShot()
    {
        countDownDelayTime -= Time.deltaTime;
        if (countDownDelayTime <= 0f)
        {
            isFire = 1;
            countDownDelayTime = delayTime;
            m_AimSlider.gameObject.SetActive(true);
        }
    }

    protected virtual void Charging()
    {
        // Increment the launch force and update the slider.
        m_CurrentLaunchForce += m_ChargeSpeed * Time.deltaTime;

        m_AimSlider.value = m_CurrentLaunchForce;
    }

    protected virtual void StartCharging()
    {
        // ... reset the fired flag and reset the launch force.
        m_Fired = false;
        m_CurrentLaunchForce = m_MinLaunchForce;

        // Change the clip to the charging clip and start it playing.
        m_ShootingAudio.clip = m_ChargingClip;
        m_ShootingAudio.Play();
    }

    protected virtual void StartLaunchingShells()
    {
        Fire();
        m_CurrentLaunchForce = m_MinLaunchForce;
        m_AimSlider.gameObject.SetActive(false);
        isFire -= 1;
    }

    protected virtual void Fire()
    {
        // Set the fired flag so only Fire is only called once.
        m_Fired = true;
        CreateShell();

        // Change the clip to the firing clip and play it.
        m_ShootingAudio.clip = m_FireClip;
        m_ShootingAudio.Play();

        // Reset the launch force.  This is a precaution in case of missing button events.
        //m_CurrentLaunchForce = m_MinLaunchForce;
    }

    protected abstract void CreateShell();

    public void GetInput(bool _isButtonDown, bool _isButtonUp, bool _isButton)
    {
        isButtonDown = _isButtonDown;
        isButtonUp = _isButtonUp;
        isButton = _isButton;
    }
}

public abstract class TankShootingBase<T> : TankShootingBase where T:Component
{
    protected T m_Shell;                   // Prefab of the shell.
    protected ObjectPooling<T> shellPool;

    public override void Awake()
    {
        base.Awake();
        m_Shell = shellPrefab.GetComponent<T>();
        shellPool = new ObjectPooling<T>();
        shellPool.GrowPool(m_Shell, numberShells);
    }

    public override void UpdateFixed()
    {
        shellPool.UpdateFixed();
    }
}