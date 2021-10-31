using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RoomSpawner : MonoBehaviour
{
    public int openingDirection;
    // 1 --> need bottom door
    // 2 --> need top door
    // 3 --> need left door
    // 4 --> need right door


    private RoomTemplates templates;
    private bool spawned = false;

    public float waitTime = 4f;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, waitTime);
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        Invoke("Spawn", 0.1f);
    }

    // Update is called once per frame
    void Spawn()
    {
        if (!spawned)
        {
            if (openingDirection == 1)
            {
                // Need to spawn a room with a BOTTOM door.
                Instantiate(templates.bottomRooms[Random.Range(0, templates.bottomRooms.Length)], transform.position,
                    Quaternion.identity);
            }
            else if (openingDirection == 2)
            {
                // Need to spawn a room with a TOP door.
                Instantiate(templates.topRooms[Random.Range(0, templates.topRooms.Length)], transform.position,
                    Quaternion.identity);
            }
            else if (openingDirection == 3)
            {
                // Need to spawn a room with a LEFT door.
                Instantiate(templates.leftRooms[Random.Range(0, templates.leftRooms.Length)], transform.position,
                    Quaternion.identity);
            }
            else if (openingDirection == 4)
            {
                // Need to spawn a room with a RIGHT door.
                Instantiate(templates.rightRooms[Random.Range(0, templates.rightRooms.Length)], transform.position,
                    Quaternion.identity);
            }

            spawned = true;
            AstarPath.active.Scan();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("SpawnPoint"))
        {
            if (other.GetComponent<RoomSpawner>().spawned == false && spawned == false)
            {
                // spawn walls blocking off any openings !
                //Instantiate(templates.closedRoom, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }

            spawned = true;
        }
    }
}