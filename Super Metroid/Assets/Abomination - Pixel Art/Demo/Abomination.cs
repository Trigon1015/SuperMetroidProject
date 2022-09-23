using UnityEngine;
using System.Collections;

public class Abomination : MonoBehaviour {

    [SerializeField] float      m_speed = 4.0f;
    [SerializeField] bool       m_noBlood = false;

    private Animator            m_animator;
    private Rigidbody2D         rb;
    public Transform player;
    public bool isFlipped =false;

    public Vector3 attackOffset;
    public float attackRange = 10f;
    public LayerMask attackMask;
    public int attackDamage = 15;
    public Transform attackPoint;

    
    public float health;
    // Use this for initialization
    void Start ()
    {
        m_animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update ()
    {







       
        //Hurt
        if (Input.GetKeyDown("q"))
            m_animator.SetTrigger("Hurt");


        //Walk
        else if (rb.velocity.magnitude != 0)
            m_animator.SetInteger("AnimState", 1);

        //Idle
        else
            m_animator.SetInteger("AnimState", 0);
    }

    public void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        if(transform.position.x > player.position.x && isFlipped)
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
        AudioManager.instance.PlaySFXAdjusted(16);
        Vector3 pos = attackPoint.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;

        Collider2D colInfo = Physics2D.OverlapCircle(pos, attackRange, attackMask);
        
        if (colInfo != null)
        {
            
            colInfo.GetComponentInParent<PlayerHealthController>().DamagePlayer(attackDamage);
        }
    }
    public void EndBattle()
    {
        m_animator.SetBool("noBlood", m_noBlood);
        m_animator.SetTrigger("Death");
        GetComponent<Collider2D>().enabled = false;
        StartCoroutine(Death());
        
        
    }

    IEnumerator Death()
    {
        yield return new WaitForSeconds(3f);
        AbomBossBattle.battleEnded = true;
        
    }
}
