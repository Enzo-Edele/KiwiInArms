using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehaviour : MonoBehaviour
{
    [SerializeField] private int health = 800;

    private bool invincible = false;
    private Animator animator;
    private Rigidbody2D rb;
    private bool isDead = false;

    public bool isFlipped = false;
    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform tspawnPoint;

    [SerializeField] private ParticleSystem ScreamBurst;


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

        if(health <= 0 && !isDead)
        {
            Death();
        }

        
    }

    private void FixedUpdate()
    {
        
    }

    public void Destroy()
    {
        Destroy(this.gameObject);
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

    public void LaunchProjectile(int multiple)
    {
        GameObject proj = Instantiate(projectile, tspawnPoint.position,Quaternion.identity);
        proj.GetComponent<ProjectileScript>().SetDirection(new Vector2 (Vector2.MoveTowards(rb.position, player.transform.position, 1f).x * -1,0));

        if (multiple != 0)
        {
            GameObject proj2 = Instantiate(projectile, tspawnPoint.position, Quaternion.identity);
            proj2.GetComponent<ProjectileScript>().SetDirection(new Vector2(Vector2.MoveTowards(rb.position, player.transform.position, 1f).x * -1, 0.65f));

            GameObject proj3 = Instantiate(projectile, tspawnPoint.position, Quaternion.identity);
            proj3.GetComponent<ProjectileScript>().SetDirection(new Vector2(Vector2.MoveTowards(rb.position, player.transform.position, 1f).x * -1, -0.65f));
        }
    }
    
    public void Death()
    {
        isDead = true;
        animator.SetBool("Death", true);
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

    public void ActivateBurst()
    {
        ScreamBurst.Play();
    }
}
