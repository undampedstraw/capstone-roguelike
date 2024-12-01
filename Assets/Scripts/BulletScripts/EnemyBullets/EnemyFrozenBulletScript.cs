using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFrozenBulletScript : FrozenBulletScript
{
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, distance, whatIsSolid);
        if (hitInfo.collider != null)
        {
            if (hitInfo.collider.CompareTag("Bullet"))
                return;
            if (hitInfo.collider.CompareTag("Player"))
            {
                Damage dmg = new Damage
                {
                    damageAmount = damage,
                    origin = transform.position,
                    pushForce = 1,
                };
                //Debug.Log("damage done");
                hitInfo.collider.SendMessage("ReceiveDamage", dmg);
                hitInfo.collider.SendMessage("ApplySlowEffect", 3f);
            }
            DestroyProjectile();
            //Debug.Log("Collison happen");
        }


        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }
}
