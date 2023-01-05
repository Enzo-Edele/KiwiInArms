using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed;

    [SerializeField] int maxLife;
    int life;

    Animator animator;
    BoxCollider2D boxCollider;
    Rigidbody2D rb2d;
    SpriteRenderer spriteRenderer;

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
        
    }

    private void FixedUpdate()
    {
        Vector3 position = rb2d.position;

        position += new Vector3(
            Input.GetAxis("Horizontal") * speed * Time.deltaTime,
            0,
            0);

        rb2d.MovePosition(position);
    }
}
