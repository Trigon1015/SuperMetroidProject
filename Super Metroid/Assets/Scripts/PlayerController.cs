using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpeed;
    public float jumpForce;
    public Transform groundPoint;
    public Transform standPoint;
    public Transform topRight;
    public Transform bottomLeft;
    
    private bool isGrounded;
    private bool cantStand;
   

    public Animator anim;

    public LayerMask whatIsGround;


    public BulletController shotToFire;
    public Transform shotPoint;

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

    // Update is called once per frame
    void Update()
    {
        if (dashRechargeCounter > 0)
        {
            dashRechargeCounter -= Time.deltaTime;
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
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
            if(afterImageCounter <= 0)
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
            }
            else if (rb.velocity.x > 0)
            {
                transform.localScale = Vector3.one;
            }
        }


        isGrounded = Physics2D.OverlapCircle(groundPoint.position, 0.2f, whatIsGround);
        cantStand = Physics2D.OverlapArea(topRight.position, bottomLeft.position, whatIsGround);

        if (Input.GetButtonDown("Jump") && (isGrounded || (canDoubleJump && standing.activeSelf)) )
        {
            if(isGrounded)
            {
                canDoubleJump = true;
            }
            else
            {
                canDoubleJump = false;

                anim.SetTrigger("doubleJump");
            }

            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            
        }

       if(Input.GetButtonDown("Fire1"))
        {
            if(standing.activeSelf)
            {
                Instantiate(shotToFire, shotPoint.position, shotPoint.rotation).moveDir = new Vector2(transform.localScale.x, 0f);

                anim.SetTrigger("shotFired");
            }
            else if(ball.activeSelf)
            {
                Instantiate(bomb, bombPoint.position, bombPoint.rotation);
            }
        }

       //ball mode
       if(!ball.activeSelf)
        {
            if(Input.GetAxisRaw("Vertical") < -0.9f)
            {
                ballCounter -= Time.deltaTime;
                if(ballCounter <= 0)
                {
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
            if ((Input.GetAxisRaw("Vertical") > 0.9f)&& !cantStand)
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
