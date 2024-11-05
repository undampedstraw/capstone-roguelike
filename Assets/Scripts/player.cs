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
    public static player instance;
    private bool isAlive = true;

    public bool isRolling;
    public float rollSpeed;
    private float rollCooldown = 0.4f;
    private float lastRoll;

    private Animator animator;

    public SpriteRenderer childSprite;

    public Vector3 rollDirection;
    private TrailRenderer trailRenderer;

    private float currentMoveSpeed;
    private float lockX, lockY;

    private void Awake()
    {
        if (instance != null)
        {

            Destroy(gameObject);
            instance = this;
            //DontDestroyOnLoad(gameObject);
            //instance = this;????????
        }
        else
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
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
        bool rollCooldownCalc = Time.time - lastRoll > rollCooldown;
        if (rollCooldownCalc && isRolling == true)
        {
            trailRenderer.emitting = false;
            isRolling = false;
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
        }

        
        

        //implement roll input here
        if(Input.GetKeyDown(KeyCode.Space) && (x != 0 || y!= 0)) //only when player is moving can they roll add later " && isRolling == false"
        {
            rollDirection = new Vector3 (x, y, 0).normalized;
            lockX = x;
            lockY = y;
            if (rollCooldownCalc)
            {
                lastRoll = Time.time;
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
            return;

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
