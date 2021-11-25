using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "Player")
        {
            string levelName = SceneManager.GetActiveScene().name;
            string levelNum = levelName.Substring(5);
            int level = int.Parse(levelNum);
            if(PlayerPrefs.GetInt("ClearLevel") < level)
            {
                PlayerPrefs.SetInt("ClearLevel", level);
            }
            Time.timeScale = 0f;
            FadeInOut.instance.SceneFadeInOut("SelectLevel");
        }
    }
}
