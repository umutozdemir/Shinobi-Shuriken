using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shuriken : MonoBehaviour
{
    float shurikenDamage;
    public GameObject shinobi;

    void Start()
    {
        shurikenDamage = shinobi.GetComponent<PlayerCombat>().shurikenDamage;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Melee Enemy")
        {
            if (collision.collider.name[0] == 'K')
            {
                collision.collider.GetComponent<Enemy>().TakeDamage(shurikenDamage);
            }
            else
            {
                collision.collider.GetComponent<Archer>().TakeDamage(shurikenDamage);

            }

            Destroy(gameObject);
        }
        
    }
}
