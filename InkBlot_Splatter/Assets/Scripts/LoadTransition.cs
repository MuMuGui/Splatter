using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class LoadTransition : MonoBehaviour
{
    public Animator transition; 

    public float transitionTime = 1f; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LoadNextScene(string sceneName)
    {
        StartCoroutine(LoadScene(sceneName)); 
    }

    public void LoadGame()
    {
        StartCoroutine(LoadScene("MainLevel2")); 
    }

    public void LoadCredits()
    {
        StartCoroutine(LoadScene("CreditScene")); 
    }

    public void LoadStart()
    {
        StartCoroutine(LoadScene("StartScene")); 
    }

    public void LoadPause()
    {
        GameObject.Find("Pause").GetComponent<PauseMenu>().Pause(); 
    }
    public void LoadWin()
    {
        StartCoroutine(LoadScene("WinScene"));
    }
    public void LoadLose()
    {
        StartCoroutine(LoadScene("LoseScene"));
    }
    public void LoadBoss()
    {
        GameObject.Find("MusicBox").GetComponent<Music>().StopSound();
        StartCoroutine(LoadScene("BossLevel"));

    }
    public void LoadStory()
    {
        StartCoroutine(LoadScene("StoryScene"));
    }
    public void LoadLevel()
    {
        StartCoroutine(LoadScene("LevelSelect"));
    }
    IEnumerator LoadScene(string sceneName)
    {
        transition.SetTrigger("start"); 

        yield return new WaitForSeconds(transitionTime); 

        if (sceneName == "StartScene")
        {
            GameObject.Find("MusicBox").GetComponent<Music>().ChangeMusic();
            GameManager.instance.ToStart(); 
        }

        else if (sceneName == "MainLevel2")
        {
            GameObject.Find("MusicBox").GetComponent<Music>().ChangeMusic();
            GameManager.instance.ToGame(); 
        }

        else if (sceneName == "WinScene")
        {
            //GameObject.Find("MusicBox").GetComponent<Music>().ChangeMusic();
            GameObject.Find("MusicBox").GetComponent<Music>().StopSound();
            GameManager.instance.ToWin();
        }

        else if (sceneName == "LoseScene")
        {
            GameObject.Find("MusicBox").GetComponent<Music>().StopSound();
            GameManager.instance.ToLose(); 
        }

        else if (sceneName == "CreditScene")
        {
            GameObject.Find("MusicBox").GetComponent<Music>().ChangeMusic();
            GameManager.instance.ToCredits(); 
        }
        else if(sceneName == "BossLevel")
        {
            GameManager.instance.ToBoss();
        }
        else if(sceneName == "StoryScene")
        {
            //StartCoroutine(AudioFade.FadeOut(GameObject.Find("MusicBox").GetComponent<Music>().GetSource(), 1f, 0f)); 
            GameObject.Find("MusicBox").GetComponent<Music>().StopSound();
            GameManager.instance.ToStory();
        }
        else if (sceneName == "LevelSelect")
        {
            GameObject.Find("MusicBox").GetComponent<Music>().ChangeMusic();
            GameManager.instance.ToLevel();
        }
    }
}
