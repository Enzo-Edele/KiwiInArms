using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehaviour : MonoBehaviour
{
    private float health;
    [SerializeField] private float maxHealth = 800;

    private bool invincible = false;
    private Animator animator;
    private Rigidbody2D rb;
    private bool isDead = false;
    private bool phase2 = false;

    public bool isFlipped = false;
    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform tspawnPoint;

    [SerializeField] private ParticleSystem ScreamBurst;
    [SerializeField] private ParticleSystem EnragedScreamBurst;

    [SerializeField] private BoxCollider2D hitBox;
    [SerializeField] private BoxCollider2D hurtBox;


    private GameObject player = null;
    private Vector2 targetToAim;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        health = maxHealth;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        

        if(health <= 350 && !phase2)
        {
            animator.SetTrigger("Phase2");
            phase2 = true;
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

    void UpdateUIHeatlh(float Health)
    {
        Debug.Log(Health);
        UIManager.Instance.UpdateHealth(maxHealth, Health);
    }

    public void TakeDamage(float damage)
    {
        if(!invincible)
        {
            this.health -= damage;
            animator.SetTrigger("Hurt");
        }
    }


    public void SetIsInvincible(bool isInvincible)
    {
        this.invincible = isInvincible;
    }

    public bool GetIsInvincible()
    {
        return this.invincible;
    }

    public void AimProjectile()
    {
        targetToAim = player.transform.position;
    }
    public void LaunchProjectile(int multiple)
    {
        GameObject proj = Instantiate(projectile, tspawnPoint.position,Quaternion.identity);
        proj.GetComponent<ProjectileScript>().SetDirection(new Vector2(Vector2.MoveTowards(rb.position, targetToAim, 0.02f).x * -1, Vector2.MoveTowards(rb.position, targetToAim, 0.02f).y));

        proj.GetComponent<ProjectileScript>().SetPlayer(player);
        proj.GetComponent<ProjectileScript>().LookAtPlayer();
        if (multiple != 0)
        {
            GameObject proj2 = Instantiate(projectile, tspawnPoint.position, Quaternion.identity);
            proj2.GetComponent<ProjectileScript>().SetDirection(new Vector2(Vector2.MoveTowards(rb.position, targetToAim, 1f).x * -1,
                                                                            Vector2.MoveTowards(rb.position, targetToAim, 1f).y + 0.85f));
            proj2.GetComponent<ProjectileScript>().SetPlayer(player);
            proj2.GetComponent<ProjectileScript>().LookAtPlayer();

            GameObject proj3 = Instantiate(projectile, tspawnPoint.position, Quaternion.identity);
            proj3.GetComponent<ProjectileScript>().SetDirection(new Vector2(Vector2.MoveTowards(rb.position, targetToAim, 1f).x * -1,
                                                                            Vector2.MoveTowards(rb.position, targetToAim, 1f).y - 0.85f));
            proj3.GetComponent<ProjectileScript>().SetPlayer(player);
            proj3.GetComponent<ProjectileScript>().LookAtPlayer();
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

    public void ActivateBurst(int enraged)
    {
        if(enraged == 0)
            ScreamBurst.Play();
        else
            EnragedScreamBurst.Play();

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hitBox.IsTouching(other))
        {
            if (other.gameObject.layer == 10)
            {
                Debug.Log("OUIOUI");
                PlayerController player = other.gameObject.GetComponent<PlayerController>();

                player.ChangeHealth(-10);
            }
        }
        
    }
}
