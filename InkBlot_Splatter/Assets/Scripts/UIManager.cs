using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class UIManager : MonoBehaviour
{

    public static UIManager instance; 

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
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
