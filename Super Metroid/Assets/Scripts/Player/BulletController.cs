using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float bulletSpeed;
    public Rigidbody2D rb;
    public Vector2 moveDir;
    public GameObject impactEffect;

    public int damageAmount = 1;
    // Update is called once per frame
    void Update()
    {
        rb.velocity = moveDir * bulletSpeed;
        if (rb.velocity.x < 0)
        {
            if(PlayerAbilityTracker.canFreeze)
            {
                transform.localScale = new Vector3(-2f, 2f, 2f);
            }
           else
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }
        }
        else if (rb.velocity.x > 0)
        {
            if (PlayerAbilityTracker.canFreeze)
            {
                transform.localScale = new Vector3(2f, 2f, 2f);
            }
            else
            {
                transform.localScale = new Vector3(1f, 1f, 1f);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Enemy")
        {
            other.GetComponent<EnemyHealthController>().DamageEnemy(damageAmount);
           
            
            
        }
        

        if(other.tag == "Boss")
        {
            BossHealthController.instance.TakeDamage(damageAmount);
        }

        if (other.tag == "Abomination")
        {
            AbomHealthController.instance.TakeDamage(damageAmount);
        }

        if (other.tag == "Demon")
        {
            DemonHealthContrroller.instance.TakeDamage(damageAmount);
        }

        if(other.tag == "Wind")
        {
            Destroy(gameObject);
        }

        if (impactEffect != null)
        {
            Instantiate(impactEffect, transform.position, Quaternion.identity);
        }

        AudioManager.instance.PlaySFXAdjusted(3);
        Destroy(gameObject);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
