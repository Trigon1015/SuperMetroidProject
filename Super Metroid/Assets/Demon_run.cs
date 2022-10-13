using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demon_run : StateMachineBehaviour
{
    private GameObject player;
    Rigidbody2D rb;
    public float speed = 2.5f;
    public float attackRange = 1f;
     
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player");
        
        rb = animator.GetComponent<Rigidbody2D>();
        
}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<Demon>().LookAtPlayer();
        Vector2 target = new Vector2(player.transform.position.x, rb.position.y);
        Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
        rb.MovePosition(newPos);
       
       if (Vector2.Distance(player.transform.position, rb.position) <= attackRange)
       {
           animator.SetTrigger("Attack");
       }

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack");
    }
}
