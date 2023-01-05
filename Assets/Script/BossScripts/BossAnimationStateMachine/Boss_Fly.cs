using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Fly : StateMachineBehaviour
{
    public float speed = 2.0f;
    private GameObject[] paths;
    private Vector3 pathToGo;
    private Rigidbody2D rb;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        pathToGo = Vector3.zero;
        paths = GameObject.FindGameObjectsWithTag("Path");

        if(rb == null)
            rb = animator.GetComponent<Rigidbody2D>();

        foreach (var go in paths)
        {
            //faire en sorte de prendre le point le plus loin pour y aller
            if(pathToGo == Vector3.zero || Vector3.Distance(rb.position, pathToGo) < Vector3.Distance( rb.position, go.transform.position))
            {
                pathToGo = go.transform.position;
            }
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector2 target = pathToGo;

        Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.deltaTime);
        rb.MovePosition(newPos);

        if(Vector3.Distance(rb.position,pathToGo) <= 0.5f)
        {
            animator.SetTrigger("DoneFlying");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("DoneFlying");
    }
}
