using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AudioFade 
{
    public static IEnumerator FadeOut(AudioSource audio, float duration, float targetVolume)
    {
        float currentTime = 0; 
        float start = audio.volume; 
        
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime; 
            audio.volume = Mathf.Lerp(start, targetVolume, currentTime/duration); 
            Debug.Log(audio.volume); 
            yield return null;  
        }
        yield break; 
    }
}
