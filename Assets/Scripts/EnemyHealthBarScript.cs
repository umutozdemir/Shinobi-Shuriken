using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthBarScript : MonoBehaviour
{
    private Vector3 localScale;

    private Enemy enemy;

    private Archer archer;

    public bool isArcher = false;
    
    // Start is called before the first frame update
    void Start()
    {
        if (isArcher)
        {
            archer = transform.parent.GetComponent<Archer>();
        }
        else
        {
            enemy = transform.parent.GetComponent<Enemy>();
        }
        
        localScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (isArcher)
        {
            if (archer.enabled == true)
            {
                localScale.x = archer.currentHealth / 150f;
            }
            else
            {
                localScale.x = 0;
            }
            
        }
        else
        {
            if (enemy.enabled == true)
            {
                localScale.x = enemy.currentHealth / 150f;
            }
            else
            {
                localScale.x = 0;
            }
        }
        
        transform.localScale = localScale;
    }
}
