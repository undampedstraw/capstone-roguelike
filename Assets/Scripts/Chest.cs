using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Collectable
{
    public Sprite emptyChest;
    public int pesosAmount;

    public GameObject[] items;
    private float[] dropLocationsX = { -0.24f, -0.08f, 0.08f, 0.24f};

    protected override void OnCollect()
    {
        if(!collected)
        {
            collected = true;
            GetComponent<SpriteRenderer>().sprite = emptyChest;

            for(int i = 0; i < items.Length; i++)
            {
                Vector2 itemPos = new Vector2(transform.position.x + dropLocationsX[i], transform.position.y + 0.16f);
                Instantiate(items[i], itemPos, Quaternion.identity);
            }

            //UnityEngine.Debug.Log("grant " + pesosAmount + " pesos.");
            //GameManager.instance.pesos += pesosAmount;
            //GameManager.instance.ShowText("+" + pesosAmount + " pesos!", 25, Color.yellow, transform.position, Vector3.up * 40, 1.5f);
        }
    }
}
