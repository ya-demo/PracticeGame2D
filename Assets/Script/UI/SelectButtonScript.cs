using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectButtonScript : MonoBehaviour
{
    public Sprite buttonSprite;
    Image imageBtn1, imageBtn2, imageBtn3;

    int clearLevel;

    // Start is called before the first frame update
    private void Awake()
    {
        imageBtn1 = GameObject.Find("Canvas/SafeAreaPanel/SelectPanelBgImage/Level1Button").GetComponent<Image>();
        imageBtn2 = GameObject.Find("Canvas/SafeAreaPanel/SelectPanelBgImage/Level2Button").GetComponent<Image>();
        imageBtn3 = GameObject.Find("Canvas/SafeAreaPanel/SelectPanelBgImage/Level3Button").GetComponent<Image>();

        clearLevel = PlayerPrefs.GetInt("ClearLevel", 0);
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
        BGMController myBGM = GameObject.Find("BGMController").GetComponent<BGMController>();
        myBGM.myAudio.PlayOneShot(myBGM.myButtonClips[0]);
        FadeInOut.instance.SceneFadeInOut("Level1");
    }

    public void GoToLevel2()
    {
        BGMController myBGM = GameObject.Find("BGMController").GetComponent<BGMController>();
        if(clearLevel >= 1)
        {
            myBGM.myAudio.PlayOneShot(myBGM.myButtonClips[0]);
            FadeInOut.instance.SceneFadeInOut("Level2");
        }else{
            myBGM.myAudio.PlayOneShot(myBGM.myButtonClips[1]);
        }
    }
    public void GoToLevel3()
    {
        BGMController myBGM = GameObject.Find("BGMController").GetComponent<BGMController>();
        if(clearLevel >= 2)
        {
            myBGM.myAudio.PlayOneShot(myBGM.myButtonClips[0]);
            FadeInOut.instance.SceneFadeInOut("Level3");
        }else{
            myBGM.myAudio.PlayOneShot(myBGM.myButtonClips[1]);
        }
    }

    public void GoToMenu()
    {
        BGMController myBGM = GameObject.Find("BGMController").GetComponent<BGMController>();
        myBGM.myAudio.PlayOneShot(myBGM.myButtonClips[0]);
        FadeInOut.instance.SceneFadeInOut("MainMenu");
    }
}
