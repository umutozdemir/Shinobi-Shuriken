using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelColliderScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collider = collision.gameObject;
        if (collider.tag == "Player")
        {
            collider.gameObject.GetComponent<PlayerCombat>().currentLevel +=1;

            SceneManager.LoadScene(1);
        }
    }
}