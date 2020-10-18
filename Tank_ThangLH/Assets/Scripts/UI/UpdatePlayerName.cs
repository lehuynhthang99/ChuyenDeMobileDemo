using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpdatePlayerName : MonoBehaviour
{
    public List<TMP_InputField> texts;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i<texts.Count;i++)
        {
            if (PlayerPrefs.HasKey("Player" + (i + 1)))
                texts[i].text = PlayerPrefs.GetString("Player" + (i + 1));
            else PlayerPrefs.SetString("Player" + (i + 1), "Player" + (i + 1));
        }
    }

    void OnDisable()
    {
        for (int i = 0; i < texts.Count; i++)
            PlayerPrefs.SetString("Player" + (i + 1), texts[i].text);
    }

}
