using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Fly : StateMachineBehaviour
{
    public float speed = 2.0f;
    private Transform playerTransform;

    private Rigidbody2D rb;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        /*if(playerTransform == null)
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;*/
        if(rb == null)
            rb = animator.GetComponent<Rigidbody2D>();


    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        /*Vector2 target = new Vector2(playerTransform.position.x, playerTransform.position.y);

        Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.deltaTime);
        rb.MovePosition(newPos);*/
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
}
