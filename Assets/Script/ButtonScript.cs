using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
