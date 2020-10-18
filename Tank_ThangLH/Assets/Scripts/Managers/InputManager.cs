using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;
    GameManager gameManager = GameManager.instance;

    public InputManager()
    {
        if (instance == null)
            instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < gameManager.m_Tanks.Length; i++)
        {
            gameManager.m_Tanks[i].InputControl();
        }
    }
}
