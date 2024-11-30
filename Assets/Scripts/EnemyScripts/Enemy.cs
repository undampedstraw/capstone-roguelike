using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Mover
{
    public int xpValue = 1; //xp value temp mechanic

    public float triggerLength = 1;
    public float chaseLength = 5;
    public float enemySpeed;
    protected bool chasing;
    protected bool collidingWithPlayer;
    protected Transform playerTransform;
    protected Vector3 startingPosition;


    //hitbox
    public ContactFilter2D filter;
    protected BoxCollider2D hitbox;
    protected Collider2D[] hits = new Collider2D[10];

    protected override void Start()
    {
        base.Start();
        //playerTransform = GameManager.instance.player.transform;
        //temp code
        playerTransform = GameObject.Find("Player").transform;
        startingPosition = transform.position;
        hitbox = transform.GetChild(0).GetComponent<BoxCollider2D>();
    }

    protected virtual void FixedUpdate()
    {
        //remember to readd basic sprite flipping here
        //UnityEngine.Debug.Log(playerTransform);
        followPlayer();
        
        //collision overlaps
        collidingWithPlayer = false;
        hitbox.OverlapCollider(filter, hits);
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i] == null)
                continue;
            //OnCollide(hits[i]);

            if (hits[i].tag == "Fighter" && hits[i].name == "Player")
                collidingWithPlayer = true;

            //clean array
            hits[i] = null;
        }
    }

    protected override void Death()
    {
        Destroy(gameObject);
        GameManager.instance.GrantXp(xpValue);
        GameManager.instance.ShowText("+" + xpValue + " xp", 30, Color.magenta, transform.position, Vector3.up * 40, 1.0f);
    }

    protected virtual void followPlayer()
    {
        //check if player is within chase length
        if (Vector3.Distance(playerTransform.position, startingPosition) < chaseLength)
        {
            if (Vector3.Distance(playerTransform.position, startingPosition) < triggerLength)
                chasing = true;

            if (chasing)
            {
                if (!collidingWithPlayer)
                {
                    //UpdateMotor((playerTransform.position - transform.position).normalized);
                    transform.position = Vector3.MoveTowards(this.transform.position, playerTransform.position, enemySpeed * Time.deltaTime);
                }
            }
            else
            {
                transform.position = Vector3.MoveTowards(this.transform.position, startingPosition, enemySpeed * Time.deltaTime);
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(this.transform.position, startingPosition, enemySpeed * Time.deltaTime);
            chasing = false;
        }
    }
}
