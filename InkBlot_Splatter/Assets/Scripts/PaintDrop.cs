using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintDrop : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float lifeTime;
    [SerializeField] float speed;
    IEnumerator CountDownLife()
    {

        yield return new WaitForSeconds(lifeTime);
        Destroy(this.gameObject);
    }
    void Start()
    {
        Debug.Log("Inkdrop Created");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.down * speed * Time.deltaTime;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            GameObject.Find("PlayerAudio").GetComponent<PlayerAudio>().PlaySplat(); 
           // anim.SetBool("Splat", true);
            Destroy(this.gameObject);
        }
    }
}
