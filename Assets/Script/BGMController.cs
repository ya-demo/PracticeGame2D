using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMController : MonoBehaviour
{
    public AudioClip[] myBGMClips;
    public AudioClip[] myButtonClips;

    [HideInInspector]
    public AudioSource myAudio;

    private void Awake()
    {
        myAudio = GetComponent<AudioSource>();
        string sceneName = SceneManager.GetActiveScene().name;

        if(sceneName == "MainMenu")
        {
            myAudio.clip = myBGMClips[0];
            myAudio.loop = true;
            myAudio.Play();
        }else if(sceneName == "SelectLevel")
        {
            myAudio.clip = myBGMClips[1];
            myAudio.loop = true;
            myAudio.Play();
        }else if(sceneName == "Level1" || sceneName == "Level2")
        {
            myAudio.clip = myBGMClips[2];
            myAudio.loop = true;
            myAudio.Play();
        }else if(sceneName == "Level3")
        {
            myAudio.clip = myBGMClips[3];
            myAudio.loop = true;
            myAudio.Play();
        }
    }
}
