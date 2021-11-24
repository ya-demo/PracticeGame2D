using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
    public GameObject selectPanel, stopButton;

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
        SceneManager.LoadScene("SelectLevel");
    }
}
