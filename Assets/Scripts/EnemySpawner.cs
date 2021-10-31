using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;
    public GameObject archerEnemy;
    private GameObject shinobi;

    public int enemyNumber = 1;
    private bool isSpawned = false;

    

    // Start is called before the first frame update
    void Start()
    {
        shinobi = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        float distanceBetweenShinobi = Vector2.Distance(transform.position, shinobi.transform.position);

        if (isSpawned == false)
        {
            if (distanceBetweenShinobi <= 8f)
            {
                int randomEnemyPicker = Random.Range(0, 4);
                
                if (randomEnemyPicker == 0 || randomEnemyPicker == 1 || randomEnemyPicker == 2)
                {
                    Instantiate(enemy, transform.position, Quaternion.identity);
                }
                else
                {
                    Instantiate(archerEnemy, transform.position, Quaternion.identity);
                }
                
                isSpawned = true;
            }
        }
    }
}