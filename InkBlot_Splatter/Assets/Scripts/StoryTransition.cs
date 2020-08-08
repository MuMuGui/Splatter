using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video; 


public class StoryTransition : MonoBehaviour
{
    [SerializeField]
    int videoTime; 

    //VideoPlayer videoPlayer; 

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GameTransition()); 
        //videoPlayer = GameObject.Find("Video Player").GetComponent<VideoPlayer>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator GameTransition()
    {
        yield return new WaitForSeconds(videoTime); 

        GameObject.Find("LoadTransition").GetComponent<LoadTransition>().LoadNextScene("MainLevel2"); 
    }

    /*public void PauseVideo()
    {
        videoPlayer.Pause(); 
    }

    public void PlayVideo()
    {
        videoPlayer.Play(); 
    }*/


}
