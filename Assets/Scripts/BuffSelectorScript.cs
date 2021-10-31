using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;


public class BuffSelectorScript : MonoBehaviour
{
    public GameObject buffPanel1;
    public GameObject buffPanel2;
    public GameObject buffPanel3;

    private Text buffName1;
    private Text buffName2;
    private Text buffName3;

    private Text buffDescription1;
    private Text buffDescription2;
    private Text buffDescription3;


    private List<int> showedBuffKeys = new List<int>();
    private int selectedBuffKey;

    private PlayerBuffs playerBuffs;

    private GameObject[] buffSelectionPanels;

    /* LIST OF BUFFS
     ID = BUFF
     1=AttackSpeed1 DONE,
     2=AttackSpeed2 DONE, 
     3=AttackSpeed3 DONE, 
     4=MovementSpeed1 DONE, 
     5=MovementSpeed2 DONE, 
     6=MovementSpeed3 DONE,
     7=KnockBack1 DONE, 
     8=KnockBack2 DONE, 
     9=DashLength1 DONE, 
     10=DashLength2 DONE, 
     11=ShurikenCount1 DONE, 
     12=ShurikenCount2 DONE, 
     13=ShurikenCount3
     14=ShurikenCount4, 
     15=ShurikenSkill1, 
     16=ShurikenSkill2, 
     17=ShurikenSkill3, 
     18=Barrelbomb1 DONE, 
     19=Barrelbomb2  ,
     20=Nails1, 
     21=Nails2, 
     22=AttackRange1 DONE, 
     23=AttackRange2 DONE, 
     */

    private void Start()
    {
        

        Time.timeScale = 0f;

        playerBuffs = FindObjectOfType<PlayerBuffs>();


        if (playerBuffs.firstTimeBuff)
        {
            playerBuffs.selectableBuffs.Add(1, new[] {"ATTACK SPEED LEVEL - 1", "A SWIFT STRIKE IS THE FINE LINE BETWEEN SURVIVAL AND DEATH, LEARN TO ATTACK FASTER"});
            playerBuffs.selectableBuffs.Add(4, new[] {"MOVEMENT SPEED LEVEL - 1", "A NINJA SHOULD LEARN TO MOVE LIKE THE WIND, INCREASE YOUR SPEED" });
            playerBuffs.selectableBuffs.Add(7, new[] {"KNOCK BACK BUFF LEVEL - 1", "EXTEND YOUR ENERGY TO YOUR ATTACKS, KNOCK YOUR ENEMIES FURTHER BACK" });
            playerBuffs.selectableBuffs.Add(9, new[] {"DASH LENGTH LEVEL - 1", "A TRUE NINJA KNOWS WHEN TO KEEP THEIR DISTANCE, DASH FURTHER LENGTHS" });
            playerBuffs.selectableBuffs.Add(11, new[] {"SHURIKEN COUNT LEVEL - 1", "INCREASE YOUR SHURIKEN LIMIT TO FOUR"});
            playerBuffs.selectableBuffs.Add(15, new[] {"SHURIKEN SKILL LEVEL - 1", "PRECISION IS KEY IN YOUR SURVIVAL, INCREASE YOUR ABILITY TO HANDLE SHURIKENS" });
            playerBuffs.selectableBuffs.Add(18, new[] {"BARREL BOMB LEVEL - 1", "AT TIMES, CHAOS IS BETTER THAN SILENCE, LEARN TO PLACE EXPLOSIVE BARRELS"});
            playerBuffs.selectableBuffs.Add(20, new[] {"CALTROPS LEVEL - 1", "A NINJA SHOULD KNOW HOW TO MANIPULATE THEIR ENVIRONMENT, LEARN TO PLACE CALTROPS "});
            playerBuffs.selectableBuffs.Add(22, new[] {"ATTACK RANGE LEVEL - 1", "DON'T FORGET, YOUR KATANA IS AN EXTENSION OF YOUR ARM, LEARN TO ATTACK FASTER" });
            playerBuffs.firstTimeBuff = false;
        }

        GetRandomBuffs();
    }

    private void OnLevelWasLoaded(int level)
    {
        buffSelectionPanels = GameObject.FindGameObjectsWithTag("BuffSelectionPanel");

        if (buffSelectionPanels.Length > 1)
        {
            //playerEqualizer(players[0], players[1]);
            Destroy(buffSelectionPanels[1]);
        }
    }


