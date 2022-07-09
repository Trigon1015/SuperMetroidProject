using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SentinelController : MonoBehaviour
{
    private float speed;
    private float dirX;
    private bool facingRight = false;
    private Vector3 localScale;
    public Rigidbody2D rb;

    public Animator anim;
    public static bool frozen = false;
    public float freezeTime;
    private float freezeCounter;
    public SpriteRenderer sprite;
    private bool init = true;
    private void Start()
    {
        localScale = transform.localScale;
        dirX = -1f;
        speed = 3f;
        rb.useFullKinematicContacts = true;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Wall")
        {
            dirX *= -1;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "FreezeBeam")
        {
            init = true;

        }
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(dirX * speed, rb.velocity.y);
    }

    private void LateUpdate()
    {
        CheckWhereToFace();
    }

    private void Update()
    {
       
        if (init)
        {
            if (freezeCounter > 0)
            {
                FreezeEnemy();
                freezeCounter -= Time.deltaTime;
            }
            else
            {
                Restore();
                init = false;
            }
        }
    }

    public void FreezeEnemy()
    {


        speed = 0f;
        anim.enabled = false;
        frozen = true;
        //rb.isKinematic = true;
        sprite.color = Color.blue;
        gameObject.layer = LayerMask.NameToLayer("Ground");

    }

    public void Restore()
    {
        speed = 3f;
        anim.enabled = true;
        frozen = false;
       // rb.isKinematic = false;
        sprite.color = Color.white;
        freezeCounter = freezeTime;
        gameObject.layer = LayerMask.NameToLayer("Default");
    }

    void CheckWhereToFace()
    {
        if(dirX > 0)
        {
            facingRight = true;
        }
        else if(dirX <0)
        {
            facingRight = false;
        }
        if(((facingRight)&&(localScale.x <0)) || ((!facingRight)&&(localScale.x >0)))
        {
            localScale.x *= -1;
        }

        transform.localScale = localScale;
    }
}
