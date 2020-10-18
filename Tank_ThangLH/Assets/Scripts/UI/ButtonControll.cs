using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonControll : MonoBehaviour
{
    public List<Image> images;
    [HideInInspector] public int currentImage = 0;

    public int playerNumber = 1;
    public KeyCode left = KeyCode.LeftArrow;
    public KeyCode right = KeyCode.RightArrow;
    public KeyCode select = KeyCode.RightControl;

    [HideInInspector] public bool isSet = false;

    // Start is called before the first frame update
    void Start()
    {
        InformationController.instance.SetChosen(-1, 0);
        images[0].gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(select))
            isSet = !isSet;
        if (isSet) return;
        int inputKey = 0;
        if (Input.GetKeyDown(left))
            inputKey = -1;
        else if (Input.GetKeyDown(right))
            inputKey = 1;
        if (inputKey != 0)
        {
            int prevImage = currentImage;
            images[currentImage].gameObject.SetActive(false);
            if (inputKey < 0)
                currentImage -= 1;
            else currentImage += 1;
            currentImage = (currentImage + images.Count) % images.Count;
            images[currentImage].gameObject.SetActive(true);
            InformationController.instance.SetChosen(prevImage, currentImage);
        }
    }
}
