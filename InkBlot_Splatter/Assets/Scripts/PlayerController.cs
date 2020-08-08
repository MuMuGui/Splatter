using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    enum PlayerState
    {
        Idle,
        Run,
        Jump,
        Dead
    };

    PlayerState currentState;
    Rigidbody2D rb;
    Animator anim;
    [SerializeField]
    private float jumpForce = 5.0f;

    [Header("Movement")]
    public float gravityMultiplier = 1.5f; 
    private float h;
    [SerializeField]
    private float speed = 5.0f;

    [Header("Collision")]
    public bool onGround = false;
    public float castLength;
    public LayerMask groundLayer;

    [Header("Health")]
    public int maxHealth = 100;
    public int currentHealth;
    [SerializeField]
    int paintDamage;
    public HealthBar healthBar;
    [SerializeField]
    int touchDamage;
    [SerializeField]
    int sprayDmg;


    private bool triggered;
    [SerializeField] Collider2D collisionObj;

    bool once;
    [SerializeField] int EnemyKill;
    // Start is called before the first frame update
    IEnumerator DeathCountdown()
    {
        
        this.GetComponent<Rigidbody2D>().simulated = false;
        anim.SetBool("isJump", false);
        anim.SetBool("isWalk", false);
        //anim.SetBool("isDead", true);
        currentState = PlayerState.Dead;
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
        GameObject.Find("LoadTransition").GetComponent<LoadTransition>().LoadNextScene("LoseScene");
    }
    IEnumerator SwitchAnimation()
    {
        yield return new WaitForSeconds(1);
        anim.SetBool("isFlip", false);
    }
    IEnumerator JumpAnimation()
    {
        
        yield return new WaitForSeconds(.1f);
        anim.SetBool("isJump", true);

    }

    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        currentState = PlayerState.Idle;
        rb.gravityScale = gravityMultiplier; 
        jumpForce *= Mathf.Sqrt(gravityMultiplier); 
        triggered = false;
        anim.SetBool("isDead", false);
        once = false;
    }

    // Update is called once per frame
    void Update()
    {
        h = Input.GetAxis("Horizontal");

        //flip
        if (h > 0)
        {
            transform.localScale = new Vector3(-0.5f, 0.5f, 1f);
        }
        else if (h < 0)
        {
            transform.localScale = new Vector3(0.5f, 0.5f, 1f);
        }

        onGround = Physics2D.Raycast(transform.position, Vector2.down, castLength, groundLayer);
        Debug.DrawRay(transform.position, Vector2.down * castLength, Color.red);

        //jump
        if (Input.GetKeyDown(KeyCode.Space) && onGround)
        {
            Jump();
        }

        if (currentState == PlayerState.Idle)
        {
            if (h != 0)
            {
                anim.SetBool("isWalk", true);
                currentState = PlayerState.Run;
            }
        }
        else if (currentState == PlayerState.Run)
        {
            if (h == 0)
            {
                anim.SetBool("isWalk", false);
                currentState = PlayerState.Idle;
            }
        }

        else if (currentState == PlayerState.Jump)
        {
            if (rb.velocity.y <= 0.1f && onGround)
            {
                anim.SetBool("isJump", false);
                //is idle?
                if (h == 0)
                {
                    currentState = PlayerState.Idle;
                }
                //is run?
                else
                {
                    anim.SetBool("isWalk", true);
                    currentState = PlayerState.Run;
                }
            }
            else
            {
                anim.SetBool("isWalk", false); 
            }
        }
        else if (currentState == PlayerState.Dead)
        {
            anim.SetBool("isDead", true);
        }


        if (h != 0)
        {
            if (!GameObject.Find("PlayerAudio").GetComponent<PlayerAudio>().Playing() && onGround)
            {
                GameObject.Find("PlayerAudio").GetComponent<PlayerAudio>().PlayWalk();
            }
        }
        else if (GameObject.Find("PlayerAudio").GetComponent<PlayerAudio>().CurrentClip() == GameObject.Find("PlayerAudio").GetComponent<PlayerAudio>().walkSFX)
        {
            GameObject.Find("PlayerAudio").GetComponent<PlayerAudio>().StopSound();
        }

        if (triggered && Input.GetKeyDown(KeyCode.Z))
        {
            GameObject.Find("PlayerAudio").GetComponent<PlayerAudio>().Pull();
            if (collisionObj.tag == "o_trigger")
            {
                anim.SetBool("isFlip", true);
                GameObject oBucket = GameObject.FindWithTag("o_bucket");
                oBucket.GetComponent<BucketSpill>().Spill();
                StartCoroutine(SwitchAnimation());

            }
            else if (collisionObj.tag == "p_trigger")
            {
                anim.SetBool("isFlip", true);
                GameObject pBucket = GameObject.FindWithTag("p_bucket");
                pBucket.GetComponent<BucketSpill>().Spill();
                StartCoroutine(SwitchAnimation());
            }
            else if (collisionObj.tag == "y_trigger")
            {
                anim.SetBool("isFlip", true);
                GameObject yBucket = GameObject.FindWithTag("y_bucket");
                yBucket.GetComponent<BucketSpill>().Spill();
                StartCoroutine(SwitchAnimation());
            }
        }


    }

    private void FixedUpdate() //runs on each physics steps
    {
        rb.velocity = new Vector2(h * speed, rb.velocity.y);
    }

    void Jump()
    {
        currentState = PlayerState.Jump;
        GameObject.Find("PlayerAudio").GetComponent<PlayerAudio>().PlayJump();
        rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        StartCoroutine(JumpAnimation());
       // anim.SetBool("isJump", true);


    }

    void TakeDamage(int damage)
    {
        
        currentHealth -= damage; 
        healthBar.SetHealth(currentHealth);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "paint")
        {
            GameObject.Find("PlayerAudio").GetComponent<PlayerAudio>().Hurt();
            TakeDamage(paintDamage);
            Destroy(collision.gameObject);
            if (currentHealth <= 0)
            {
                if(once == false)
                {
                    StartCoroutine(DeathCountdown());
                    once = true;
                    
                }
                
                //Destroy(gameObject); 
                //GameObject.Find("LoadTransition").GetComponent<LoadTransition>().LoadNextScene("LoseScene"); 
            }
        }
        else if (collision.tag == "portal" && ScoreCounter.turnedGood == EnemyKill)
        {
            GameObject.Find("PlayerAudio").GetComponent<PlayerAudio>().PlayClear(); 
            GameObject.Find("LoadTransition").GetComponent<LoadTransition>().LoadBoss();
        }
        else if (collision.tag == "o_trigger" || collision.tag == "p_trigger" || collision.tag == "y_trigger")
        {
            triggered = true;
            collisionObj = collision;
        }
        else if (collision.tag == "Boss")
        {
            GameObject.Find("PlayerAudio").GetComponent<PlayerAudio>().Hurt();
            TakeDamage(touchDamage);


            if (currentHealth <= 0)
            {
                //Destroy(gameObject);
                StartCoroutine(DeathCountdown());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        triggered = false;
    }
    private void OnParticleCollision(GameObject spray)
    {

        TakeDamage(sprayDmg);

        if (currentHealth <= 0)
        {
            //Destroy(gameObject);
            StartCoroutine(DeathCountdown());
        }
    }
}
