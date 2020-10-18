using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectedTanks : MonoBehaviour
{
    public static SelectedTanks instance;
    public List<ButtonControll> buttonControlls;
    public List<Color> colors;

    public List<GameObject> tankPrefabs;
    [HideInInspector] public TankManager[] tanks;
    
    public SelectedTanks ()
    {
        if (instance == null)
            instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        foreach (ButtonControll buttonControll in buttonControlls)
            if (!buttonControll.isSet)
                return;

        tanks = new TankManager[buttonControlls.Count];

        for (int i=0; i<buttonControlls.Count; i++)
        {
            
            tanks[i] = new TankManager();
            tanks[i].tankPrefab = tankPrefabs[buttonControlls[i].currentImage];
            tanks[i].m_PlayerColor = colors[i];
        }
        SceneManager.LoadScene("Main");
    }
}
