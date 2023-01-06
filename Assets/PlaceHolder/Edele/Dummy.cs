using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy : MonoBehaviour
{
    Rigidbody2D rb2d;
    SpriteRenderer spriteRenderer;

    float totalLife = 100.0f;
    [SerializeField]float life;
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        life = totalLife;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        spriteRenderer.color = Color.red;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        spriteRenderer.color = Color.white;
    }

    public void ChangeHealth(float val)
    {
        life += val;
        if (life < 0) print("Je me mourute.");
        UIManager.Instance.UpdateBossHealth(totalLife, life);
    }
}
