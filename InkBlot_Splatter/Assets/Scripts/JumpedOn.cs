using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JumpedOn : MonoBehaviour
{
    EnemyScript touched;
    [SerializeField] public int turnedGood = 0;
    [SerializeField] Text scoreCount;
    [SerializeField] public int hit;
    // Start is called before the first frame update
    ScoreCounter activate;
    [SerializeField] AudioSource splat;



    void Start()
    {
        turnedGood = 0;
        touched = transform.parent.GetComponent<EnemyScript>();
        activate = GameObject.FindWithTag("score").GetComponent<ScoreCounter>();
        splat = GetComponent<AudioSource>();
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" || collision.tag =="goodPaint")
        {
            if (collision.tag == "Player")
            {
                GameObject.Find("PlayerAudio").GetComponent<PlayerAudio>().PlaySplat();
            }



            if (touched.notBad == false)
            {
                activate.Count();
            }

            
            touched.notBad = true;
           // Debug.Log("jump" +turnedGood);
            //transform.gameObject.tag = "good";

        }

    }

}
