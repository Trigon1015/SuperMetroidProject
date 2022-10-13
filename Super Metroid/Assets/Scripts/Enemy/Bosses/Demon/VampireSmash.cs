using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VampireSmash : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody2D rb;
    public float speed;
    public GameObject portal;
    
    
    void Start()
    {
        rb.useFullKinematicContacts = true;

    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(0, -speed);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag== "Wall")
        {
            speed = 0;
            Instantiate(portal, transform.position, Quaternion.identity);
            StartCoroutine(Vanish());
        }
    }

     IEnumerator Vanish()
    {
        yield return new WaitForSeconds(1.5f);
        gameObject.SetActive(false);
        Destroy(gameObject);
    }
    

    
}
