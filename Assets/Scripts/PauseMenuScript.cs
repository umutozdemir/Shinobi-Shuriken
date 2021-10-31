using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuScript : MonoBehaviour
{
    public static bool gameIsPaused = false;

    private GameObject shinobi;

    public GameObject pauseMenuUI;


    private void Start()
    {
        shinobi = GameObject.FindGameObjectWithTag("Player");
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }


    public void Resume()
    {
        shinobi.GetComponent<PlayerMovement>().enabled = true;
        shinobi.GetComponent<PlayerCombat>().enabled = true;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        shinobi.GetComponent<PlayerMovement>().enabled = false;
        shinobi.GetComponent<PlayerCombat>().enabled = false;
        Time.timeScale = 0f;
        gameIsPaused = true;
    }


    public void QuitGame()
    {
        Application.Quit();
    }
}