using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectButtonScript : MonoBehaviour
{
    public Sprite buttonSprite;
    Image imageBtn1, imageBtn2, imageBtn3;



    // Start is called before the first frame update
    private void Awake()
    {
        imageBtn1 = GameObject.Find("Canvas/SafeAreaPanel/Level1Button").GetComponent<Image>();
        imageBtn2 = GameObject.Find("Canvas/SafeAreaPanel/Level2Button").GetComponent<Image>();
        imageBtn3 = GameObject.Find("Canvas/SafeAreaPanel/Level3Button").GetComponent<Image>();

        int clearLevel = 0;
        if(clearLevel == 0)
        {
            imageBtn1.sprite = buttonSprite;
        }else if(clearLevel <= 1)
        {
            imageBtn1.sprite = buttonSprite;
            imageBtn2.sprite = buttonSprite;
        }else if(clearLevel >= 2)
        {
            imageBtn1.sprite = buttonSprite;
            imageBtn2.sprite = buttonSprite;
            imageBtn3.sprite = buttonSprite;
        }
    }

    public void GoToLevel1()
    {
        SceneManager.LoadScene("Level1");
    }
}
