using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirEnemy : Enemy
{
    public GameObject projectile;

    private float timeBtwShots;
    public float startTimeBtwShots;

    public float attackPatternCooldown;
    private float attackPatternTime;
    private bool pauseAttack = false;
    private int BulletFlag = 0;


    protected override void Start()
    {
        base.Start();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    protected override void followPlayer()
    {
        Vector3 bulletDirection = playerTransform.position - transform.position;
        bulletDirection.Normalize();

        float angleBetween = Mathf.Atan2(bulletDirection.y, bulletDirection.x) * Mathf.Rad2Deg;

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
                if (pauseAttack)
                {
                    if (Time.time - attackPatternTime < attackPatternCooldown)
                    {
                        return;
                    }
                    else
                        pauseAttack = false;
                }

                if (timeBtwShots <= 0)
                {

                    Instantiate(projectile, transform.position, Quaternion.Euler(0, 0, angleBetween - 90f));

                    timeBtwShots = startTimeBtwShots;
                    BulletFlag++;
                    if (BulletFlag % 3 == 0)
                    {
                        pauseAttack = true;
                        attackPatternTime = Time.time;
                    }
                }
                else
                {
                    timeBtwShots -= Time.deltaTime;
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

        

        //UnityEngine.Debug.Log(angleBetween);

        
    }

    protected override void Death()
    {
        base.Death();
    }
}
