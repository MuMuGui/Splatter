using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerDetection : MonoBehaviour
{
    EnemyScript state;


    // Start is called before the first frame update
    void Start()
    {
        state = this.transform.parent.GetComponent<EnemyScript>();
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && state.stop == false)
        {
           state.prox = true;
            Debug.Log("Hit");
            //this works
            // add local variable

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            state.prox = false;
            Debug.Log("gone");
           // Debug.Log(EnemyScript.prox);

        }
    }

}
