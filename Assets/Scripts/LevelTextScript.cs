using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelTextScript : MonoBehaviour
{

    private PlayerCombat playerCombat;

    public string level;
    
    
    // Start is called before the first frame update
    void Start()
    {
        playerCombat = FindObjectOfType<PlayerCombat>();
        level = playerCombat.currentLevel.ToString();
        transform.GetComponent<Text>().text = "Level-" + level;
    }

    

    private void OnLevelWasLoaded(int level)
    {
        StartCoroutine(checkForPlayer());
    }

    IEnumerator checkForPlayer()
    {
        yield return new WaitForSeconds(1f);
        playerCombat = FindObjectOfType<PlayerCombat>();
        level = playerCombat.currentLevel.ToString();
        transform.GetComponent<Text>().text = "Level-" + level;

    }

}
