using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    public GameObject item;
    private Transform player;

    private void Start()
    {
        player = GameObject.Find("Player").transform;
    }

    public void SpawnDroppedItem()
    {
        Vector2 playerPosition = new Vector2(player.position.x, player.position.y + 0.16f);
        Instantiate(item, playerPosition, Quaternion.identity);
    }
}
