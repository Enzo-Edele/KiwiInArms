using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCheck : MonoBehaviour
{
    [SerializeField] PlayerController player;
    Rigidbody2D rb2d;
    [HideInInspector]public Animator animator;

    public Vector2 pos;
    [SerializeField] float damage;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        pos = transform.localPosition;
        player = GetComponentInParent<PlayerController>();
    }

    void Update()
    {
        transform.localPosition = pos;
    }

    public void Flip(bool orientation)
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 15)
        {
            BossBehaviour boss = collision.gameObject.GetComponentInParent<BossBehaviour>();
            boss.TakeDamage(-damage);
        }
    }
}
