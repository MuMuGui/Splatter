using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] ParticleSystem spray;
    [SerializeField] bool x;
    public BossState currentState;
    Rigidbody2D rb;
    bool done;
    Animator anim;
    [SerializeField]  float force;
    [SerializeField] float speed;
    //
   public bool good= false;
    bool died = false;


    public enum BossState
    {
      Shoot,
      Move,
      Calm
    }
    IEnumerator ShootTimer()
    {
        currentState = BossState.Shoot;
        yield return new WaitForSeconds(5);
        //Random.Range(2,5))
        currentState = BossState.Move;
        if (done == false)
        {
            StartCoroutine(MoveTimer());
        }
        

    }
    IEnumerator MoveTimer()
    {
        currentState = BossState.Move;
        yield return new WaitForSeconds(5);
        //Random.Range(2,5))
        if (done == false)
        {
            StartCoroutine(ShootTimer());
        }
        


    }
    IEnumerator DeathTimer()
    {
        GameObject.Find("EnemyAudio").GetComponent<EnemyAudio>().DieSound();
        yield return new WaitForSeconds(6);
        GameObject.Find("LoadTransition").GetComponent<LoadTransition>().LoadNextScene("WinScene");
    }
    //health bar
    [Header("Health")]
    public int maxHealth = 90;
    public int currentHealth;
    [SerializeField]
    int paintDamage;

    public HealthBar healthBar;
     

    void Start()
    {
        StartCoroutine(MoveTimer());
        currentState = BossState.Move;
        rb = GetComponent<Rigidbody2D>();
        healthBar.SetMaxHealth(maxHealth);
        currentHealth = maxHealth;
        done = false;
        anim = GetComponent<Animator>();
        anim.SetBool("Attack", false);
        //


    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0 && died ==false)
        {

            StartCoroutine(DeathTimer());
            died = true;
            
        }
        if (currentState == BossState.Shoot && good == false) 
        {
            anim.SetBool("Attack", true);
            
            if (x)
            {
                //
                var MySystem = spray;
                var Shape = MySystem.shape;
                var Emission = MySystem.emission;
                var MyRotation = Shape.rotation;
                //ParticleSystem.shape.rotation = new Vector3(0f, 180f, 0f);
                if (GameObject.FindWithTag("Player").transform.position.x > this.transform.position.x)
                {
                    Shape.rotation = new Vector3(0f, 150f, 0f);
                    
                }
                else
                {
                    //If the player is facing left
                    Shape.rotation = new Vector3(0f, 85f, 0f);
                }

                GameObject.Find("EnemyAudio").GetComponent<EnemyAudio>().SpraySound();
                spray.Play();
                x = false;
            }
            
        }

        else if (currentState == BossState.Move && good == false)
        {
            if (!GameObject.Find("EnemyAudio").GetComponent<EnemyAudio>().PlayingBoss())
            {
                GameObject.Find("EnemyAudio").GetComponent<EnemyAudio>().Move();
            }
                        
            anim.SetBool("Attack", false);
            spray.Stop();
            x = true;
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(GameObject.FindGameObjectWithTag("Player").transform.position.x, transform.position.y), speed * Time.deltaTime);
            
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
        else if(currentState == BossState.Calm)
        {
            spray.Stop();
            good = true;
            anim.SetBool("isDead", true);
            this.GetComponent<Rigidbody2D>().simulated = false;
            transform.position = new Vector3(this.transform.position.x, this.transform.position.y, 0);
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "goodPaint")
        {
            Destroy(collision.gameObject);
            TakeDamage(paintDamage);

            Debug.Log("30");
            if (currentHealth <= 0)
            {
                currentState = BossState.Calm;
                done = true;
                //Destroy(gameObject);
            }
        }
    }
    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }
}
