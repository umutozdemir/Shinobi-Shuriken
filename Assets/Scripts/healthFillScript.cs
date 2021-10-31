using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthFillScript : MonoBehaviour
{
    private Image HealthFill;
    PlayerCombat Player;
    // Start is called before the first frame update
    void Start()
    {
        HealthFill = GetComponent<Image>();
        Player = FindObjectOfType<PlayerCombat>();
        

    }

    // Update is called once per frame
    void Update()
    {
        HealthFill.fillAmount = Player.healthPercentage;
    }

    private void OnLevelWasLoaded(int level)
    {
        StartCoroutine(checkForPlayer());
    }

    IEnumerator checkForPlayer()
    {
        yield return new WaitForSeconds(1f);
        HealthFill = GetComponent<Image>();
        Player = FindObjectOfType<PlayerCombat>();
    }
}
