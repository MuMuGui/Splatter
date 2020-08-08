using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBoss : MonoBehaviour
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
    private float h;
    [SerializeField]
    private float speed = 5.0f;

    [Header("Collision")]
    public bool onGround = false;
    public float castLength;
    public LayerMask groundLayer;

    [Header("Health")]
    public int maxHealth;
    public int currentHealth;
    [SerializeField]
    int touchDamage;
    [SerializeField]
    int sprayDmg;

    public HealthBar healthBar;
    [SerializeField] int paintDamage = 1;

    [Header("Audio")]
    [SerializeField]
    AudioClip walkSFX;
    [SerializeField]
    AudioClip jumpSFX;
    private AudioSource audio;

    private bool triggered;
    Collider2D collisionObj;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        currentState = PlayerState.Idle;
        audio = GetComponent<AudioSource>();
        triggered = false;
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
        }

        if (h != 0)
        {
            if (!audio.isPlaying && onGround)
            {
                ClipWalk();
                audio.Play();
            }
        }
        else if (audio.clip == walkSFX)
        {
            audio.Stop();
        }

        if (triggered && Input.GetKeyDown(KeyCode.Z))
        {
            if (collisionObj.tag == "o_trigger")
            {
                GameObject oBucket = GameObject.FindWithTag("o_bucket");
                oBucket.GetComponent<BucketSpill>().Spill();

            }
            else if (collisionObj.tag == "p_trigger")
            {
                GameObject pBucket = GameObject.FindWithTag("p_bucket");
                pBucket.GetComponent<BucketSpill>().Spill();
            }
            else if (collisionObj.tag == "y_trigger")
            {
                GameObject yBucket = GameObject.FindWithTag("y_bucket");
                yBucket.GetComponent<BucketSpill>().Spill();
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
        audio.clip = jumpSFX;
        audio.loop = false;
        audio.Play();
        rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        anim.SetBool("isJump", true);


    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        triggered = true;
        if (collision.tag == "Boss")
        {

            TakeDamage(touchDamage);
            

            if (currentHealth <= 0)
            {
                Destroy(gameObject);
            }
        }

        else if (collision.tag == "o_trigger" || collision.tag == "p_trigger" || collision.tag == "y_trigger")
        {
            collisionObj = collision;
        }
        else if (collision.tag == "paint")
        {
            
            TakeDamage(paintDamage);
            //Destroy(collision.gameObject);
            if (currentHealth <= 0)
            {
                //Destroy(gameObject);
                //GameObject.Find("LoadTransition").GetComponent<LoadTransition>().LoadNextScene("LoseScene");
            }
        }
    }
    private void OnParticleCollision(GameObject spray)
    {
       
        TakeDamage(sprayDmg);
        
        if (currentHealth <= 0)
        {
            //Destroy(gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        triggered = false;
    }

    void ClipWalk()
    {
        audio.clip = walkSFX;
        audio.loop = false;
    }

}
