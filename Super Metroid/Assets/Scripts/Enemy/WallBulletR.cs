using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBulletR : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D rb;
    public int damageAmount;
    public GameObject impactEffect;

    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = new Vector3(-1f, 1f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = transform.right * moveSpeed;

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        
        if (other.gameObject.tag == "Player")
        {
            PlayerHealthController.instance.DamagePlayer(damageAmount);
        }

        if (impactEffect != null)
        {
            Instantiate(impactEffect, transform.position, transform.rotation);
        }
        Destroy(gameObject);
    }
}

