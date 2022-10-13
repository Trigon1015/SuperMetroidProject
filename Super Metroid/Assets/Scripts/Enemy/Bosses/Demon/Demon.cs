using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demon : MonoBehaviour
{
    [SerializeField] float m_speed = 4.0f;
    

    private Animator m_animator;
    private Rigidbody2D rb;
    public Transform player;
    public bool isFlipped = false;

    public Vector3 attackOffset;
    public float attackRange = 1f;
    public LayerMask attackMask;
    public int attackDamage = 15;
    public Transform attackPoint;


    
    // Use this for initialization
    void Start()
    {
        m_animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        rb.useFullKinematicContacts = true;
        player = GameObject.FindGameObjectWithTag("Player").transform;

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        if (transform.position.x > player.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, -180f, 0f);
            isFlipped = false;
        }
        else if (transform.position.x < player.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, -180f, 0f);
            isFlipped = true;
        }
    }

    public void Attack()
    {
        
        Vector3 pos = attackPoint.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;

        Collider2D colInfo = Physics2D.OverlapCircle(pos, attackRange, attackMask);
        
        if (colInfo != null)
        {
            AudioManager.instance.PlaySFX(18);
            colInfo.GetComponentInParent<PlayerHealthController>().DamagePlayer(attackDamage);

        }
        else
        {
            AudioManager.instance.PlaySFX(19);
        }
    }
    public void EndBattle()
    {
       
        m_animator.SetTrigger("Death");
        GetComponent<Collider2D>().enabled = false;
        StartCoroutine(Death());
        AudioManager.instance.PlayFinishMusic();


    }

    IEnumerator Death()
   {
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
        yield return new WaitForSeconds(1f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(attackPoint.position, attackRange);
    }
}
