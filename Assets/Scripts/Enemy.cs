using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Animator animator;
    public LayerMask playerLayer;
    public Transform attackPoint;
    public Rigidbody2D rb;

    private PlayerCombat playerCombat;

    //  HEALTH STATS    
    public float maxHealth = 100;
    public float currentHealth;
    public float healthPowerUp = 0f;

    //   COMBAT STATS

    // KATANA ATTACK
    public float attackRange = 0.5f;
    // more rate means more attack.
    public float attackRate = 2f;
    // to reduce spam left and right click
    private float nextAttackTime = 0f;
    // power ups will increase these values.
    public float attackSpeed = 1f;
    public float attackMultiplier = 1f;
    public float attackDamage = 30f;

    //  SHURIKEN ATTACK
    public float shurikenRange = 10f;
    public float shurikenRate = 2f;
    // to reduce spam left and right click
    private float nextShurikenTime = 0f;
    // power ups will increase these values.
    public float shurikenSpeed = 1f;
    public float shurikenMultiplier = 1f;
    public float shurikenDamage = 20f;

    //Knockback
    public float KBamount = 320f;

    private void Awake()
    {
        GetComponent<AIDestinationSetter>().target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        playerCombat = FindObjectOfType<PlayerCombat>();
        currentHealth = maxHealth + healthPowerUp;
        attackDamage *= attackMultiplier;
        shurikenDamage *= shurikenMultiplier;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            Attack();
            nextAttackTime = Time.time + 1f / attackRate * attackSpeed;
        }

        if (Time.time >= nextShurikenTime)
        {
            ThrowShuriken();
            nextShurikenTime = Time.time + 1f / shurikenRate * shurikenSpeed;
        }
    }


    void Attack()
    {
        Collider2D hitPlayer = Physics2D.OverlapCircle(attackPoint.position, attackRange, playerLayer);
        if (hitPlayer == null) return;
        animator.SetTrigger("Attack");
        hitPlayer.GetComponent<PlayerCombat>().TakeDamage(attackDamage);
    }
    
    // DEGİSECEK           zombi kod mu ?
    void ThrowShuriken()
    {
        Collider2D hitPlayer = Physics2D.OverlapCircle(attackPoint.position, shurikenRange, playerLayer);
        if (hitPlayer == null) return;
        //animator.SetTrigger("Throw");
        //hitPlayer.GetComponent<PlayerCombat>().TakeDamage(shurikenDamage);
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
        
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
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