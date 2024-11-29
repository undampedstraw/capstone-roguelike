using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Mover
{
    public int xpValue = 1; //xp value temp mechanic

    public float triggerLength = 1;
    public float chaseLength = 5;
    private bool chasing;
    private bool collidingWithPlayer;
    private Transform playerTransform;
    private Vector3 startingPosition;

<<<<<<< Updated upstream
=======

    // Slow effect variables
    private bool isSlowed = false;          // To check if the enemy is slowed


>>>>>>> Stashed changes
    //hitbox
    public ContactFilter2D filter;
    private BoxCollider2D hitbox;
    private Collider2D[] hits = new Collider2D[10];

    protected override void Start()
    {
        base.Start();
        playerTransform = GameManager.instance.player.transform;
        startingPosition = transform.position;
        hitbox = transform.GetChild(0).GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {
        //remember to readd basic sprite flipping here


        //check if player is within chase length
        if(Vector3.Distance(playerTransform.position, startingPosition) < chaseLength)
        {
            if(Vector3.Distance(playerTransform.position, startingPosition) < triggerLength)
                chasing = true;
            
            if(chasing)
            {
                if(!collidingWithPlayer)
                {
                    UpdateMotor((playerTransform.position - transform.position).normalized);
                }
            }
            else
            {
                UpdateMotor(startingPosition - transform.position);
            }
        }
        else
        {
            UpdateMotor(startingPosition - transform.position);
            chasing = false;
        }
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

        // Change sprite color to blue to indicate slow effect
        spriteRenderer.color = Color.blue;

        // Wait for the duration of the slow effect
        yield return new WaitForSeconds(duration);

        // Revert back to normal speed and sprite color
        isSlowed = false;
        enemySpeed = 5f;  // Set back to original speed (or any default speed)
        spriteRenderer.color = Color.white;  // Reset sprite color
    }

    protected override void Death()
    {
        Destroy(gameObject);
        GameManager.instance.GrantXp(xpValue);
        GameManager.instance.ShowText("+" + xpValue + " xp", 30, Color.magenta, transform.position, Vector3.up * 40, 1.0f);
    }
}
