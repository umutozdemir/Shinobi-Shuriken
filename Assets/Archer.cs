using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class Archer : MonoBehaviour
{
    public Animator animator;
    public LayerMask playerLayer;
    public Transform attackPoint;
    public Rigidbody2D rb;
    public GameObject player;

    public Transform shootPoint;
    public GameObject arrowPrefab;

    private PlayerCombat playerCombat;

    //  HEALTH STATS    
    public float maxHealth = 100;
    public float currentHealth;
    public float healthPowerUp = 0f;

    
    //  SHURIKEN ATTACK
    public float shurikenRange = 10f;
    public float shurikenRate = 2f;
    // to reduce spam left and right click
    private float nextShurikenTime = 0f;
    // power ups will increase these values.
    public float shurikenSpeed = 3.5f;
    public float shurikenMultiplier = 1f;
    public float shurikenDamage = 20f;

    //Knockback
    public float KBamount = 320f;

    //look direction
    Vector2 playerPos;
    bool shooting= false;
    float shootingTime = 2.6f; //will shoot every 1.2f
    float currentShootingTime = 0f; //when this reaches 2.6 stop shooting and start walking again
    float shootNow = 1.2f;
    float currentShootNow = 0f;
    AIDestinationSetter AIDS;
    AIPath AIP;
    Seeker Skr;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerCombat = FindObjectOfType<PlayerCombat>();
        GetComponent<AIDestinationSetter>().target = player.transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth + healthPowerUp;
        shurikenDamage *= shurikenMultiplier;
        AIDS = GetComponent<AIDestinationSetter>();
        AIP = GetComponent<AIPath>();
        Skr = GetComponent<Seeker>();
    }

    // Update is called once per frame
    void Update()
    {
        float distanceBetweenShinobi = Vector2.Distance(transform.position, player.transform.position);
        if (!shooting) //if currently not shooting 
        {
            
            if (distanceBetweenShinobi < 2.4) //stop the scripts
            {
                AIDS.enabled = false;
                AIP.enabled = false;
                Skr.enabled = false;
                shooting = true;
                currentShootNow = 0f;
            }
            else
            { //reactivate the scripts
                AIDS.enabled = true;
                AIP.enabled = true;
                Skr.enabled = true;

            }
        }
        else
        {
            
            currentShootingTime += Time.deltaTime;
            currentShootNow += Time.deltaTime;
            if(currentShootingTime > shootingTime)
            {
                shooting = false;
                currentShootingTime = 0;
            }
            else
            {
                if(currentShootNow > shootNow)
                {
                    ShootArrow();
                    currentShootNow = 0f;
                }

            }
        }



    }

    void ShootArrow()
    {
        animator.SetTrigger("Shoot");
        SoundManager.soundManager.PlayShurikenSound();
        GameObject shuriken = Instantiate(arrowPrefab, shootPoint.position, shootPoint.rotation);
        Rigidbody2D rbShuriken = shuriken.GetComponent<Rigidbody2D>();
        rbShuriken.AddForce(shootPoint.up * shurikenSpeed, ForceMode2D.Impulse);
        Destroy(shuriken, 2.5f);
    }

    void FixedUpdate()
    {
        if (shooting)
        {
            playerPos = new Vector2(player.transform.position.x, player.transform.position.y);
            Vector2 lookDirection = playerPos - rb.position;
            float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;

            rb.rotation = angle;
        }
        
    }




    public void TakeDamage(float damage)
    {
        
        currentHealth -= damage;
        playerCombat.totalAttackDamaged += (int) damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        rb.velocity = Vector2.zero;
        rb.freezeRotation = true;
        animator.SetTrigger("isDead");
        playerCombat.totalEnemyKilled++;
        GetComponent<Rigidbody2D>().isKinematic = true;

        // Disable enemy
        GetComponent<AIPath>().enabled = false;
        GetComponent<AIDestinationSetter>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        this.enabled = false;
    }

    public void KnockBack(float KBforce)
    {
        rb.AddForce(transform.up.normalized * KBamount * KBforce * -1, ForceMode2D.Force);
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;

        
    }

    //knockback from shuriken
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.collider.tag == "Shuriken")
        {

            rb.AddForce(collision.collider.transform.up.normalized * KBamount, ForceMode2D.Force);
            //StartCoroutine(addForce(1,collision));

        }


    }
}