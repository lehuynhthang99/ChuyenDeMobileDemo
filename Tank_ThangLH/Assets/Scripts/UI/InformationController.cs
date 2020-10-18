using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InformationController : MonoBehaviour
{
    public static InformationController instance;
    public List<Image> panels;
    int[] isChosen;

    public InformationController()
    {
        if (instance == null)
            instance = this;
    }

    void Awake()
    {
        isChosen = new int[panels.Count];
    }

    public void SetChosen(int prevPos, int curPos)
    {
        if (prevPos != -1)
        {
            isChosen[prevPos] -= 1;
            if (isChosen[prevPos] <= 0)
                panels[prevPos].gameObject.SetActive(false);
        }
        isChosen[curPos] += 1;
        if (!panels[curPos].gameObject.activeInHierarchy)
            panels[curPos].gameObject.SetActive(true);
    }
}
