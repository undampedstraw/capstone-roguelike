using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    private Inventory inventory;
    public GameObject itemButton;

    private void Start()
    {
        inventory = GameObject.Find("Player").GetComponent<Inventory>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) //change tag to "Player" later
        {
            for (int i = 0; i < inventory.InvSlots.Length; i++)
            {
                if (inventory.slotFull[i] == false)
                {
                    //add item to inventory
                    inventory.slotFull[i] = true;
                    Instantiate(itemButton, inventory.InvSlots[i].transform, false);
                    Destroy(gameObject);
                    break;
                }
            }
        }
    }
}
