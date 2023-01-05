using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [SerializeField] PlayerController player;
    Rigidbody2D rb2d;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = new Vector3(0, 0, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        player.SetGrounded(true);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        player.SetGrounded(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        player.SetGrounded(false);
    }
}
