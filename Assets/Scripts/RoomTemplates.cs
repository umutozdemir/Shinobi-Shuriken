using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RoomTemplates : MonoBehaviour
{
    public GameObject[] bottomRooms;
    public GameObject[] topRooms;
    public GameObject[] leftRooms;
    public GameObject[] rightRooms;

    //public GameObject closedRoom;

    public List<GameObject> rooms;

    public GameObject levelExit;

    public float waitTime;
    private bool spawnedBoss;
    public GameObject boss;

    void Update()
    {
        if (waitTime <= 0 && spawnedBoss == false)
        {
            for (int i = 0; i < rooms.Count; i++)
            {
                if (i == rooms.Count - 1)
                {
                    Debug.Log(rooms[i].transform.position);
                    Vector3 levelExitPosition = rooms[i].transform.position;
                    levelExitPosition.x += -1.8f;
                    levelExitPosition.y += 1.8f;
                    Debug.Log("level exit : " + levelExitPosition);
                    Instantiate(levelExit, levelExitPosition, Quaternion.identity);
                    spawnedBoss = true;
                }
            }
        }
        else
        {
            waitTime -= Time.deltaTime;
        }
    }
}