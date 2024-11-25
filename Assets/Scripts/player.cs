using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

//[RequireComponent(typeof(BoxCollider2D))]
public class player : MoverPlayer
{
    public static player instance { get; private set; }
    private bool isAlive = true;

    public bool isRolling;
    public float rollSpeed;
    private float rollLength = 0.4f;
    private float rollCooldown = 0.5f;
    private float lastRoll;

    private bool canAttack = true; //cant attack during dash

    public Animator animator;

    public SpriteRenderer childSprite;

    public Vector3 rollDirection;
    private TrailRenderer trailRenderer;

    private float currentMoveSpeed;
    private float lockX, lockY;

    public bool getCanAttack(){ return canAttack; }


    private void Awake()
    {
        if (instance != null && instance != this)
        {

            Destroy(gameObject);
            //DontDestroyOnLoad(gameObject);
            //instance = this;????????
        }
        else
        {
            instance = this;
        }
    }

    protected override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
        trailRenderer = GetComponent<TrailRenderer>();
        //NOTE: Because of roll changes, the Mover parent sprite renderer goes unused in player
        childSprite = GameObject.Find("PlayerSprite").GetComponent<SpriteRenderer>();
        currentMoveSpeed = moveSpeed;

    }
    private void Update()
    {
        bool rollLengthCalc = Time.time - lastRoll > rollLength;
        if (rollLengthCalc && isRolling == true)
        {
            trailRenderer.emitting = false;
            isRolling = false;
            canAttack = true;
            currentMoveSpeed = moveSpeed;
        }

        //if (isRolling)
        //    return;


        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        if (isAlive)
        {
            Vector3 move;
            if(!isRolling)
                move = new Vector3(x, y, 0);
            else
                move = new Vector3(lockX, lockY, 0);
            move.Normalize();
            rigidbodyPlayer.velocity = move * currentMoveSpeed;
            if(rigidbodyPlayer.velocity != Vector2.zero)
            {
                //UnityEngine.Debug.Log("player is moving");
                animator.SetBool("isMoving", true);
            }
            else
            {
                //UnityEngine.Debug.Log("player is idling");
                animator.SetBool("isMoving", false);
            }
        }




        //implement roll input here
        bool canRoll;
        if (lastRoll == 0)
            canRoll = true;
        else
            canRoll = Time.time - lastRoll > rollCooldown;
        if(Input.GetKeyDown(KeyCode.Space) && (x != 0 || y!= 0) && canRoll) //only when player is moving can they roll add later " && isRolling == false"
        {
            rollDirection = new Vector3 (x, y, 0).normalized;
            lockX = x;
            lockY = y;
            if (rollLengthCalc)
            {
                lastRoll = Time.time;
                canAttack = false;
                Roll(rollDirection);
            }
        }

    }

    public void SwapSprite(int skinId)
    {
        spriteRenderer.sprite = GameManager.instance.playerSprites[skinId];
        childSprite.sprite = spriteRenderer.sprite;
    }

    public void OnLevelUp()
    {
        maxHitPoint++;
        hitpoint = maxHitPoint;
    }

    public void SetLevel(int level)
    {
        for (int i = 0; i < level; i++)
        {
            OnLevelUp();
        }
    }

    public void Roll(Vector3 dir)
    {
        isRolling = true;
        trailRenderer.emitting = true;
        //transform.Translate(dir.x * rollSpeed * Time.deltaTime, dir.y * rollSpeed * Time.deltaTime, 0);
        //rigidbodyPlayer.AddForce(dir * rollSpeed);
        currentMoveSpeed = rollSpeed;
        animator.SetTrigger("Roll");
    }

    public void Heal(int healingAmount)
    {
        if (hitpoint == maxHitPoint)
        {
            GameManager.instance.ShowText("+0hp", 25, Color.green, transform.position, Vector3.up * 30, 1.0f);
            return;
        }

        hitpoint += healingAmount;
        if (hitpoint > maxHitPoint)
            hitpoint = maxHitPoint;
        GameManager.instance.ShowText("+" + healingAmount.ToString() + "hp", 25, Color.green, transform.position, Vector3.up * 30, 1.0f);
        GameManager.instance.OnHitpointChange();
    }

    protected override void ReceiveDamage(Damage dmg)
    {
        if (!isAlive)
            return;
        base.ReceiveDamage(dmg);
        GameManager.instance.OnHitpointChange();
    }

    protected override void Death()
    {
        isAlive = false;
        GameManager.instance.deathMenuAnimator.SetTrigger("Show");
    }

    public void Respawn()
    {
        Heal(maxHitPoint);
        isAlive = true;
        lastImmune = Time.time;
        pushDirection = Vector3.zero;
    }
}
