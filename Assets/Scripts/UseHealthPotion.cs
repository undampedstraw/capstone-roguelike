using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseHealthPotion : MonoBehaviour
{
    public GameObject effect;
    private player player;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<player>();
    }

    public void UseItem()
    {
        //when we have a health potion effect
        //Instantiate(effect, player.transform.position, Quaternion.identity);

        UnityEngine.Debug.Log("using potion");
        GameManager.instance.player.Heal(player.maxHitPoint-player.hitpoint);
        player.hitpoint = player.maxHitPoint;
        GameManager.instance.OnHitpointChange();
        
        Destroy(gameObject);
    }

}
