using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpWeapon : MonoBehaviour
{
    private Inventory inventory;
    public GameObject item;

    private void Start()
    {
        inventory = GameObject.Find("Player").GetComponent<Inventory>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) //change tag to "Player" later
        {
            for (int i = 0; i < inventory.WeaponSlots.Length; i++)
            {
                if (inventory.weaponSlotFull[i] == false)
                {
                    //add item to inventory
                    inventory.weaponSlotFull[i] = true;
                    Instantiate(item, inventory.WeaponSlots[i].transform, false);
                    Destroy(gameObject);
                    break;
                }
            }
        }
    }
}
