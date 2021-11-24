using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
    public GameObject selectPanel, stopButton, levelSelectBtn, MenuBtn, ReplyBtn;

    public void LevelSelectBtnClick()
    {
        FadeInOut.instance.SceneFadeInOut("SelectLevel");
    }
    public void MenuBtnClick()
    {
        FadeInOut.instance.SceneFadeInOut("MainMenu");
    }
    public void ReplyBtnClick()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        FadeInOut.instance.SceneFadeInOut(sceneName);
    }

    public void NoDeleteButton()
    {
        RectTransform dataDeleteTransform = GameObject.Find("Canvas/SafeAreaPanel/DataDeleteImage").GetComponent<RectTransform>();
        dataDeleteTransform.anchoredPosition = new Vector2(0f,1500f);
    }

    public void YesDeleteButton()
    {
        PlayerPrefs.DeleteAll();
        IsFirstTimePlayCheck checkScript = GameObject.Find("IsFirstTimePlayCheck").GetComponent<IsFirstTimePlayCheck>();
        checkScript.FirstTimePlayState();
        RectTransform dataDeleteTransform = GameObject.Find("Canvas/SafeAreaPanel/DataDeleteImage").GetComponent<RectTransform>();
        dataDeleteTransform.anchoredPosition = new Vector2(0f,1500f);
    }

    public void DataDeleteButton()
    {
        RectTransform dataDeleteTransform = GameObject.Find("Canvas/SafeAreaPanel/DataDeleteImage").GetComponent<RectTransform>();
        dataDeleteTransform.anchoredPosition = new Vector2(0f,-100f);
    }

    public void SetSelectPanelOn()
    {
        selectPanel.SetActive(true);
        Time.timeScale = 0f;
    }
    public void SetStopButtonOn()
    {
        stopButton.SetActive(true);
    }
    public void SetStopButtonOff()
    {
        stopButton.SetActive(false);
    }
    public void SetSelectPanelOff()
    {
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
        FadeInOut.instance.SceneFadeInOut("SelectLevel");
    }
}
