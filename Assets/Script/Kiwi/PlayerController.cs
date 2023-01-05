using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float jumpForce;
    bool isJumping = false;
    [SerializeField] bool isGrounded = true;

    [SerializeField] float maxLife;
    float life;

    [SerializeField] AttackCheck arms;
    [SerializeField] AttackCheck beak;


    public Animator animator;
    BoxCollider2D boxCollider;
    Rigidbody2D rb2d;
    SpriteRenderer spriteRenderer;

    float timerArms;
    const float timeArms = 0.9f;
    const float cooldownArms = 0.7f;
    float timerBeaks;
    const float timeBeaks = 0.75f;
    const float cooldownBeaks = 0.5f;

    void Start()
    {
        life = maxLife;

        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        timerArms = -cooldownArms;
        timerBeaks = -cooldownBeaks;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) && timerArms <= -cooldownArms) {
            ArmAttack(true);
            timerArms = timeArms;
        }
        if (Input.GetKeyDown(KeyCode.E) && timerBeaks <= -cooldownBeaks) {
            BeakAttack(true);
            timerBeaks = timeBeaks;
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded) {
            isJumping = true;
            animator.SetTrigger("Jump");
        }

        if (timerArms < 0)
        {
            ArmAttack(false);
        }
        if (timerArms < -cooldownArms) {
            timerArms = -cooldownArms;
        }
        else if(timerArms != -cooldownArms)
        {
            timerArms -= Time.deltaTime;
        }
        if (timerBeaks < 0){
            BeakAttack(false);
        }
        if (timerBeaks < -cooldownBeaks){
            timerBeaks = -cooldownBeaks;
        }
        else if (timerBeaks != -cooldownBeaks){
            timerBeaks -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        Vector2 velocity = transform.position;
        velocity.x += Input.GetAxis("Horizontal") * speed* Time.fixedDeltaTime;
        velocity.x = Mathf.Clamp(velocity.x, -7.75f, 7.75f);

        animator.SetBool("Walking", true);

        if(Input.GetAxis("Horizontal") < 0) FlipChild(false); 
        else if (Input.GetAxis("Horizontal") > 0) FlipChild(true); 
        else animator.SetBool("Walking", false);

        transform.position = velocity;
        if (isJumping)
        {
            rb2d.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
            isJumping = false;
        }
    }

    void FlipChild(bool orientation)
    {
        if (orientation) {
            spriteRenderer.flipX = !orientation;
            beak.pos = new Vector2(Mathf.Abs(beak.pos.x), beak.pos.y);
            arms.pos = new Vector2(Mathf.Abs(arms.pos.x), arms.pos.y);
        }
        else {
            spriteRenderer.flipX = !orientation;
            beak.pos = new Vector2(-Mathf.Abs(beak.pos.x), beak.pos.y);
            arms.pos = new Vector2(-Mathf.Abs(arms.pos.x), arms.pos.y);
        }
    }

    public void SetGrounded(bool newState)
    {
        isGrounded = newState;
        //if (isGrounded)
            //spriteRenderer.color = Color.green;
        //if (!isGrounded)
            //spriteRenderer.color = Color.red;
    }

    void ArmAttack(bool state)
    {
        arms.gameObject.SetActive(state);
        if(state)
            animator.SetTrigger("Hit");
    }
    void BeakAttack(bool state)
    {
        beak.gameObject.SetActive(state);
        if (state)
            animator.SetTrigger("Beak");
    }

    public void ChangeHealth(float val)
    {
        life += val;
        if (life < 0) print("T nul.");
    }
}

/*float currentSpeed = rb2d.velocity.magnitude;
        float maxSpeed = 1;
        Vector2 defaultForce = new Vector2(Input.GetAxis("Horizontal") * speed, 0);
        Vector2 forceToAdd = new Vector2();

        if (Input.GetAxis("Horizontal") > 0)
        {
            if (currentSpeed > maxSpeed - (maxSpeed / 4))
            {
                float forceMultiplier = defaultForce.x * maxSpeed - (currentSpeed / maxSpeed);
                forceToAdd.x = forceMultiplier;
            }
            else
            {
                forceToAdd = defaultForce;
            }
        }
        if (Input.GetAxis("Horizontal") < 0)
        {
            if (currentSpeed < -maxSpeed - (-maxSpeed / 4))
            {
                float forceMultiplier = defaultForce.x * -maxSpeed - (currentSpeed / -maxSpeed);
                forceToAdd.x = forceMultiplier;
            }
            else
            {
                forceToAdd = defaultForce;
            }
        }

        //rb2d.MovePosition(rb2d.position + velocity * Time.fixedDeltaTime);
        //rb2d.AddForce(forceToAdd, ForceMode2D.Impulse);*/