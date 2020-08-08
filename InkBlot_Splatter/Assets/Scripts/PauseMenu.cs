using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false; 
    public GameObject pauseMenuUI; 

    public static PauseMenu instance; 

    void Awake()
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
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause(); 
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false); 
        /*if (!SceneManager.GetActiveScene().name == "StoryScene")
        {
            Time.timeScale = 1f; 
        }*/
        Time.timeScale = 1f; 
        GameIsPaused = false; 
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true); 
        if (SceneManager.GetActiveScene().name != "StoryScene")
        {
            Time.timeScale = 0f; 
        }
        GameIsPaused = true; 
    }

    public void LoadMenu()
    {
        GameObject.Find("MusicBox").GetComponent<Music>().ChangeMusic();
        Time.timeScale = 1f; 
        pauseMenuUI.SetActive(false); 
        GameManager.instance.ToStart();
        
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game..."); 
        Application.Quit(); 
    }
}
