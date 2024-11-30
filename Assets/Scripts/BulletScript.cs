using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float speed;
    public float lifeTime;
    public float distance;
    public int damage;
    public LayerMask whatIsSolid;


    public int[] damagePoint = { 1, 2, 3, 4, 5, 6, 7 };
    public float[] pushForce = { 2.0f, 2.2f, 2.5f, 3.0f, 3.2f, 3.6f, 4.0f };

    //upgrade
    public int weaponLevel = 0;
    public SpriteRenderer spriteRenderer;

    private void Start()
    {
        Invoke("DestroyProjectile", lifeTime);
    }

    private void Update()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, distance, whatIsSolid);
        if (hitInfo.collider != null)
        {
            if (hitInfo.collider.CompareTag("Fighter"))
            {
                Damage dmg = new Damage
                {
                    damageAmount = damage,
                    origin = transform.position,
                    pushForce = 1,
                };
                Debug.Log("damage done");
                hitInfo.collider.SendMessage("ReceiveDamage", dmg);
            }
            DestroyProjectile();
            Debug.Log("Collison happen");
        }


        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}