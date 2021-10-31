using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NailsScript : MonoBehaviour
{
    float currentTime=0;
    float tickTime = 0.5f;
    float tickDamage = 7f;
    float tickRange = 0.3f;
    public LayerMask enemyLayers;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if(currentTime > tickTime)
        {
            currentTime = 0;
            tickDamageFunc();
        }
    }

    void tickDamageFunc()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, tickRange, enemyLayers);

        // Damage them
        foreach (Collider2D enemy in hitEnemies)
        {
            if(enemy.name[0] == 'K')
            {
                //SoundManager.soundManager.PlayKatanaHitSound(); play pricking sound
                enemy.GetComponent<Enemy>().TakeDamage(tickDamage);
                enemy.GetComponent<Enemy>().KnockBack(0.8f);
            }
            else
            {
                enemy.GetComponent<Archer>().TakeDamage(tickDamage);
                enemy.GetComponent<Archer>().KnockBack(0.8f);
            }
            
        }
    }
}
