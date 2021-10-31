using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBuffs : MonoBehaviour
{
    //List Of BUFFS player  currently has
    public List<int> buffList = new List<int>();
    public int barrelAmount = 5;
    public int nailAmount = 0;

    public Dictionary<int, string[]> selectableBuffs = new Dictionary<int, string[]>();


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
     19=Barrelbomb2  CANCELED,
     20=Nails1, 
     21=Nails2, 
     22=AttackRange1 DONE, 
     23=AttackRange2 DONE, 
     */


    PlayerCombat PC;
    PlayerMovement PM;

    //barrel variables
    public GameObject barrelSmall;
    float barrelCoolDown = 0.7f;
    float barrelCoolDownCurrent = 0f;
    int barrelCounter = 0; //this is to give every barrel a unique name

    //nails
    public GameObject nails;


    public bool firstTimeBuff = true;

    // Start is called before the first frame update
    void Start()
    {
        PC = GetComponent<PlayerCombat>();
        PM = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        movementBuffChecker();
        combatBuffChecker();
    }

    void movementBuffChecker()
    {
        if (buffList.Contains(4)) //MS1
        {
            PM.moveSpeed = 2.3f;
        }

        if (buffList.Contains(5)) //MS2
        {
            PM.moveSpeed = 3f;
        }

        if (buffList.Contains(6)) //MS3
        {
            PM.moveSpeed = 3.8f;
        }

        if (buffList.Contains(9)) //dashlength 1
        {
            PM.dashSpeed = 80;
        }

        if (buffList.Contains(10)) //dashlength 2
        {
            PM.dashSpeed = 110;
        }
    }

    void combatBuffChecker()
    {
        if (buffList.Contains(1)) //attackspeed1
        {
            PC.attackRate = 3.3f;
        }

        if (buffList.Contains(2)) //attackspeed2
        {
            PC.attackRate = 4f;
        }

        if (buffList.Contains(3)) //attackspeed3
        {
            PC.attackRate = 5f;
        }

        if (buffList.Contains(7)) //knockback1
        {
            PC.KBforce = 1.8f;
        }

        if (buffList.Contains(8)) //knockback2
        {
            PC.KBforce = 2.8f;
        }

        if (buffList.Contains(18)) //barrel
        {
            if (Input.GetKeyDown("space") && barrelCoolDownCurrent > barrelCoolDown)
            {
                PlaceBarrel();
                barrelCoolDownCurrent = 0;
            }

            barrelCoolDownCurrent += Time.deltaTime;
        }

        if (buffList.Contains(11)) //max shuriken
        {
            PC.maxShurikenAmount = 4;
        }

        if (buffList.Contains(12)) //max shuriken
        {
            PC.maxShurikenAmount = 5;
        }

        if (buffList.Contains(22)) //attack range
        {
            PC.attackRange = 0.38f;
        }

        if (buffList.Contains(23)) //attack range2
        {
            PC.attackRange = 0.46f;
        }

        if (buffList.Contains(15)) //Shuriken skill
        {
            PC.shurikenSpeed = 2.4f;
            PC.shurikenRate = 0.3f;
            PC.reloadRate = 0.92f;
        }

        if (buffList.Contains(16)) //Shuriken skill 2
        {
            PC.shurikenSpeed = 2.7f;
            PC.shurikenRate = 0.23f;
            PC.reloadRate = 0.8f;
        }

        if (buffList.Contains(20)) //nails
        {
            if (Input.GetKeyDown(KeyCode.N))
            {
                PlaceNails();
                //barrelCoolDownCurrent = 0;
            }
        }
    }

    void PlaceNails()
    {
        GameObject track =
            Instantiate(nails, transform.position + (transform.up.normalized * 0.5f), transform.rotation);
        Destroy(track, 16f);
    }

    void PlaceBarrel()
    {
        GameObject track = Instantiate(barrelSmall, transform.position + (transform.up.normalized * 0.5f),
            transform.rotation);
        track.name = track.name + barrelCounter.ToString();
        barrelCounter++;
        AstarPath.active.Scan(); //map out when new barrels are put in the game
    }
}