using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

//[RequireComponent(typeof(BoxCollider2D))]
public class player : Mover
{
    public static player instance;
    private bool isAlive = true;

    public bool isRolling;
    public float rollSpeed;
    private float rollCooldown = 0.50f;
    private float lastRoll;

    private Animator animator;

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
    }
    private void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        if(isAlive)
            UpdateMotor(new Vector3 (x, y, 0).normalized);

        //implement roll input here
        if(Input.GetKeyDown(KeyCode.Space) && (x != 0 || y!= 0)) //only when player is moving can they roll
                                                                 //add later " && isRolling == false"
        {
            UnityEngine.Debug.Log("player should dodge roll here");
            if (Time.time - lastRoll > rollCooldown)
            {
                lastRoll = Time.time;
                Roll();
            }
        }

    }

    public void SwapSprite(int skinId)
    {
        spriteRenderer.sprite = GameManager.instance.playerSprites[skinId];
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

    public void Roll()
    {
        //isRolling = true;
        UnityEngine.Debug.Log("Rolling....");
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
