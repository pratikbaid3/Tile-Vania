using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField]
    private int runSpeed = 2;

    [SerializeField]
    private int jumpSpeed = 2;

    [SerializeField]
    private int climbSpeed = 2;

    private bool canJump=false;
    private bool canClimb = false;
    private bool death = false;
    private Animator _anim;
    private Rigidbody2D myRigidbody;


    // Start is called before the first frame update
    void Start()
    {
        _anim = gameObject.GetComponent<Animator>();
        myRigidbody = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Calling the player movement function
        //PlayerMovement();
        if(death==false)
        {
            Run();
            Animations();
            Jump();
            Climb();
        }
    }
    
    void Run()
    {
        if(death==false)
        {
            float controlThrow = Input.GetAxis("Horizontal");

            //Enabling touch controlls
            /**if(Input.touchCount>0)
            {
                controlThrow = Input.GetTouch(1f);
            }**/
            Vector2 playerVelocity = new Vector2(controlThrow * runSpeed, myRigidbody.velocity.y);
            myRigidbody.velocity = playerVelocity;
        }
    }

    void Jump()
    {
        if(Input.GetButtonDown("Jump") && canJump==true)
        {
            Vector2 jumpVelocityToAdd = new Vector2(myRigidbody.velocity.x, jumpSpeed);
            myRigidbody.velocity = jumpVelocityToAdd;
        }
    }

    void Climb()
    {
        if ((Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) && canClimb == true)
        {
            Vector2 climbVelocity = new Vector2(myRigidbody.velocity.x, climbSpeed);
            myRigidbody.velocity = climbVelocity;
        }
    }

    void Death(float enemyPosition)
    {
        death = true;
        _anim.SetBool("isRunning", false);
        _anim.SetBool("isClimbing", false);
        _anim.SetBool("isDead", true);

        if(transform.position.x<enemyPosition)
        {
            myRigidbody.transform.Translate(Vector3.left * Time.deltaTime * 300);
        }
        else if(transform.position.x>enemyPosition)
        {

            myRigidbody.transform.Translate(Vector3.right * Time.deltaTime * 300);
        }
        else
        {
            myRigidbody.transform.Translate(Vector3.up * Time.deltaTime * 300);
        }
        Vector2 playerVelocity = new Vector2(0,0);
        myRigidbody.velocity = playerVelocity;

    }

    //Checks if the player stays in collision with the ground
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            canJump = true;
            Debug.Log("Entered collision with Ground");
        }
    }
    //Checking if the player has exited collision with the gorund
    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.collider.tag=="Ground")
        {
            canJump = false;
            //Debug.Log("Exited collision with ground");
        }
    }


    //Checking if the player has entered collision with the ladder
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="Ladder")
        {
            Debug.Log("Entered collision with ladder");
            canClimb = true;
        }
        if (collision.tag == "Enemy")
        {
            Debug.Log("Death");
            Death(collision.transform.position.x);
        }
    }
    //Checking if the player has exited collision with the ladder
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag=="Ladder")
        {
            canClimb = false;
            Debug.Log("Exited collision with Ladder");
        }
    }

    //Function controlling the animation of the player based on the player input
    void Animations()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            _anim.SetBool("isRunning", true);
            _anim.SetBool("isClimbing", false);
            transform.localScale = new Vector3(1, 1, 1);
        }
        if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))&& canClimb==true)
        {
            _anim.SetBool("isRunning", false);
            _anim.SetBool("isClimbing", true);
        }
        if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.D))
        {
            _anim.SetBool("isRunning", false);
            //_anim.SetBool("isClimbing", false);
        }
        if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.W))
        {
            //_anim.SetBool("isRunning", false);
            _anim.SetBool("isClimbing", false);
        }
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.localScale = new Vector3(-1, 1, 1);
            _anim.SetBool("isRunning", true);
            _anim.SetBool("isClimbing", false);
        }

        //Checking that no key is pressed
        if(!Input.anyKey)
        {
            _anim.SetBool("isRunning", false);
            _anim.SetBool("isClimbing", false);
        }
    }
}
