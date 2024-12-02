using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrozenBulletScript : MonoBehaviour
{
    public float speed;
    public float lifeTime;
    public float distance;
    public int damage;
    public LayerMask whatIsSolid;


    public int[] damagePoint = { 1, 2, 3, 4, 5, 6, 7 };
    public float[] pushForce = { 2.0f, 2.2f, 2.5f, 3.0f, 3.2f, 3.6f, 4.0f};

    //upgrade
    public int weaponLevel = 0;
    public SpriteRenderer spriteRenderer;

    protected virtual void Start()
    {
        Invoke("DestroyProjectile", lifeTime);
    }

    protected virtual void Update()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, distance, whatIsSolid);

        if (hitInfo.collider != null) {

            Enemy enemy = hitInfo.collider.GetComponent<Enemy>();
            if (hitInfo.collider.CompareTag("Bullet"))
                return;
            if (hitInfo.collider.CompareTag("Fighter")) {
                

                if(enemy != null)
                {
                int critDamage = damage * 2;

                if(enemy.isFire)
                {
                    Damage dmg = new Damage
                    {
                        damageAmount = critDamage,
                        origin = transform.position,
                        pushForce = 1,
                    };
                    hitInfo.collider.SendMessage("ReceiveDamage", dmg);
                    hitInfo.collider.SendMessage("ApplySlowEffect", 3f);
                }
                else
                {
                    Damage dmg = new Damage
                    {
                        damageAmount = damage,
                        origin = transform.position,
                        pushForce = 1,
                    };

                    hitInfo.collider.SendMessage("ReceiveDamage", dmg);
                    hitInfo.collider.SendMessage("ApplySlowEffect", 3f);
                }
                //Debug.Log("damage done");
            }
            }
            DestroyProjectile();
            //Debug.Log("Collison happen");
            
        }


        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    protected void DestroyProjectile() {
        Destroy(gameObject);
    }
}