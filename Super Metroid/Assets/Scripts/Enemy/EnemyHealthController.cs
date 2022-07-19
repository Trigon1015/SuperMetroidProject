using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthController : MonoBehaviour
{
    public int totalHealth = 10;
    public GameObject deathEffect;
    public SpriteRenderer sprite;
    
    
    
    public void DamageEnemy(int damageAmount)
    {
        totalHealth -= damageAmount;
        StartCoroutine(FlashRed());
        


        if (totalHealth <= 0)
        {
            if(deathEffect != null)
            {
                Instantiate(deathEffect, transform.position, transform.rotation);
                
            }
            EnemyPatroller patroller = gameObject.GetComponent<EnemyPatroller>();
            if(patroller != null)
            {
                patroller.DestroyPatrolPoints();
            }
            AudioManager.instance.PlaySFX(4);
            Destroy(gameObject);
        }
    }

    

    public IEnumerator FlashRed()
    {
        sprite.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        sprite.color = Color.white;
    }
   
}
