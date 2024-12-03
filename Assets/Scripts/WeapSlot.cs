using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeapSlot : MonoBehaviour
{
    private Inventory inventory;
    public int slotID;

    private void Start()
    {
        inventory = GameObject.Find("Player").GetComponent<Inventory>();
    }

    private void Update()
    {
        if (transform.childCount <= 0)
        {
            inventory.weaponSlotFull[slotID] = false;
        }
        //if(transform.childCount == 1)
        //{
        //    //transform.GetChild(0).name
        //    UnityEngine.Debug.Log(transform.GetChild(0).name);
        //}

    }
}
