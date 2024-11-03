using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : Collidable
{
    //Damage struct
    public int[] damagePoint = { 1, 2, 3, 4, 5, 6, 7 };
    public float[] pushForce = { 2.0f, 2.2f, 2.5f, 3.0f, 3.2f, 3.6f, 4.0f};

    //upgrade
    public int weaponLevel = 0;
    public SpriteRenderer spriteRenderer;

    //swing sword
    private float cooldown = 0.5f;
    private float lastSwing;
    private Animator anim;

    private player player;
    public Transform weaponFlip;


    protected override void Start()
    {
        base.Start();
        anim = GetComponent<Animator>();
        player = GameObject.Find("Player").GetComponent<player>();
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

        FlipSprite();
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
                damageAmount = damagePoint[weaponLevel],
                origin = transform.position,
                pushForce = pushForce[weaponLevel],
            };

            coll.SendMessage("ReceiveDamage", dmg);
            UnityEngine.Debug.Log(coll.name);
        }
    }

    private void Swing()
    {
        anim.SetTrigger("Swing_Sword");
    }

    public void UpgradeWeapon()
    {
        weaponLevel++;
        spriteRenderer.sprite = GameManager.instance.weaponSprites[weaponLevel];
    }

    public void SetWeaponLevel(int level)
    {
        weaponLevel = level;
        spriteRenderer.sprite = GameManager.instance.weaponSprites[weaponLevel];
    }

    public void FlipSprite()
    {
        if (player.spriteRenderer.flipX == true)
        {
            spriteRenderer.flipX = true;
            //weaponFlip.
        }
        else
        {
            spriteRenderer.flipX = false;
            transform.position += new Vector3(0.16f, 0, 0);
        }
    }
}
