using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Music : MonoBehaviour
{
    private AudioSource audio; 

    [SerializeField]
    public AudioClip regBackground; 
    public AudioClip bossBattle;
    public AudioClip introBoss;
    public AudioClip storyMusic; 

    private bool audioBlendInProgress; 

    public static Music instance;
    AudioReference begin;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this; 
        }
        else
        {
            Destroy(this.gameObject); 
        }
        DontDestroyOnLoad(transform.gameObject);
        //audio = GetComponent<AudioSource>(); 
        //PlayMusic(); 
    }

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        PlayMusic();
        
    }

    // Update is called once per frame
    void Update()
    {
        //intro music
        if (SceneManager.GetActiveScene().name == "BossLevel")
        {

            begin = GameObject.Find("StartAudio").GetComponent<AudioReference>();
        }
        else if (!audio.isPlaying && SceneManager.GetActiveScene().name == "StoryScene")
        {
            StoryPlay();
        }

        if (!audio.isPlaying && !begin.UIAudioStart)
        {
            if (SceneManager.GetActiveScene().name == "BossLevel")
            {
                audio.clip = introBoss;
                audio.loop = false;
                audio.Play();
                begin.UIAudioStart = true;
            }

        }
        else if (!audio.isPlaying && SceneManager.GetActiveScene().name == "BossLevel" && begin.UIAudioStart == true)
        {
            
            BossMusic();
        }


    }

    public void PlayMusic()
    {
        if (!audio.isPlaying)
        {
            audio.clip = regBackground;
            audio.loop = true; 
            audio.Play(); 
        }
    }
    public void StopSound()
    {
        audio.Stop();
        audio.clip = null; 
    }
    public void BossMusic()
    {
        audio.clip = bossBattle;
        audio.loop = true;
        audio.Play();
    }
    public void ChangeMusic()
    {
        if(audio.clip != regBackground)
        {
            StopSound();
            PlayMusic();
        }

    }
    public void StoryPlay()
    {
        audio.clip = storyMusic;
        audio.loop = true;
        audio.Play();
    }

    public AudioSource GetSource()
    {
        return audio; 
    }

    /*public IEnumerator FadeOut(float FadeTime)
    {
        float startVolume = audio.volume; 
        float adjustedVolume = startVolume; 

        while (adjustedVolume > 0)
        {
            adjustedVolume -= startVolume * Time.deltaTime / FadeTime; 
            audio.volume = adjustedVolume;
            Debug.Log(adjustedVolume); 
            yield return null; 
        }

        //yield return new WaitForSeconds(FadeTime); 
        audio.Stop(); 
        audio.volume = startVolume; 
    }

    public IEnumerator FadeIn(float FadeTime)
    {
        float startVolume = 0.2f; 

        audio.volume = 0; 
        audio.Play(); 

        while (audio.volume < 1.0f)
        {
            audio.volume += startVolume * Time.deltaTime / FadeTime; 

            yield return null; 
        }
        audio.volume = 1f; //prob have to change this to go back to
        //what the player prefers in the slider 
    }*/
}
