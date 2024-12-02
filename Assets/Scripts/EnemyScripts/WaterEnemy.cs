using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterEnemy : Enemy
{
    public GameObject projectile;

    private float timeBtwShots;
    public float startTimeBtwShots;

    private float[] bulletPattern = {0f, 45f, 90f, 135f, 180f, -45f, -90f, -135f};
    private float angleMod = 22.5f;
    private int angleFlag = 0;

    public override bool isWater => true;
    

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
                if (timeBtwShots <= 0)
                {
                    foreach (int angle in bulletPattern)
                    {
                        Quaternion bulletRotation;
                        if (angleFlag % 2 == 0)
                            bulletRotation = Quaternion.Euler(0, 0, angle + angleMod);
                        else
                            bulletRotation = Quaternion.Euler(0, 0, angle);
                        Instantiate(projectile, transform.position, bulletRotation);
                    }

                    timeBtwShots = startTimeBtwShots;
                    UnityEngine.Debug.Log("shoot bullet");
                    angleFlag++;
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

        
    }

    protected override void Death()
    {
        base.Death();
    }
}
