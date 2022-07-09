using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public Transform player;
    public Transform ballT;
    public float moveSpeed;
    public float jumpForce;
    public Transform groundPoint;
    public Transform groundPointB;
    public Transform standPoint;
    public Transform topRight;
    public Transform bottomLeft;
    
    
    
    
    private bool isGrounded;
    private bool isGroundedB;
    private bool cantStand;
   

    public Animator anim;

    public LayerMask whatIsGround;


    public BulletController BaseBullet;
    public BulletController freezeBeam;
    public Transform shotPoint;
    public Transform shotUpPoint;
    public Transform duckPoint;
    private bool duck = false;

    //jump
    private float jumpTimeCounter;
    public float jumpTime;
    private bool isJumping;


    //double Jump
    private bool canDoubleJump;

    //dash
    public float dashSpeed, dashTime;
    private float dashCounter;
    public SpriteRenderer sr, afterImage, ballS;
    public float afterImageLifeTime, timeBetweenAfterImages;
    private float afterImageCounter;
    public Color AfterImageColor;
    public float DashCD;
    private float dashRechargeCounter;
    public Transform Ballp;

    //ball
    public GameObject standing, ball;
    public float waitToBall;
    private float ballCounter;
    public Animator ballAnim;

    public Transform bombPoint;
    public GameObject bomb;

    //gravity reverse
    public bool reversed = false;


    private PlayerAbilityTracker abilities;
    public bool canMove;

    void Start()
    {
        abilities = GetComponent<PlayerAbilityTracker>();
        canMove = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {


            if (dashRechargeCounter > 0)
            {
                dashRechargeCounter -= Time.deltaTime;
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.LeftShift) && PlayerAbilityTracker.canDash)
                {
                    dashCounter = dashTime;
                    ShowAfterImage();
                }
            }
            if (dashCounter > 0)
            {
                dashCounter = dashCounter - Time.deltaTime;
                rb.velocity = new Vector2(dashSpeed * transform.localScale.x, 0);
                afterImageCounter -= Time.deltaTime;
                if (afterImageCounter <= 0)
                {
                    ShowAfterImage();
                }
                dashRechargeCounter = DashCD;
            }
            else
            {

                rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * moveSpeed, rb.velocity.y);

                //flip 
                if (rb.velocity.x < 0)
                {
                    transform.localScale = new Vector3(-1f, 1f, 1f);
                    shotUpPoint.eulerAngles = new Vector3(180f, 0f, -90f);
                }
                else if (rb.velocity.x > 0)
                {
                    transform.localScale = Vector3.one;
                    shotUpPoint.eulerAngles = new Vector3(180f, 0f, 270f);
                }

                //gravity reverse


                if (Input.GetKeyDown(KeyCode.Q) && PlayerAbilityTracker.canGravityReverse && ball.activeSelf && (isGrounded || isGroundedB))
                {


                    if (!reversed)
                    {
                        rb.gravityScale = -5;


                        ballT.Rotate(0.0f, -180.0f, -180.0f);

                        reversed = !reversed;

                    }
                    else
                    {
                        rb.gravityScale = 5;

                        ballT.Rotate(0.0f, -180.0f, -180.0f);


                        reversed = !reversed;
                    }

                }

            }

            

            isGroundedB = Physics2D.OverlapCircle(groundPointB.position, 0.2f, whatIsGround);
            isGrounded = Physics2D.OverlapCircle(groundPoint.position, 0.2f, whatIsGround);
            cantStand = Physics2D.OverlapArea(topRight.position, bottomLeft.position, whatIsGround);

            if (Input.GetButtonDown("Jump") && (isGrounded || ((canDoubleJump && PlayerAbilityTracker.canDoubleJump) && standing.activeSelf)))
            {

                if (isGrounded)
                {
                    duck = false;
                    anim.SetBool("duck", duck);
                    canDoubleJump = true;


                }
                else
                {
                    canDoubleJump = false;

                    anim.SetTrigger("doubleJump");

                }


                isJumping = true;
                jumpTimeCounter = jumpTime;

                rb.velocity = new Vector2(rb.velocity.x, jumpForce);


            }


            
            if (Input.GetKey(KeyCode.Space) && isJumping)
            {


                if (jumpTimeCounter > 0)
                {
                    jumpTimeCounter -= Time.deltaTime;
                    rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                    if (Input.GetKeyUp(KeyCode.Space))
                    {
                        Debug.Log("!");
                    }
                }
                else
                {
                    isJumping = false;
                }
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                isJumping = false;

            }



            //duck
            if (rb.velocity.x > 0.1 || rb.velocity.x < -0.1)
            {
                duck = false;
                anim.SetBool("duck", duck);

            }
            if (Input.GetAxisRaw("Vertical") < -0.9f && standing.activeSelf && isGrounded && (rb.velocity.x < 0.1 && rb.velocity.x > -0.1))
            {
                duck = true;
                anim.SetBool("duck", duck);
            }
            if (duck)
            {
                if (Input.GetKey(KeyCode.W))
                {
                    duck = false;
                    anim.SetBool("duck", duck);
                }

                if (Input.GetButtonDown("Fire1"))
                {

                    if (standing.activeSelf && !PlayerAbilityTracker.canFreeze)
                    {

                        Instantiate(BaseBullet, duckPoint.position, duckPoint.rotation).moveDir = new Vector2(transform.localScale.x, 0f);

                    }
                    else if (standing.activeSelf && PlayerAbilityTracker.canFreeze)
                    {
                        Instantiate(freezeBeam, duckPoint.position, duckPoint.rotation).moveDir = new Vector2(transform.localScale.x, 0f);

                    }

                }
            }

            //shooting up
            if (Input.GetKey(KeyCode.W) && standing.activeSelf && isGrounded && (rb.velocity.x < 0.1 && rb.velocity.x > -0.1) && !duck)
            {
                anim.SetBool("shotUp", true);

                if (Input.GetButtonDown("Fire1"))
                {

                    if (standing.activeSelf && !PlayerAbilityTracker.canFreeze)
                    {

                        Instantiate(BaseBullet, shotUpPoint.position, shotUpPoint.rotation).moveDir = new Vector2(0f, transform.localScale.y);

                    }
                    else if (standing.activeSelf && PlayerAbilityTracker.canFreeze)
                    {
                        Instantiate(freezeBeam, shotUpPoint.position, shotUpPoint.rotation).moveDir = new Vector2(0f, transform.localScale.y);


                    }

                }

            }
            else if (!duck)
            {
                anim.SetBool("shotUp", false);

                if (Input.GetButtonDown("Fire1"))
                {
                    if (standing.activeSelf && !PlayerAbilityTracker.canFreeze)
                    {


                        Instantiate(BaseBullet, shotPoint.position, shotPoint.rotation).moveDir = new Vector2(transform.localScale.x, 0f);
                        anim.SetTrigger("shotFired");




                    }
                    else if (standing.activeSelf && PlayerAbilityTracker.canFreeze)
                    {
                        Instantiate(freezeBeam, shotPoint.position, shotPoint.rotation).moveDir = new Vector2(transform.localScale.x, 0f);

                        anim.SetTrigger("shotFired");

                    }
                    else if (ball.activeSelf && PlayerAbilityTracker.canDropBomb)
                    {

                        Instantiate(bomb, bombPoint.position, bombPoint.rotation);
                    }
                }
            }
            //ball mode
            if (!ball.activeSelf)
            {
                if (Input.GetAxisRaw("Vertical") < -0.9f && PlayerAbilityTracker.canMorphball)
                {
                    ballCounter -= Time.deltaTime;
                    if (ballCounter <= 0)
                    {
                        duck = false;
                        anim.SetBool("duck", duck);
                        ball.SetActive(true);
                        standing.SetActive(false);

                    }
                }
                else
                {
                    ballCounter = waitToBall;
                }
            }
            else
            {
                if ((Input.GetAxisRaw("Vertical") > 0.9f) && !cantStand)
                {
                    ballCounter -= Time.deltaTime;
                    if (ballCounter <= 0)
                    {
                        ball.SetActive(false);
                        standing.SetActive(true);

                    }
                }
                else
                {
                    ballCounter = waitToBall;
                }
            }

        }
        else
        {
            rb.velocity = Vector2.zero;
        }
        if(standing.activeSelf)
        {
            anim.SetBool("isGrounded", isGrounded);
            anim.SetFloat("speed", Mathf.Abs(rb.velocity.x));
        }
        
        if(ball.activeSelf)
        {
            ballAnim.SetFloat("speed", Mathf.Abs(rb.velocity.x));
        }
        
    }

    public void ShowAfterImage()
    {
        //SpriteRenderer image = Instantiate(afterImage, transform.position, transform.rotation);
        //image.sprite = sr.sprite;
        if(ball.activeSelf)
        {
            SpriteRenderer image = Instantiate(afterImage, Ballp.position, Ballp.rotation);
            image.sprite = ballS.sprite;
            image.transform.localScale = transform.localScale;
            image.color = AfterImageColor;

            Destroy(image.gameObject, afterImageLifeTime);

            afterImageCounter = timeBetweenAfterImages;
        }
        else if(standing.activeSelf)
        {
            SpriteRenderer image = Instantiate(afterImage, transform.position, transform.rotation);
            image.sprite = sr.sprite;
            image.transform.localScale = transform.localScale;
            image.color = AfterImageColor;

            Destroy(image.gameObject, afterImageLifeTime);

            afterImageCounter = timeBetweenAfterImages;
        }
        
    }

   
    

    
    
}
