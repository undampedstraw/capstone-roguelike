using System;
using System.Collections;
using System.Collections.Generic;
using System.Security;
using System.Security.Cryptography;
using UnityEngine;

public class NPCDialogue : Collidable
{
    public string message;
    private float cooldown = 4.0f;
    private float lastShout = -4.0f;

    //protected override void Start()
    //{
    //    lastShout =  cooldown * -1;
    //}

    protected override void OnCollide(Collider2D coll)
    {
        if (Time.time - lastShout > cooldown)
        {
            lastShout = Time.time;
            GameManager.instance.ShowText(message, 20, Color.white, transform.position + new Vector3(0, 0.16f, 0), Vector3.zero, cooldown);
        }
    }
}
