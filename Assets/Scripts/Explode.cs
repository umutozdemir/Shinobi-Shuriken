using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{
    public Animator anim;
    public float explosionRange = 0.3f;
    public LayerMask enemyLayers;
    public LayerMask BarrelLayers;
    public Collider2D collid;

    public bool exploded = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Shuriken")
        {
            ExplodeB();
        }
    }

    void ExplodeB()
    {
        if (!exploded)
        {
            anim.SetTrigger("Explode");
            SoundManager.soundManager.PlayExplosionSound();
            Destroy(gameObject, 0.60f);
            exploded = true;
            collid.enabled = false;
            AstarPath.active.Scan();


            Collider2D[] hitBarrels = Physics2D.OverlapCircleAll(transform.position, explosionRange, BarrelLayers);
            Debug.Log(hitBarrels);
            foreach (Collider2D barrel in hitBarrels)
            {
                
                if(this.name != barrel.name)
                {
                    if (!barrel.GetComponent<Explode>().exploded)
                    {
                        StartCoroutine(ExplodeOtherBarrels(barrel));
                    }
                    
                }
                
            }
            
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, explosionRange, enemyLayers);
            foreach (Collider2D enemy in hitEnemies)
            {
                enemy.GetComponent<Enemy>().TakeDamage(60f);
                enemy.GetComponent<Enemy>().KnockBack(4f);
            }
            
        }
        
    }

    IEnumerator ExplodeOtherBarrels(Collider2D x)
    {
        yield return new WaitForSeconds(0.3f);
        x.GetComponent<Explode>().ExplodeB();
    }


    private void OnDrawGizmosSelected()
    {

        Gizmos.DrawWireSphere(transform.position, explosionRange);
    }
}
