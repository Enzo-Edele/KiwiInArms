using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCheck : MonoBehaviour
{
    [SerializeField] PlayerController player;
    Rigidbody2D rb2d;

    public Vector2 pos;
    [SerializeField] float damage;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        pos = transform.localPosition;
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
        if (collision.tag == "Enemy") collision.GetComponent<Dummy>().ChangeHealth(damage);
    }
}
