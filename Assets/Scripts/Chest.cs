using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Collectable
{
    public Sprite emptyChest;
    public int pesosAmount;

    public GameObject[] possibleItems;

    protected override void OnCollect()
    {
        if(!collected)
        {
            collected = true;
            GetComponent<SpriteRenderer>().sprite = emptyChest;
            UnityEngine.Debug.Log("grant " + pesosAmount + " pesos.");
            GameManager.instance.pesos += pesosAmount;
            GameManager.instance.ShowText("+" + pesosAmount + " pesos!", 25, Color.yellow, transform.position, Vector3.up * 40, 1.5f);
        }
        /*base.OnCollect();
        UnityEngine.Debug.Log("grant pesos");*/
    }
}
