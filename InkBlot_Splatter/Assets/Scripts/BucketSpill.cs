using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BucketSpill : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject PaintAmmo;
    Animator anim;
    bool spillonce;
    IEnumerator Countdown()
    {
        
        yield return new WaitForSeconds(10);
        Respawn();
        
    }
    void Start()
    {
        anim = GetComponent<Animator>();
        spillonce = false;
        anim.SetBool("Turn", false);
        //StartCoroutine(Respawn());
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Spill()
    {
        if (spillonce == false)
        {
            GameObject.Find("PlayerAudio").GetComponent<PlayerAudio>().Dump();
            GameObject goodBlob = Instantiate(PaintAmmo) as GameObject;
            goodBlob.transform.position = transform.position;
            // transform.eulerAngles = new Vector3(0, 0, 90);
            anim.SetBool("Turn", true);
            //GetComponent<Animator>().Play("OragneFlip", -1, 0f);
            //anim.SetBool("Turn", false);
            StartCoroutine(Countdown());

        }
        spillonce = true;

    }
    void Respawn()
    {
        anim.SetBool("Turn", false);
        spillonce = false;
    }
}
