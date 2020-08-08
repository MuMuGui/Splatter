using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPan : MonoBehaviour
{
    [SerializeField] Vector3 offset;
    private Transform playerTrans;
    Transform portal;
    [SerializeField] bool x = false;
    // Start is called before the first frame update
    void Start()
    {
        playerTrans = GameObject.FindGameObjectWithTag("Player").transform;
        portal = GameObject.FindGameObjectWithTag("portal").transform;
    }


    // Update is called once per frame
    void Update()
    {
        transform.position = playerTrans.position + offset;
        if (x)
        {
            transform.position = portal.position + offset;
        }
        
    }
}
