using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    public Transform playerTransform;
    GameObject[] cameras;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void OnLevelWasLoaded(int level)
    {
        transform.position = new Vector3(0, 0, -4);
        cameras = GameObject.FindGameObjectsWithTag("MainCamera");
        if (cameras.Length > 1)
        {
            //playerEqualizer(players[0], players[1]);
            Destroy(cameras[1]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 camvec = new Vector3(playerTransform.position.x, playerTransform.position.y, transform.position.z);
        transform.position = camvec;
    }
}
