using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class shurikenFillScript : MonoBehaviour
{
    private Image shurikenFill;
    PlayerCombat Player;
    // Start is called before the first frame update
    void Start()
    {
        shurikenFill = GetComponent<Image>();
        Player = FindObjectOfType<PlayerCombat>();

    }

    private void OnLevelWasLoaded(int level)
    {
        StartCoroutine(checkForPlayer());
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.currentShuriken == 0)
        {
            shurikenFill.fillAmount = 0f;
        }else if(Player.currentShuriken == 1)
        {
            shurikenFill.fillAmount = 0.225f;
        }
        else if (Player.currentShuriken == 2)
        {
            shurikenFill.fillAmount = 0.415f;
        }
        else if (Player.currentShuriken == 3)
        {
            shurikenFill.fillAmount = 0.603f;
        }
        else if (Player.currentShuriken == 4)
        {
            shurikenFill.fillAmount = 0.79f;
        }
        else if (Player.currentShuriken == 5)
        {
            shurikenFill.fillAmount = 1f;
        }
    }

    IEnumerator checkForPlayer()
    {
        yield return new WaitForSeconds(1f);
        shurikenFill = GetComponent<Image>();
        Player = FindObjectOfType<PlayerCombat>();
    }
}
