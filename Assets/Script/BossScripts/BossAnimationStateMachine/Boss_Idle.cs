using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Idle : StateMachineBehaviour
{
    private Transform playerTransform;

    private Rigidbody2D rb;
    public float timer = 3.0f;
    private float handlingTimer;

    public float timerIdle = 2f;
    private float timerUpHandle = 0.0f;
    public float speedIdling = 5f;

    public float meleeAttackRange = 3.0f;
    private Vector2 randomUp;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       /* if (playerTransform == null)
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;*/
        if (rb == null)
            rb = animator.GetComponent<Rigidbody2D>();

        handlingTimer = timer;
        if(timerUpHandle == 0.0f)
            timerUpHandle = timerIdle;
        randomUp = RandomUpDirection(1);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<BossBehaviour>().LookAtPlayer();

        handlingTimer -= Time.deltaTime;
        if(handlingTimer <= 0)
        {
            handlingTimer = timer;
            int randomAttackIndex = Random.Range(0, 4);
            TriggerAttack(randomAttackIndex, animator);
            //RandomAttack();
        }
        timerUpHandle -= Time.deltaTime;
        speedIdling = timerUpHandle <= timerUpHandle / 2 ? -speedIdling : speedIdling;

        if (timerUpHandle <= 0)
        {
            timerUpHandle = timerIdle;
            randomUp = RandomUpDirection(1);
        }
        else if(timerUpHandle <= timerUpHandle / 2)
        {
            randomUp = RandomUpDirection(-1);
        }


        Vector2 targetPos = Vector2.MoveTowards(rb.position, rb.position +randomUp, speedIdling * Time.fixedDeltaTime);
        rb.MovePosition(targetPos);

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        handlingTimer = timer;
        ResetTrigger(animator);
    }

    private void RandomAttack()
    {
        //check si en range d'attaque pendant l'animation d'Idle
        /*if (Vector2.Distance(playerTransform.position, rb.position) <= meleeAttackRange)
        {
            //melee attack random
            int randomAttackIndex = Random.Range(0, 1);
            TriggerAttack(randomAttackIndex, animator);
        }
        else
        {
            //ranged attack random
            int randomAttackIndex = Random.Range(2, 3);
            TriggerAttack(randomAttackIndex, animator);
        }*/
    }

    private void TriggerAttack(int randAttackIndex,Animator animator)
    {
        switch (randAttackIndex)
        {
            case 0:
                //faire attaque 1 (un Cri)
                animator.SetTrigger("Fly");
                return;
            case 1:
                //faire attaque 2 (Coup de griffe)
                animator.SetTrigger("Attack");
                return;
            case 2:
                //faire attaque 3 (Lancer de plume)
                animator.SetTrigger("RangeAttack");
                return;
            case 3:
                animator.SetTrigger("Scream");
                //faire attaque 4 (un Cri)
                return;
        }
    }

    private void ResetTrigger(Animator animator)
    {
        for (int i = 0; i < animator.parameterCount; i++)
        {

            animator.ResetTrigger(i);
        }
        
        
    }

    private Vector2 RandomUpDirection(int mult)
    {
        Vector2 randomUp;

        randomUp = new Vector2(Random.Range(-0.1f, 0.6f) * mult, 1);


        return randomUp;
    }
}
