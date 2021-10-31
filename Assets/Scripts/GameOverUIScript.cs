using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverUIScript : MonoBehaviour
{

    private Transform gameOverPanel;

    private GameObject canvas;

    private PlayerCombat playerCombat;

    private LevelTextScript levelTextScript;

    public string level;
    
    public bool isGameOver = false;
    
    // Start is called before the first frame update
    void Start()
    {
        levelTextScript = FindObjectOfType<LevelTextScript>();
        FindGameOverPanels();
        playerCombat = transform.GetComponent<PlayerCombat>();
    }

    private void Update()
    {
        if (isGameOver)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }
    
    
    
    public void ActivateGameOverMenu()
    {
         
        isGameOver = true;
        gameOverPanel.gameObject.SetActive(true);

        Time.timeScale = 0;

        gameOverPanel.GetChild(2).gameObject.GetComponent<Text>().text =
            "ENEMY KILLED \n" + playerCombat.totalEnemyKilled.ToString();
        
        gameOverPanel.GetChild(3).gameObject.GetComponent<Text>().text =
            "TOTAL DAMAGE \n" + playerCombat.totalAttackDamaged.ToString();
        
        gameOverPanel.GetChild(5).gameObject.GetComponent<Text>().text =
            "LEVEL \n" + level;

    }

    public void FindGameOverPanels()
    {
        canvas = GameObject.Find("Canvas");
        gameOverPanel = canvas.transform.Find("GameOverPanel");
    }

    
   
}
