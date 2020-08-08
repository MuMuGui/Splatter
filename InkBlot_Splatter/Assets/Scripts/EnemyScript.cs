using System.Collections;
using System.Collections.Generic;
using System.Threading;

using UnityEngine;
using UnityEngine.SocialPlatforms;

public class EnemyScript : MonoBehaviour
{
    // Start is called before the first frame update
    public enum EnemyState
    {
        Walking,
        Shoot,
        Good
    }
    public float speed;
    //public float distance;

    private bool movingRight = true;

    public Transform groundDetection;
    Animator anim;

    EnemyState currentState;
    Rigidbody2D rb;
    public bool prox;
    public bool shoot;
    [SerializeField]  public  bool notBad;
    public  bool stop;
    //ground
    [Header("Collision")]
    public bool onGround = false;
    public float castLength;
    public LayerMask groundLayer;
    bool collide;
    //colisions with each other
    [SerializeField] float collideInterval;
    float ddtimer = 0f;
    bool touchPlayer = false;
    public int groundTimer = 0;
    


    [SerializeField] GameObject paint;
    bool splatonce;

    //JumpedOn score;



    void Start()
    {
        currentState = EnemyState.Walking;
        rb = GetComponent<Rigidbody2D>();
        prox = false;
        shoot = false;
        notBad = false;
        stop = false;
        transform.gameObject.tag = "Enemy";
        //transform.localScale = new Vector3(0.5f, 0.5f, 1f);
        collide = false;
        // score = GameObject.FindWithTag("score").GetComponent<JumpedOn>();
        anim = GetComponent<Animator>();
        splatonce = false;

        


    }
    // Update is called once per frame
    void FixedUpdate()
    {
        //Debug.Log(transform.localEulerAngles.y);
        if (!onGround)
        {
            groundTimer += 1;
        }
        else if (onGround)
        {
            groundTimer = 0;
        }
        if(groundTimer >2 && this.currentState == EnemyState.Walking)
        {
            Debug.Log("work");
            if (movingRight == true)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;
                transform.position = new Vector3(this.transform.position.x - 2f, this.transform.position.y, 0);
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
                transform.position = new Vector3(this.transform.position.x + 2f, this.transform.position.y, 0);
            }
            groundTimer = 0;
        }
        //
        if (this.prox == true)
        {
            this.currentState = EnemyState.Shoot;
        }
        else if (this.notBad == true)
        {
            this.currentState = EnemyState.Good;
        }
        
        else
        {
            this.currentState = EnemyState.Walking;
        }
        if (this.currentState == EnemyState.Walking)
        {

            transform.Translate(Vector2.right * speed * Time.deltaTime);
            anim.SetBool("IsAttack", false);



            //RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down);
            //Debug.DrawRay(groundDetection.position, Vector2.down);
            onGround = Physics2D.Raycast(transform.position, Vector2.down, castLength, groundLayer);
            Debug.DrawRay(transform.position, Vector2.down * castLength, Color.red);
            if (!onGround || collide == true)
            {
               
                //Debug.Log("Not on Ground");
                if (movingRight == true)
                {
                    transform.eulerAngles = new Vector3(0, -180, 0);
                    movingRight = false;
                    transform.position = new Vector3(this.transform.position.x-1f, this.transform.position.y, 0);
                }
                else
                {
                    transform.eulerAngles = new Vector3(0, 0, 0);
                    movingRight = true;
                    transform.position = new Vector3(this.transform.position.x + 1f, this.transform.position.y, 0);
                }
            }

            

        }
        else if (this.currentState == EnemyState.Shoot) 
        {
            //movement
           // Vector3 moveDirection = (GameObject.FindGameObjectWithTag("Player").transform.position - this.transform.position).normalized;

            //RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down); (groundInfo.collider == false
           // onGround = Physics2D.Raycast(transform.position, Vector2.down, castLength, groundLayer);
            if (!onGround)
            {
                
                //   transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
            }
            else
            {
               // EnemyMove(moveDirection);
            }
            
            shoot = true;
            rb.velocity = Vector3.zero;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            anim.SetBool("IsAttack", true);
            //rotation
            if (GameObject.FindWithTag("Player").transform.position.x > this.transform.position.x)
            {

                transform.eulerAngles = new Vector3(0, 0, 0);
            }
            else
            {
                //If the player is facing left
                transform.eulerAngles = new Vector3(0, -180, 0);
            }


        }
        else if (this.currentState == EnemyState.Good) 
        {
            //change sprite
            this.GetComponent<Collider2D>().isTrigger = true;
            this.GetComponent<Rigidbody2D>().gravityScale = 0f;
            rb.constraints = RigidbodyConstraints2D.FreezePositionX;
            rb.constraints = RigidbodyConstraints2D.FreezePositionY;
            this.GetComponent<Rigidbody2D>().simulated = false;
            transform.position = new Vector3(this.transform.position.x, this.transform.position.y, 0);
            //transform.position.y = this.transform.position.y - .2f;
            
            transform.gameObject.tag = "good";
            //add in variable to force stop everything else
            stop = true;
            //score.ScoreCount();
            anim.SetBool("IsGood",true);
            //splot background
            if(splatonce == false)
            {
                GameObject splotch = Instantiate(paint) as GameObject;
                splotch.transform.position = transform.position + Vector3.forward*2;
                splotch.transform.eulerAngles = new Vector3(0, 0, Random.Range(0,359));
            }
            splatonce = true;

            
        }
        
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            //put something here later probably
            
        }
        if(collision.tag == "Wall")
        {
            collide = true;
            Debug.Log("Wall");
        }
        
    }

            

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Wall" )
        {
            collide = false;
        
        
        }
        if (collision.tag == "Player")
        {
            touchPlayer = true;
        }


    }
    void EnemyMove(Vector3 direction)
    {
        rb.velocity = new Vector2(direction.x, this.transform.position.y);
    }
}
