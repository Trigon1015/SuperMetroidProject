using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWallController : MonoBehaviour
{
    // Start is called before the first frame update
    public static int Health = 5;
    public int damage = 5;
    public SpriteRenderer sprite;
    public static FireWallController instance;

    //public float moveSpeed;
    //public Rigidbody2D rb;
   // public int damageAmount;
    
    public void Awake()
    {
        instance = this;
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //rb.velocity = -transform.right * moveSpeed;

        // transform.position = new Vector2()
        if (Health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerHealthController.instance.DamagePlayer(damage);
            
        }


    }

    public void DamageEnemy(int damageAmount)
    {
        Health -= damageAmount;
        StartCoroutine(FlashRed());
    }

    public IEnumerator FlashRed()
    {
        sprite.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        sprite.color = Color.white;
    }

    

    
  

    
}
