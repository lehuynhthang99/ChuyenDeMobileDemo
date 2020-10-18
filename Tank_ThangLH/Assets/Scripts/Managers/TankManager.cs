using System;
using UnityEngine;

[Serializable]
public class TankManager
{
    public GameObject tankPrefab;
    public Color m_PlayerColor;            
    public Transform m_SpawnPoint;         
    [HideInInspector] public int m_PlayerNumber;             
    [HideInInspector] public string m_ColoredPlayerText;
    [HideInInspector] public GameObject m_Instance;          
    [HideInInspector] public int m_Wins;
    private string m_PlayerName;

    private bool isActive = false;
    private TankInfo m_TankInfo;
    private TankMovement m_Movement;       
    private TankShootingBase m_Shooting;
    private GameObject m_CanvasGameObject;
    private TankHealth m_TankHealth;
    private TankEffects m_TankEffects;
    private SkillBase m_SkillBase;

    ////Input
    //Shooting
    private string m_FireButton;

    //Movement
    private string m_MovementAxisName;
    private string m_TurnAxisName;

    //Skill
    private string m_Skill;

    public void Setup()
    {
        m_PlayerName = PlayerPrefs.GetString("Player" + m_PlayerNumber);
        //Input
        m_FireButton = "Fire" + m_PlayerNumber;
        m_MovementAxisName = "Vertical" + m_PlayerNumber;
        m_TurnAxisName = "Horizontal" + m_PlayerNumber;
        m_Skill = "Skill" + m_PlayerNumber;

        m_TankInfo = m_Instance.GetComponent<TankInfo>();
        if (!m_TankInfo)
            return;
        m_Movement = m_TankInfo.tankMovement;
        m_Shooting = m_TankInfo.tankShooting;
        m_CanvasGameObject = m_TankInfo.tankCanvas;
        m_TankEffects = m_TankInfo.tankEffects;
        m_TankHealth = m_TankInfo.tankHealth;
        m_SkillBase = m_TankInfo.skill;
        m_TankInfo.id = m_PlayerNumber;


        m_ColoredPlayerText = "<color=#" + ColorUtility.ToHtmlStringRGB(m_PlayerColor) + ">" + m_PlayerName + "</color>";

        MeshRenderer[] renderers = m_Instance.GetComponentsInChildren<MeshRenderer>();

        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].material.color = m_PlayerColor;
        }
    }


    public void DisableControl()
    {
        m_Movement.enabled = false;
        m_Shooting.enabled = false;
        m_TankEffects.enabled = false;
        m_TankHealth.enabled = false;
        m_SkillBase.enabled = false;


        m_CanvasGameObject.SetActive(false);
    }


    public void EnableControl()
    {
        m_Movement.enabled = true;
        m_Shooting.enabled = true;
        m_TankEffects.enabled = true;
        m_TankHealth.enabled = true;
        m_SkillBase.enabled = true;

        m_CanvasGameObject.SetActive(true);
    }


    public void Reset()
    {
        m_Instance.transform.SetPositionAndRotation(m_SpawnPoint.position, m_SpawnPoint.rotation);

        m_Instance.SetActive(false);
        m_Instance.SetActive(true);
    }

    public void InputControl()
    {
        m_Movement.GetInput(Input.GetAxis(m_MovementAxisName), Input.GetAxis(m_TurnAxisName));
        m_Shooting.GetInput(Input.GetButtonDown(m_FireButton), Input.GetButtonUp(m_FireButton), Input.GetButton(m_FireButton));
        m_SkillBase.GetInput(Input.GetButtonDown(m_Skill));
    }
}
