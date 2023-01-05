using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehaviour : MonoBehaviour
{
    [SerializeField] private int health = 500;

    private bool invincible = false;
    private Animator animator;
    private Rigidbody2D rb;
    private bool isDead = false;

    public bool isFlipped = false;
    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform tspawnPoint;


    private GameObject player = null;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 350)
        {
            animator.SetBool("Phase2",true);
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            TakeDamage(20);
        }
        UpdateUIHeatlh(health);
    }

    private void FixedUpdate()
    {
        
    }

    void UpdateUIHeatlh(int Health)
    {
        Debug.Log(Health);
    }

    public void TakeDamage(int damage)
    {
        if(!invincible)
            this.health -= damage;
    }

    public void BossAttack(int damage)
    {

    }

    public void SetIsInvincible(bool isInvincible)
    {
        this.invincible = isInvincible;
    }

    public bool GetIsInvincible()
    {
        return this.invincible;
    }

    public void LaunchProjectile()
    {
        GameObject proj = Instantiate(projectile, tspawnPoint);
        proj.GetComponent<ProjectileScript>().SetDirection(new Vector2 (-Vector2.MoveTowards(this.rb.position, player.transform.position, 1f).x,0));
    }

    public void Death()
    {

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
}
