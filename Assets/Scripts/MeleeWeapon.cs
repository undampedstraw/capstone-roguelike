using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
    public UnityEvent OnAnimationEventTriggered;
    public bool isAttacking { get; set; }

    private player player;

    public MeleeAim parent;



    protected override void Start()
    {
        base.Start();
        anim = GetComponent<Animator>();
        player = GameObject.Find("Player").GetComponent<player>();
        parent = GameObject.Find("MeleeAim").GetComponent<MeleeAim>();
    }

    protected override void Update()
    {
        base.Update();
        if(Input.GetMouseButton(1))
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
        parent.isAttacking = true;
        anim.SetTrigger("Swing_Sword");
    }

    public void TriggerEvent()
    {
        OnAnimationEventTriggered?.Invoke();
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

}
