using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCombat : MonoBehaviour
{
    
    public Animator animator;
    public LayerMask enemyLayers;
    public Transform attackPoint;
    
            //  HEALTH STATS    
    public float maxHealth = 100;
    public float currentHealth;
    public float healthPercentage; //this is for health bar
    public float healthPowerUp = 0f;

    //   COMBAT STATS
    public float KBforce = 1.0f;
    public int totalAttackDamaged = 0;
    public int totalEnemyKilled = 0;

    // KATANA ATTACK

    public float attackRange = 0.5f;
    // more rate means more attack.
    public float attackRate = 2f;
    // to reduce spam left and right click
    public float nextAttackTime = 0f;
    // power ups will increase these values.
    public float attackSpeed = 1f;
    public float attackPowerUp = 1f;
    public float attackDamage = 30f;
            
            //  SHURIKEN ATTACK
    public float shurikenRange = 10f;
    public float shurikenRate = 2f;
    // to reduce spam left and right click
    public float nextShurikenTime = 0f;
    // power ups will increase these values.
    public float shurikenSpeed = 4f;
    public float shurikenMultiplier = 1f;
    public float shurikenDamage = 20f;
    public int currentShuriken;
    public int maxShurikenAmount = 3; //shinobi starts with 3 shuriken can go up to 5
    public float shurikenReloadTime = 0f;
    public float reloadRate = 3f;

    // Shuriken initializers
    public Transform shootPoint;
    public GameObject shurikenPrefab;

    

    public int currentLevel = 1;//level 0 means you are dead coming into this level 

    //Regen stats
    float nextRegenTime = 0;
    float regenRate = 2f;
    
    
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth + healthPowerUp;
        attackDamage *= attackPowerUp;
        shurikenDamage *= shurikenMultiplier;
        currentShuriken = maxShurikenAmount;
    }

    // Update is called once per frame
    void Update()
    {
        healthPercentage = currentHealth / maxHealth;

        if (Time.time >= nextAttackTime)
        {
            if (Input.GetMouseButton(0))
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate * attackSpeed;
            }
        }

        if (Time.time >= nextShurikenTime)
        {
            if (Input.GetMouseButton(1) && currentShuriken>0)
            {
                ThrowShuriken();
                nextShurikenTime = Time.time + shurikenRate;
                if (currentShuriken == maxShurikenAmount && Time.time >= shurikenReloadTime) { currentShuriken--; }//fixes instant reloading of a shuriken
                currentShuriken--;
                
            }
        }

        if(Time.time >= shurikenReloadTime)
        {
            if(currentShuriken < maxShurikenAmount)
            {
                currentShuriken++;
                shurikenReloadTime = Time.time + reloadRate;
            }
        }

        if(Time.time >= nextRegenTime)
        {
            if(currentHealth < maxHealth)
            {
                currentHealth += 6;
            }
            
            nextRegenTime = Time.time + regenRate;

        }

        

    }
    
    
    void Attack()
    {
        // Play an attack animation.
        animator.SetTrigger("Attack");
        
        SoundManager.soundManager.PlayKatanaSound();
        
        // Detect enemies in range of attack.
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        Debug.Log(hitEnemies);
        
        // Damage them
        foreach (Collider2D enemy in hitEnemies)
        {
            SoundManager.soundManager.PlayKatanaHitSound();
            if (enemy.name[0] == 'K')
            {
                enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
                enemy.GetComponent<Enemy>().KnockBack(KBforce);
            }
            else
            {
                enemy.GetComponent<Archer>().TakeDamage(attackDamage);
                enemy.GetComponent<Archer>().KnockBack(KBforce);
            }
            
        }
    }
    
    
    void ThrowShuriken()
    {
        animator.SetTrigger("Throw");
        SoundManager.soundManager.PlayShurikenSound();
        GameObject shuriken = Instantiate(shurikenPrefab, shootPoint.position, shootPoint.rotation);
        Rigidbody2D rbShuriken = shuriken.GetComponent<Rigidbody2D>();
        rbShuriken.AddForce(shootPoint.up * shurikenSpeed, ForceMode2D.Impulse);
        Destroy(shuriken, 4f);
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    


    public void TakeDamage(float damage)
    {
        
        currentHealth -= damage + currentLevel;
        
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Shinobi died");
        animator.SetTrigger("isDead");
        
        
        SoundManager.soundManager.PlayGameOverSoundtrack();

        transform.GetComponent<GameOverUIScript>().level = currentLevel.ToString();
        
        StartCoroutine(ActivateGameOverUI());
        
        // Disable enemy
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<PlayerMovement>().moveSpeed = 0;
        currentLevel = 0;
        this.enabled = false;
       
        
       

    }


    IEnumerator ActivateGameOverUI()
    {
        yield return new WaitForSeconds(2.7f);
        transform.GetComponent<GameOverUIScript>().ActivateGameOverMenu();
    }
    
}
