using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseHealthPotion : MonoBehaviour
{
    public GameObject effect;
    private player player;
    private Inventory inventory;
    private int invSlot;
    private int playerSelectedSlot;
    private string parentName;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<player>();
        inventory = GameObject.Find("Player").GetComponent<Inventory>();
        playerSelectedSlot = inventory.getSlot();
        parentName = transform.parent.name;
        invSlot = (int)(parentName[parentName.Length - 1] - '0') - 1;
    }

    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.Q))
        {
            //UnityEngine.Debug.Log(invSlot);
            if (inventory.getSlot() == invSlot)
            {
                //UnityEngine.Debug.Log("potion is in slot: " + invSlot);
                UseItem();
            }
            
        }
    }

    public void UseItem()
    {
        //when we have a health potion effect
        //Instantiate(effect, player.transform.position, Quaternion.identity);

        
        GameManager.instance.player.Heal(player.maxHitPoint-player.hitpoint);
        player.hitpoint = player.maxHitPoint;
        GameManager.instance.OnHitpointChange();
        
        Destroy(gameObject);
    }

}
