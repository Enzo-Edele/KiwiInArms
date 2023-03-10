using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    public float speed = 4.0f;
    private float timer = 5.0f;
    private float timerScale;
    private Vector3 direction;

    private GameObject player;
    private bool isFlipped;

    void Start()
    {
        timerScale = timer;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 newPos = Vector2.MoveTowards(this.transform.position, this.transform.position + direction, speed * Time.deltaTime);

        this.transform.position = newPos;


        timerScale -= Time.deltaTime;
        if(timerScale <= 0)
        {
            timerScale = timer;
            Destroy(this.gameObject);
        }

    }

    public void SetDirection(Vector2 dir)
    {
        this.direction = dir.normalized;
    }

    public void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        if (transform.position.x > player.transform.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }
        else if (transform.position.x < player.transform.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }
    }

    public void SetPlayer(GameObject player)
    {
        this.player = player;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            player.ChangeHealth(-20);
        }
    }
}
