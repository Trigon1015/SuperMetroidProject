using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateController : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator anim;
    public static bool frozen = false;
    public  float freezeTime;
    private float freezeCounter;
    public SpriteRenderer sprite;

    private bool init = true;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
        if(init)
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

        //Debug.Log("freeze");
        rb.velocity = new Vector2(0f, 0f);
        anim.enabled = false;
        frozen = true;
        rb.isKinematic = true;
        sprite.color = Color.blue;
        gameObject.layer = LayerMask.NameToLayer("Ground");

    }

    public void Restore()
    {
        //Debug.Log("move");
        anim.enabled = true;
        frozen = false;
        rb.isKinematic = false;
        sprite.color = Color.white;
        freezeCounter = freezeTime;
        gameObject.layer = LayerMask.NameToLayer("Enemy");
    }

    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "FreezeBeam")
        {
            init = true;
             
        }
    }





}
