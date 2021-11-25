using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
    public GameObject selectPanel, stopButton, levelSelectBtn, MenuBtn, ReplyBtn;

    public void LevelSelectBtnClick()
    {
        BGMController myBGM = GameObject.Find("BGMController").GetComponent<BGMController>();
        myBGM.myAudio.PlayOneShot(myBGM.myButtonClips[0]);
        FadeInOut.instance.SceneFadeInOut("SelectLevel");
    }
    public void MenuBtnClick()
    {
        BGMController myBGM = GameObject.Find("BGMController").GetComponent<BGMController>();
        myBGM.myAudio.PlayOneShot(myBGM.myButtonClips[0]);
        FadeInOut.instance.SceneFadeInOut("MainMenu");
    }
    public void ReplyBtnClick()
    {
        BGMController myBGM = GameObject.Find("BGMController").GetComponent<BGMController>();
        myBGM.myAudio.PlayOneShot(myBGM.myButtonClips[0]);
        string sceneName = SceneManager.GetActiveScene().name;
        FadeInOut.instance.SceneFadeInOut(sceneName);
    }

    public void NoDeleteButton()
    {
        BGMController myBGM = GameObject.Find("BGMController").GetComponent<BGMController>();
        myBGM.myAudio.PlayOneShot(myBGM.myButtonClips[0]);
        RectTransform dataDeleteTransform = GameObject.Find("Canvas/SafeAreaPanel/DataDeleteImage").GetComponent<RectTransform>();
        dataDeleteTransform.anchoredPosition = new Vector2(0f,1500f);
    }

    public void YesDeleteButton()
    {
        BGMController myBGM = GameObject.Find("BGMController").GetComponent<BGMController>();
        myBGM.myAudio.PlayOneShot(myBGM.myButtonClips[0]);
        PlayerPrefs.DeleteAll();
        IsFirstTimePlayCheck checkScript = GameObject.Find("IsFirstTimePlayCheck").GetComponent<IsFirstTimePlayCheck>();
        checkScript.FirstTimePlayState();
        RectTransform dataDeleteTransform = GameObject.Find("Canvas/SafeAreaPanel/DataDeleteImage").GetComponent<RectTransform>();
        dataDeleteTransform.anchoredPosition = new Vector2(0f,1500f);
    }

    public void DataDeleteButton()
    {
        BGMController myBGM = GameObject.Find("BGMController").GetComponent<BGMController>();
        myBGM.myAudio.PlayOneShot(myBGM.myButtonClips[0]);
        RectTransform dataDeleteTransform = GameObject.Find("Canvas/SafeAreaPanel/DataDeleteImage").GetComponent<RectTransform>();
        dataDeleteTransform.anchoredPosition = new Vector2(0f,-100f);
    }

    public void SetSelectPanelOn()
    {
        BGMController myBGM = GameObject.Find("BGMController").GetComponent<BGMController>();
        myBGM.myAudio.PlayOneShot(myBGM.myButtonClips[0]);
        selectPanel.SetActive(true);
        Time.timeScale = 0f;
    }
    public void SetStopButtonOn()
    {
        BGMController myBGM = GameObject.Find("BGMController").GetComponent<BGMController>();
        myBGM.myAudio.PlayOneShot(myBGM.myButtonClips[0]);
        stopButton.SetActive(true);
    }
    public void SetStopButtonOff()
    {
        BGMController myBGM = GameObject.Find("BGMController").GetComponent<BGMController>();
        myBGM.myAudio.PlayOneShot(myBGM.myButtonClips[0]);
        stopButton.SetActive(false);
    }
    public void SetSelectPanelOff()
    {
        BGMController myBGM = GameObject.Find("BGMController").GetComponent<BGMController>();
        myBGM.myAudio.PlayOneShot(myBGM.myButtonClips[0]);
        selectPanel.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void MainMenuPlayButton()
    {
        GameObject mainMenuPlayer = GameObject.Find("MenuPlayer");
        Animator myAnim = mainMenuPlayer.GetComponent<Animator>();
        myAnim.SetBool("Run", true);
        GameObject playButton = GameObject.Find("Canvas/SafeAreaPanel/PlayButton");
        playButton.SetActive(false);

        BGMController myBGM = GameObject.Find("BGMController").GetComponent<BGMController>();
        myBGM.myAudio.PlayOneShot(myBGM.myButtonClips[0]);

        FadeInOut.instance.SceneFadeInOut("SelectLevel");
    }
}
