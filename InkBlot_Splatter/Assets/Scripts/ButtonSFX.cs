using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSFX : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ButtonClick()
    {
        GameObject.Find("UIAudio").GetComponent<UIAudio>().PlayClick(); 
    }
}
