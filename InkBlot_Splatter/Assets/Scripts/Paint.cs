using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paint : MonoBehaviour
{
    [SerializeField] float flyingSpeed;
    [SerializeField] float lifeTime;
    public float flydirection = 0;
    Animator anim;
    //timer
    [SerializeField] float interval;
    private float delay;
    // Start is called before the first frame update
    IEnumerator CountDownLife() 
    {

        yield return new WaitForSeconds(lifeTime);
        Destroy(this.gameObject);
    }


    void Start()
    {
        StartCoroutine(CountDownLife());
        anim = GetComponent<Animator>();
        if (GameObject.FindWithTag("Player").transform.position.x > this.transform.position.x)
        {

            transform.eulerAngles = new Vector3(0, -180, 0);
        }
        else
        {
            //If the player is facing left
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += flydirection *Vector3.right * flyingSpeed * Time.deltaTime;
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            anim.SetBool("Splat", true);
            //Destroy(this.gameObject);
        }
    }
}
