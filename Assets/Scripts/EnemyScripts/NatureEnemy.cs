using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class NatureEnemy : Enemy
{
    public float chargeSpeed;
    private bool chargeMode = false;
    private bool timerMode = false;
    public float chargeCooldown;
    private float chargeTimer;
    Vector3 detectedPlayerPosition;
    protected override void Start()
    {
        base.Start();
        
    }

    IEnumerator test()
    {
        yield return new WaitForSeconds(1);
        UnityEngine.Debug.Log("ueshild");
        yield return new WaitForSeconds(1);
        UnityEngine.Debug.Log("ueshild");
        yield return new WaitForSeconds(1);
        UnityEngine.Debug.Log("ueshild");
    }


    protected override void FixedUpdate()
    {
        //StartCoroutine(chargePlayer());

        //collision overlaps
        //collidingWithPlayer = false;
        //hitbox.OverlapCollider(filter, hits);
        //for (int i = 0; i < hits.Length; i++)
        //{
        //    if (hits[i] == null)
        //        continue;
        //    //OnCollide(hits[i]);

        //    if (hits[i].tag == "Fighter" && hits[i].name == "Player")
        //        collidingWithPlayer = true;

        //    //clean array
        //    hits[i] = null;
        //}
        base.FixedUpdate();
    }

    protected override void Death()
    {
        base.Death();
    }

    //IEnumerator chargePlayer()
    protected override void followPlayer()
    {
        if(timerMode)
        {

            if (Time.time < chargeTimer)
            {
                UnityEngine.Debug.Log("current time: " + Time.time);
                UnityEngine.Debug.Log("end time: " + chargeCooldown);
                UnityEngine.Debug.Log("TIMER MODE");
                return;
            }
            else
            {
                UnityEngine.Debug.Log("END TIMER MODE");
                timerMode = false;
            }
            
        }

        //check if player is within chase length
        if (Vector3.Distance(playerTransform.position, startingPosition) < chaseLength)
        {
            if ((Vector3.Distance(playerTransform.position, startingPosition) < triggerLength) && !chargeMode)
            {
                chasing = true;
                detectedPlayerPosition = playerTransform.position;
                chargeMode = true;
            }

            

            if (chasing && chargeMode)
            {
                if (!collidingWithPlayer)
                {
                    transform.position = Vector3.MoveTowards(this.transform.position, detectedPlayerPosition, chargeSpeed * Time.deltaTime);
                }
                if (transform.position == detectedPlayerPosition)
                {
                    chargeTimer = Time.time + chargeCooldown;
                    UnityEngine.Debug.Log("start timer");
                    timerMode = true;
                    chargeMode = false;
                    //yield return new WaitForSeconds(2);
                    

                }
            }
            //else
            //{
            //    transform.position = Vector3.MoveTowards(this.transform.position, startingPosition, enemySpeed * Time.deltaTime);
            //}
        }
        else
        {
            //transform.position = Vector3.MoveTowards(this.transform.position, startingPosition, enemySpeed * Time.deltaTime);
            chasing = false; //commented because enemy will chase until killed
        }
    }
}
