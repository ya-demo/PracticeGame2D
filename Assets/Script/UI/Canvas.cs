using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Canvas : MonoBehaviour
{
    public Text lifeText, kunaiText, stoneText;
    private void Awake()
    {
        LifeUpdate();
        KunaiUpdate();
        StoneUpdate();
    }
    
    public void LifeUpdate()
    {
        lifeText.text = "x" + PlayerPrefs.GetInt("PlayerLife").ToString();
    }

    public void KunaiUpdate()
    {
        kunaiText.text = "x" + PlayerPrefs.GetInt("PlayerKunai").ToString();
    }

    public void StoneUpdate()
    {
        stoneText.text = "x" + PlayerPrefs.GetInt("PlayerStone").ToString();
    }

}
