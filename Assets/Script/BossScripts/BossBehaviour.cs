using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehaviour : MonoBehaviour
{
    private float health;
    [SerializeField] private float maxHealth = 900;

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
    [SerializeField] private ParticleSystem FeatherBurst;

    [SerializeField] private BoxCollider2D hitBox;
    [SerializeField] private BoxCollider2D hurtBox;

    public GameObject telegraph;

    private GameObject player = null;
    private Vector3 targetToAim;
    private CameraShake cam;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        health = maxHealth;
    }
    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraShake>();
    }

    // Update is called once per frame
    void Update()
    {

        

        if(health <= 390 && !phase2)
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


    void UpdateUIHeatlh(float Health)
    {
        Debug.Log(Health);
        UIManager.Instance.UpdateBossHealth(maxHealth, Health);
    }

    public void TakeDamage(float damage)
    {
        if(!invincible)
        {
            this.health -= damage;
            animator.SetTrigger("Hurt");
        }
    }

    public void EndGame()
    {
        UIManager.Instance.EndFight(true);
        Destroy(this.gameObject);

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
        proj.GetComponent<ProjectileScript>().SetDirection(targetToAim - rb.transform.position);

        proj.GetComponent<ProjectileScript>().SetPlayer(player);
        proj.GetComponent<ProjectileScript>().LookAtPlayer();
        if (multiple != 0)
        {
            GameObject proj2 = Instantiate(projectile, tspawnPoint.position, Quaternion.identity);
            proj2.GetComponent<ProjectileScript>().SetDirection(targetToAim - rb.transform.position + new Vector3 (0,0.9f));
            proj2.GetComponent<ProjectileScript>().SetPlayer(player);
            proj2.GetComponent<ProjectileScript>().LookAtPlayer();

            GameObject proj3 = Instantiate(projectile, tspawnPoint.position, Quaternion.identity);
            proj3.GetComponent<ProjectileScript>().SetDirection(targetToAim - rb.transform.position + new Vector3(0, -0.9f));
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

    public void ActivateBurst(int selected)
    {
        switch (selected)
        {
            case 0:
                ScreamBurst.Play();
                return;
            case 1:
                EnragedScreamBurst.Play();
                return;
            case 2:
                FeatherBurst.Play();
                return;
        }

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
                player.PushBack(this);
            }
        }
        
    }

    public void PlaySoundEffect(string soundCue)
    {
        SoundManager.Instance.Play(soundCue);
    }

    public void PlayMusic (string music)
    {
        SoundManager.Instance.PlayMusic(music);
    }

    public void StopSoundEffect(string soundCue)
    {
        SoundManager.Instance.StopSound(soundCue);
    }

    public void StopMusic(string music)
    {
        SoundManager.Instance.StopMusic(music);
    }

    public void ModifyVolume(float volume)
    {
        
        SoundManager.Instance.ModifyMusicVolume("Glory",volume);
    }
    public void CamShake()
    {
        cam.Shake();
    }
}
