using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlyingController : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator anim;
    //private static bool frozen = false;
    public float freezeTime;
    private float freezeCounter;
    public SpriteRenderer sprite;

    public bool init = true;


    

    public void FreezeEnemy()
    {


        moveSpeed = 0f;
        anim.enabled = false;
        //frozen = true;

        sprite.color = Color.blue;
        gameObject.layer = LayerMask.NameToLayer("Ground");

    }

    public void Restore()
    {

        anim.enabled = true;
        //frozen = false;
        moveSpeed = 5f;
        sprite.color = Color.white;
        freezeCounter = freezeTime;
        gameObject.layer = LayerMask.NameToLayer("Enemy");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "FreezeBeam")
        {
            init = true;

        }
    }


    public float rangeToStartChase;
    private bool isChasing;

    public float moveSpeed, turnSpeed;
    private Transform player;
    
    // Start is called before the first frame update
    void Start()
    {
        player = PlayerHealthController.instance.transform;

    }

    // Update is called once per frame
    void Update()
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

    private void FixedUpdate()
    {
        if (!isChasing )
        {
            if (Vector3.Distance(transform.position, player.position) < rangeToStartChase)
            {
                isChasing = true;
                anim.SetBool("isChasing", isChasing);
            }
        }
        else if (isChasing )
        {
            if (player.gameObject.activeSelf )
            {
                Vector3 direction = transform.position - player.position;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                Quaternion targetRot = Quaternion.AngleAxis(angle, Vector3.forward);

                transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, turnSpeed * Time.deltaTime);


                //transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
                transform.position += -transform.right * moveSpeed * Time.deltaTime;
            }
        }
    }
}
