using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class UIAudio : MonoBehaviour
{
    [Header("Audio")]
    public AudioClip buttonSFX; 
    public AudioClip loseSFX; 

    public AudioClip winSFX; 
    private AudioSource audio;

    AudioReference begin;
    bool played; 

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>(); 
        played = false;
       

    }

    // Update is called once per frame
    void Update()
    {

        
        if (SceneManager.GetActiveScene().name == "LoseScene")
        {
            begin = GameObject.Find("StartAudio").GetComponent<AudioReference>();
        }
        else if (SceneManager.GetActiveScene().name == "WinScene")
        {
            begin = GameObject.Find("StartAudio").GetComponent<AudioReference>();
        }

        if (!audio.isPlaying && !begin.UIAudioStart)
        {
            
            
            if (SceneManager.GetActiveScene().name == "LoseScene")
            {
                Debug.Log("You died");
                
                //played = true; 
                audio.clip = loseSFX; 
                audio.loop = false; 
                audio.Play();
                begin.UIAudioStart = true;
            }
            else if (SceneManager.GetActiveScene().name == "WinScene")
            {
                audio.clip = winSFX;
                audio.loop = false;
                audio.Play();
                begin.UIAudioStart = true;
            }


        }
        /*
        else if (begin.UIAudioStart && (SceneManager.GetActiveScene().name != "LoseScene")); 
        {
            //audio.Stop();
           begin.UIAudioStart = false; 
        }
        */
    }

    public void PlayClick()
    {
        audio.clip = buttonSFX; 
        audio.loop = false; 
        audio.Play(); 
    }

    public void PlayLose()
    {
        audio.clip = loseSFX; 
        audio.loop = false; 
        audio.Play(); 
    }

    public void PlayWin()
    {
        audio.clip = winSFX; 
        audio.loop = false; 
        audio.pitch = Random.Range(0.9f, 1.1f); 
        audio.Play(); 
    }
 
}
