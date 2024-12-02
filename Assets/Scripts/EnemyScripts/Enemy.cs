using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Mover
{
    public int xpValue = 1; //xp value temp mechanic

    public float triggerLength = 1;
    public float chaseLength = 5;
    public float enemySpeed;
    private float originalSpeed;
    protected bool chasing;
    protected bool collidingWithPlayer;
    protected Transform playerTransform;
    protected Vector3 startingPosition;
    
    public virtual bool isFire { get; protected set; } = false;
    public virtual bool isWater { get; protected set; } = false;
    public virtual bool isNature { get; protected set; } = false;
    public virtual bool isAir { get; protected set; } = false;

    //hitbox
    public ContactFilter2D filter;
    protected BoxCollider2D hitbox;
    protected Collider2D[] hits = new Collider2D[10];

    // Slow effect variables
    private bool isSlowed = false;          // To check if the enemy is slowed

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

    // Method to apply the slow effect
    public void ApplySlowEffect(float slowDuration)
    {
        if (!isSlowed)
        {
            StartCoroutine(SlowEffectCoroutine(slowDuration));  // Start the coroutine to apply the slow effect
        }
    }

    // Coroutine to handle the slow effect duration and revert the changes
    private IEnumerator SlowEffectCoroutine(float duration)
    {
        isSlowed = true;
        originalSpeed = enemySpeed;
        enemySpeed *= 0.4f;

        // Change sprite color to blue to indicate slow effect
        spriteRenderer.color = Color.blue;

        // Wait for the duration of the slow effect
        yield return new WaitForSeconds(duration);

        // Revert back to normal speed and sprite color
        isSlowed = false;
        enemySpeed = originalSpeed;  // Set back to original speed (or any default speed)
        spriteRenderer.color = Color.white;  // Reset sprite color
    }

    public void ApplyFireEffect()
    {
    StartCoroutine(FireEffectCoroutine());
    }

    private IEnumerator FireEffectCoroutine()
    {
        float elapsed = 0f;
        spriteRenderer.color = Color.red;

        while (elapsed < 5)
        {
            // Apply damage every second
            Damage fireDamage = new Damage
            {
                damageAmount = 1,
                origin = transform.position,
                pushForce = 0 // Fire effect doesn't push the enemy
            };

            ReceiveDamage(fireDamage); // Use ReceiveDamage to apply the damage logic
            elapsed += 1f;

            // Wait for 1 second
            yield return new WaitForSeconds(1f);
        }
        spriteRenderer.color = Color.white;
    }

    
}

