using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : Collidable
{
    //Damage struct
    public int damagePoint = 1;
    public float pushForce = 2.0f;

    //upgrade
    public int weaponLevel = 0;
    private SpriteRenderer spriteRenderer;

    //swing sword
    private float cooldown = 0.5f;
    private float lastSwing;
    private Animator anim;

    protected override void Start()
    {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    protected override void Update()
    {
        base.Update();

        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(Time.time - lastSwing > cooldown)
            {
                lastSwing = Time.time;
                Swing();
            }
        }
    }

    protected override void OnCollide(Collider2D coll)
    {
        //base.OnCollide(coll);
        if (coll.tag == "Fighter")
        {
            if (coll.name == "Player")
                return;
            //create damage object and send to fighter that is hit
            Damage dmg = new Damage
            {
                damageAmount = damagePoint,
                origin = transform.position,
                pushForce = pushForce,
            };

            coll.SendMessage("ReceiveDamage", dmg);
            UnityEngine.Debug.Log(coll.name);
        }
    }

    private void Swing()
    {
        anim.SetTrigger("Swing_Sword");
    }
}
