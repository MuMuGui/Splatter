using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAudio : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioClip spraySFX;
    public AudioClip dieSFX;
    public AudioClip jumpSFX;
    private AudioSource audio;
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SpraySound()
    {
        audio.clip = spraySFX;
        audio.loop = false;
        audio.Play();
    }
    public void DieSound()
    {
        audio.clip = dieSFX;
        audio.loop = false;
        audio.Play();
    }
    public void Move()
    {
        audio.clip = jumpSFX;
        audio.loop = false;
        audio.Play();
    }
    public bool PlayingBoss()
    {
        return (audio.isPlaying);
    }
}
