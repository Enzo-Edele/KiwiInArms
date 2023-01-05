using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float jumpForce;
    bool isJumping = false;
    [SerializeField] bool isGrounded = true;

    [SerializeField] int maxLife;
    int life;

    [SerializeField] GameObject arms;
    [SerializeField] GameObject beak;


    Animator animator;
    BoxCollider2D boxCollider;
    Rigidbody2D rb2d;
    SpriteRenderer spriteRenderer;

    float timerArms;

    float timerBeaks;

    void Start()
    {
        life = maxLife;

        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            ArmAttack();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            BeakAttack();
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            isJumping = true;
        }
    }

    private void FixedUpdate()
    {
        Vector2 velocity = transform.position;
        velocity.x += Input.GetAxis("Horizontal") * speed* Time.fixedDeltaTime;

        transform.position = velocity;
        if (isJumping)
        {
            rb2d.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
            isJumping = false;
        }
    }

    public void SetGrounded(bool newState)
    {
        isGrounded = newState;
        if (isGrounded)
            spriteRenderer.color = Color.green;
        if (!isGrounded)
            spriteRenderer.color = Color.red;
    }

    void ArmAttack()
    {
        arms.SetActive(true);
    }
    void BeakAttack()
    {
        beak.SetActive(true);
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