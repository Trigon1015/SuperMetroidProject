using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWallBossController : MonoBehaviour
{
    // Start is called before the first frame update
    
    public int damage = 5;
    
    public static FireWallBossController instance;

    public float moveSpeed;
    public Rigidbody2D rb;
    public int damageAmount;

    public void Awake()
    {
        instance = this;
    }
    void Start()
    {
        rb.useFullKinematicContacts = true;
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = -transform.right * moveSpeed;

        
       
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerHealthController.instance.DamagePlayer(damage);
            Destroy(gameObject);
        }
        if (other.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }


    }

    

    
}