    public void GetRandomBuffs()
    {
        Time.timeScale = 0f;

        SetUpBuffPanels();

        Debug.Log("GET RANDOM BUFFS");

        List<int> keys = new List<int>();

        foreach (var key in playerBuffs.selectableBuffs.Keys)
        {
            keys.Add(key);
        }

        int index1 = keys.ElementAt(Random.Range(0, keys.Count));
        int index2 = keys.ElementAt(Random.Range(0, keys.Count));
        int index3 = keys.ElementAt(Random.Range(0, keys.Count));


        while (index1 == index2 || index1 == index3 || index2 == index3)
        {
            if (index1 == index2)
            {
                index2 = keys.ElementAt(Random.Range(0, keys.Count));
            }

            if (index1 == index3)
            {
                index3 = keys.ElementAt(Random.Range(0, keys.Count));
            }

            if (index2 == index3)
            {
                index3 = keys.ElementAt(Random.Range(0, keys.Count));
            }
        }


        showedBuffKeys.Add(index1);
        showedBuffKeys.Add(index2);
        showedBuffKeys.Add(index3);

        buffName1.text = playerBuffs.selectableBuffs[index1][0];
        buffName2.text = playerBuffs.selectableBuffs[index2][0];
        buffName3.text = playerBuffs.selectableBuffs[index3][0];

        buffDescription1.text = playerBuffs.selectableBuffs[index1][1];
        buffDescription2.text = playerBuffs.selectableBuffs[index2][1];
        buffDescription3.text = playerBuffs.selectableBuffs[index3][1];
    }


    public void SelectBuff(int key)
    {
        Debug.Log("BUFF SELECTED");
        SoundManager.soundManager.PlayClickSound();
        selectedBuffKey = showedBuffKeys[key];
        playerBuffs.buffList.Add(selectedBuffKey);
        playerBuffs.selectableBuffs.Remove(selectedBuffKey);
        Time.timeScale = 1f;
        BuffLevelUp(selectedBuffKey);
    }


    private void SetUpBuffPanels()
    {
        buffName1 = buffPanel1.transform.GetChild(0).gameObject.GetComponent<Text>();
        buffName2 = buffPanel2.transform.GetChild(0).gameObject.GetComponent<Text>();
        buffName3 = buffPanel3.transform.GetChild(0).gameObject.GetComponent<Text>();

        buffDescription1 = buffPanel1.transform.GetChild(1).gameObject.GetComponent<Text>();
        buffDescription2 = buffPanel2.transform.GetChild(1).gameObject.GetComponent<Text>();
        buffDescription3 = buffPanel3.transform.GetChild(1).gameObject.GetComponent<Text>();
    }


    private void BuffLevelUp(int buffKey)
    {
        switch (buffKey)
        {
            case 1:
                
                playerBuffs.selectableBuffs.Add(2, new[] {"ATTACK SPEED LEVEL - 2", "A SWIFT STRIKE IS THE FINE LINE BETWEEN SURVIVAL AND DEATH, LEARN TO ATTACK FASTER" });
                break;
            case 2:
                playerBuffs.selectableBuffs.Add(3, new[] {"ATTACK SPEED LEVEL - 3", "A SWIFT STRIKE IS THE FINE LINE BETWEEN SURVIVAL AND DEATH, LEARN TO ATTACK FASTER" });
                break;
            case 4:
                playerBuffs.selectableBuffs.Add(5, new[] {"MOVEMENT SPEED LEVEL - 2", "A NINJA SHOULD LEARN TO MOVE LIKE THE WIND, INCREASE YOUR SPEED" });
                break;
            case 5:
                playerBuffs.selectableBuffs.Add(6, new[] {"MOVEMENT SPEED LEVEL - 3", "A NINJA SHOULD LEARN TO MOVE LIKE THE WIND, INCREASE YOUR SPEED" });
                break;
            case 7:
                playerBuffs.selectableBuffs.Add(8, new[] {"KNOCK BACK BUFF LEVEL - 2", "EXTEND YOUR ENERGY TO YOUR ATTACKS, KNOCK YOUR ENEMIES FURTHER BACK" });
                break;
            case 9:
                playerBuffs.selectableBuffs.Add(10, new[] {"DASH LENGTH LEVEL - 2", "A TRUE NINJA KNOWS WHEN TO KEEP THEIR DISTANCE, DASH FURTHER LENGTHS" });
                break;
            case 11:
               playerBuffs.selectableBuffs.Add(12, new[] {"SHURIKEN COUNT LEVEL - 2", "INCREASE YOUR SHURIKEN LIMIT TO FIVE" });
                break;
            case 15:
                playerBuffs.selectableBuffs.Add(16, new[] {"SHURIKEN SKILL LEVEL - 2", "PRECISION IS KEY IN YOUR SURVIVAL, INCREASE YOUR ABILITY TO HANDLE SHURIKENS" });
                break;
            case 22:
                playerBuffs.selectableBuffs.Add(23, new[] {"ATTACK RANGE LEVEL - 2", "DON'T FORGET, YOUR KATANA IS AN EXTENSION OF YOUR ARM, LEARN TO ATTACK FASTER" });
                break;
        }

        foreach (var value in playerBuffs.selectableBuffs.Values)
        {
            Debug.Log(value[0]);
        }
    }
}