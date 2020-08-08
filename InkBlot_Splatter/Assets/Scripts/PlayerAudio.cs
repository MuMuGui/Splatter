using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    [Header("Audio")]
    public AudioClip walkSFX; 
    public AudioClip jumpSFX; 

    public AudioClip splatSFX; 
    public AudioClip levelClearSFX;

    public AudioClip hurtSFX;
    public AudioClip leverSFX;
    public AudioClip dumpSFX;

    private AudioSource audio; 

    
    

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayWalk()
    {
        audio.clip = walkSFX; 
        audio.loop = false; 
        audio.Play(); 
    }

    public void PlayJump()
    {
        audio.clip = jumpSFX; 
        audio.loop = false;
        audio.Play(); 
    }

    public void PlaySplat()
    {
        audio.clip = splatSFX; 
        audio.loop = false;
        audio.Play(); 
    }

    public void PlayClear()
    {
        audio.clip = levelClearSFX; 
        audio.loop = false; 
        audio.Play(); 
    }

    public void StopSound()
    {
        audio.Stop(); 
    }

    public bool Playing()
    {
        return (audio.isPlaying); 
    }

    public AudioClip CurrentClip ()
    {
        return audio.clip; 
    }
    public void Hurt()
    {
        audio.clip = hurtSFX;
        audio.loop = false;
        audio.Play();
    }
    public void Pull()
    {
        audio.clip = leverSFX;
        audio.loop = false;
        audio.Play();
    }
    public void Dump()
    {
        audio.clip = dumpSFX;
        audio.loop = false;
        audio.Play();
    }
}
