using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paint_Shoot : MonoBehaviour
{
    [SerializeField] GameObject paintPrefab;
    [SerializeField] float shootInterval;
    private float shootTimeCounter;
    // Start is called before the first frame update
    EnemyScript close;

    void Start()
    {
        shootTimeCounter = shootInterval;
        close = transform.GetComponent<EnemyScript>();
    }

    // Update is called once per frame
    void Update()
    {
        shootTimeCounter -= Time.deltaTime;
        if (shootTimeCounter <= 0)
        {
            if (this.close.prox == true) 
            {
                GameObject newBlob = Instantiate(paintPrefab) as GameObject;
                //newBlob.transform.position = transform.position + Vector3.right * .5f;
                shootTimeCounter = shootInterval;
                //GameObject.FindWithTag("Enemy").transform.position.x > this.transform.position.x
                if (GameObject.FindWithTag("Player").transform.position.x > this.transform.position.x)
                {

                    newBlob.GetComponent<Paint>().flydirection = 1;
                    newBlob.transform.position = transform.position + Vector3.right * 0.5f;
                }
                else
                {
                    //If the player is facing left
                    newBlob.GetComponent<Paint>().flydirection = -1;
                    newBlob.transform.position = transform.position + Vector3.left * 0.5f;
                }
            }

           
                
            

                
            
            
        }

    }
}
