using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class GameManager : MonoBehaviour
{
    public static GameManager instance; 

    private void Awake()
    {
        if (instance == null)
        {
            instance = this; 
            DontDestroyOnLoad(this.gameObject); 
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ToStart()
    {
        SceneManager.LoadScene("StartScene"); 
    }

    public void ToGame()
    {
        SceneManager.LoadScene("MainLevel2"); //go to main game scene
    }

    public void ToWin()
    {
        SceneManager.LoadScene("WinScene"); 
    }

    public void ToLose()
    {
        SceneManager.LoadScene("LoseScene"); 
    }

    public void ToCredits()
    {
        SceneManager.LoadScene("CreditScene"); 
    }
    public void ToBoss()
    {
        SceneManager.LoadScene("BossLevel");
    }
    public void ToStory()
    {
        SceneManager.LoadScene("StoryScene");
    }
    public void ToLevel()
    {
        SceneManager.LoadScene("LevelSelect");
    }
}
