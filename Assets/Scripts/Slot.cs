using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    private Inventory inventory;
    public int slotID;

    private void Start()
    {
        inventory = GameObject.Find("Player").GetComponent<Inventory>();
    }

    private void Update()
    {
        if(transform.childCount <= 0)
        {
            inventory.slotFull[slotID] = false;
        }
    }
    public void DropItem()
    {
        foreach(Transform child in transform)
        {
            child.GetComponent<DropItem>().SpawnDroppedItem();
            GameObject.Destroy(child.gameObject);
        }
    }
}
