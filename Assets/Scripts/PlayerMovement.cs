using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    public Animator animator;
    private Rigidbody2D rb;
    GameObject[] players;
    
    public float moveSpeed = 1.3f;
    public Camera cam;
    
    private Vector2 movement;
    private Vector2 mousePos;
    
    private bool _isRun = false;

    //dashing variables
    public float dashSpeed;
    public float startDashTime;
    private float currentDashTime;
    private Vector2 dashDirection;
    private bool isDashing = false;
    private float dashCoolDownCurrent;
    public float dashCoolDown =4f;

    //BuffList
    PlayerCombat PC;
    PlayerBuffs PB;


    private GameObject teleport;
    

    // Start is called before the first frame update
    void Start()
    {
        PB = GetComponent<PlayerBuffs>();
        rb = GetComponent<Rigidbody2D>();
        PC = GetComponent<PlayerCombat>();
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        
        RunningAnimation();
        dash();


        if (Input.GetKeyDown(KeyCode.T))
        {
            teleport = GameObject.FindGameObjectWithTag("Teleport");
            Vector3 teleportPos = teleport.transform.position;
            teleportPos.x += -0.5f;
            teleportPos.y += -0.5f;
            transform.position = teleportPos;
        }

    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

        Vector2 lookDirection = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;

        rb.rotation = angle;
    }
    
    void RunningAnimation()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || 
            Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            _isRun = true;
            animator.SetBool("isRun", _isRun);
        }
        else
        {
            _isRun = false;
            animator.SetBool("isRun", _isRun);
        }
    }

    void dash() //experimental dash - could also  be  turned into a slide
    {
        dashCoolDownCurrent -= Time.deltaTime;

        if (Input.GetKey(KeyCode.LeftShift )  && isDashing != true &&  dashCoolDownCurrent <= 0)
        {
            dashCoolDownCurrent = dashCoolDown;
            rb.velocity = Vector2.zero;
            isDashing = true;
            currentDashTime = startDashTime;
        }

        if (isDashing)
        {
            dashDirection = movement;
            rb.AddForce( dashSpeed * dashDirection * Time.deltaTime * 175f) ;
            currentDashTime -= Time.deltaTime;
            if(currentDashTime <= 0)
            {
                isDashing = false;
            }
        }
    }

    private void OnLevelWasLoaded(int level)
    {
        transform.position = new Vector3(0, 0, 0);
        players = GameObject.FindGameObjectsWithTag("Player");
        AstarPath.active.Scan();
        if(players.Length > 1)
        {
            //playerEqualizer(players[0], players[1]);
            Destroy(players[1]);
        }
        if(PC.currentLevel == 0)
        {
            Respawner();
        }
        transform.GetComponent<GameOverUIScript>().FindGameOverPanels();
    }

    void Respawner()
    {
        animator.SetBool("isDead",false);
        //SoundManager.soundManager.PlayShinobiDeadSound();

        // Disable enemy
        GetComponent<BoxCollider2D>().enabled = true;
        GetComponent<PlayerCombat>().enabled = true;
        this.enabled = true;
        PC.currentLevel = 1;
        PB.buffList = new List<int>();
        PB.selectableBuffs = new Dictionary<int, string[]>();
        PB.selectableBuffs.Add(1, new[] { "ATTACK SPEED LEVEL - 1", "A SWIFT STRIKE IS THE FINE LINE BETWEEN SURVIVAL AND DEATH, LEARN TO ATTACK FASTER" });
        PB.selectableBuffs.Add(4, new[] { "MOVEMENT SPEED LEVEL - 1", "A NINJA SHOULD LEARN TO MOVE LIKE THE WIND, INCREASE YOUR SPEED" });
        PB.selectableBuffs.Add(7, new[] { "KNOCK BACK BUFF LEVEL - 1", "EXTEND YOUR ENERGY TO YOUR ATTACKS, KNOCK YOUR ENEMIES FURTHER BACK" });
        PB.selectableBuffs.Add(9, new[] { "DASH LENGTH LEVEL - 1", "A TRUE NINJA KNOWS WHEN TO KEEP THEIR DISTANCE, DASH FURTHER LENGTHS" });
        PB.selectableBuffs.Add(11, new[] { "SHURIKEN COUNT LEVEL - 1", "INCREASE YOUR SHURIKEN LIMIT TO FOUR" });
        PB.selectableBuffs.Add(15, new[] { "SHURIKEN SKILL LEVEL - 1", "PRECISION IS KEY IN YOUR SURVIVAL, INCREASE YOUR ABILITY TO HANDLE SHURIKENS" });
        PB.selectableBuffs.Add(18, new[] { "BARREL BOMB LEVEL - 1", "AT TIMES, CHAOS IS BETTER THAN SILENCE, LEARN TO PLACE EXPLOSIVE BARRELS" });
        PB.selectableBuffs.Add(20, new[] { "CALTROPS LEVEL - 1", "A NINJA SHOULD KNOW HOW TO MANIPULATE THEIR ENVIRONMENT, LEARN TO PLACE CALTROPS " });
        PB.selectableBuffs.Add(22, new[] { "ATTACK RANGE LEVEL - 1", "DON'T FORGET, YOUR KATANA IS AN EXTENSION OF YOUR ARM, LEARN TO ATTACK FASTER" });
        PB.firstTimeBuff = false;
        PC.maxHealth = 100;
        PC.currentHealth = 100;
        PC.KBforce = 1;
        PC.attackRange = 0.33f;
        PC.attackRate = 2f;
        PC.shurikenSpeed = 2.2f;
        PC.shurikenRate = 0.36f;
        PC.reloadRate = 1.05f;
        PC.maxShurikenAmount = 3;
        PC.currentShuriken = 3;
        PC.totalAttackDamaged = 0;
        PC.totalEnemyKilled = 0;
        moveSpeed = 1.3f;
        dashSpeed = 55;
        transform.GetComponent<GameOverUIScript>().isGameOver = false;
    }



}
