using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartingScene : MonoBehaviour
{
    public void GoToNextScene()
    {

        SceneManager.LoadScene("Home Menu");
    }
}
