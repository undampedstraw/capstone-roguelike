using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NatureBoss : Enemy
{
    public GameObject projectile;

    private float timeBtwShots;
    private float startTimeBtwShots;

    private float[] bulletPattern = { 0f, 11.25f, 22.5f, 33.75f, 45f, 55.25f, 67.5f, 78.75f, 90f, 101.25f, 112.5f, 123.75f, 135f, 146.25f, 157.5f, 168.75f, 180f,
                                        -11.25f, -22.5f, -33.75f, -45f, -55.25f, -67.5f, -78.75f, -90f, -101.25f, -112.5f, -123.75f, -135f, -146.25f, -157.5f, -168.75f};

    public float attackPattern1Cooldown;
    public float attackPattern2Cooldown;
    public float attackPattern3Cooldown;

    private float attackPatternTime;
    private bool pauseAttack = false;
    private bool pattern1Active = false;

    private int BulletFlag = 0;
    private int numPattern2;
    private int pattern2Count = 0;
    private bool pattern2Active = false;

    private bool pattern3Active = false;

    private int patternFlag = 1;

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

                attackPattern();
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

    private void attackPattern()
    {
        if (pattern2Count == numPattern2)
        {
            pattern2Active = false;
            pattern2Count = 0;
        }
        if (!pattern2Active && !pattern1Active && !pattern3Active)
        {
            patternFlag = Random.Range(1, 4);
            UnityEngine.Debug.Log("Switch attack to: " + patternFlag);
        }
        if (patternFlag == 1 && !pattern1Active)
        {
            pattern1Active = true;
        }
        if (patternFlag == 2 && !pattern2Active)
        {
            numPattern2 = 1;
            pattern2Active = true;

        }
        if (patternFlag == 3 && !pattern3Active)
        {
            pattern3Active = true;
        }

        switch (patternFlag)
        {
            case 1:
                startTimeBtwShots = 0.6f;

                if (pauseAttack)
                {
                    if (Time.time - attackPatternTime < attackPattern1Cooldown)
                    {
                        return;
                    }
                    else
                        pauseAttack = false;
                }

                if (timeBtwShots <= 0)
                {

                    foreach (int angle in bulletPattern)
                    {
                        Quaternion bulletRotation = Quaternion.Euler(0, 0, angle);
                        Instantiate(projectile, transform.position, bulletRotation);
                    }

                    timeBtwShots = startTimeBtwShots;
                    BulletFlag++;
                    if (BulletFlag % 3 == 0)
                    {
                        pauseAttack = true;
                        attackPatternTime = Time.time;
                        pattern1Active = false;
                    }
                }
                else
                {
                    timeBtwShots -= Time.deltaTime;
                }
                break;
            case 2:
                startTimeBtwShots = 0.1f;
                Vector3 bulletDirection = playerTransform.position - transform.position;
                bulletDirection.Normalize();

                float angleBetween = Mathf.Atan2(bulletDirection.y, bulletDirection.x) * Mathf.Rad2Deg;
                if (pauseAttack)
                {
                    if (Time.time - attackPatternTime < attackPattern2Cooldown)
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
                    //UnityEngine.Debug.Log(BulletFlag);
                    if (BulletFlag % 10 == 0)
                    {
                        pauseAttack = true;
                        attackPatternTime = Time.time;
                        pattern2Count++;
                    }
                }
                else
                {
                    timeBtwShots -= Time.deltaTime;
                }
                break;
            case 3:
                startTimeBtwShots = 0.025f;
                if (pauseAttack)
                {
                    if (Time.time - attackPatternTime < attackPattern3Cooldown)
                    {
                        return;
                    }
                    else
                        pauseAttack = false;
                }

                if (timeBtwShots <= 0)
                {

                    Instantiate(projectile, transform.position, Quaternion.Euler(0, 0, Random.Range(0, 361)));

                    timeBtwShots = startTimeBtwShots;
                    BulletFlag++;
                    //UnityEngine.Debug.Log(BulletFlag);
                    if (BulletFlag % 40 == 0)
                    {
                        pauseAttack = true;
                        attackPatternTime = Time.time;
                        pattern3Active = false;
                    }
                }
                else
                {
                    timeBtwShots -= Time.deltaTime;
                }
                break;
            default:
                break;
        }
    }

    protected override void Death()
    {
        base.Death();
    }
}
