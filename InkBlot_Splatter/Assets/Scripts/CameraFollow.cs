using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CameraFollow : MonoBehaviour
{
    [SerializeField] Vector3 offset;
    private Transform playerTrans;
    Boss pan;
    Transform Boss;
    // Start is called before the first frame update
    void Start()
    {
        playerTrans = GameObject.FindGameObjectWithTag("Player").transform;
        pan = GameObject.Find("BossTemp").GetComponent<Boss>();
        Boss = GameObject.Find("BossTemp").transform;
    }


    // Update is called once per frame
    void Update()
    {
        if(SceneManager.GetActiveScene().name == "BossLevel")
        {
            if (pan.good == true)
            {
                transform.position = Boss.position+ offset;
            }
            else
            {
                transform.position = playerTrans.position + offset;
            }
        }
        else
        {
            transform.position = playerTrans.position + offset;
        }

       
    }
}
