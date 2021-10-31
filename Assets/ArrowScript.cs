using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    float arrowDamage = 18;

    void Start()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            
            collision.collider.GetComponent<PlayerCombat>().TakeDamage(arrowDamage);

            Destroy(gameObject);
        }

    }
}
