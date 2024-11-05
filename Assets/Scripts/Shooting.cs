using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    private Camera Camera;
    private Vector3 mousePosition;
    public GameObject bullet;
    public Transform bulletTransform;
    public bool canFire;
    public float timer;
    public float timeBetweenFiring;

    private player player;

    //public SpriteRenderer spriteRenderer;

    void Start()
    {
        Camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        player = GameObject.Find("Player").GetComponent<player>();
        //spriteRenderer = GameObject.Find("PlayerSprite").GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        mousePosition = Camera.ScreenToWorldPoint(Input.mousePosition);
        Vector3 rotation = mousePosition - transform.position;

        float rotationZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        //UnityEngine.Debug.Log(rotationZ);
        transform.rotation = Quaternion.Euler(0, 0, rotationZ);

        player.spriteRenderer.flipX = rotationZ > 90 || rotationZ < -90;
        player.childSprite.flipX = rotationZ > 90 || rotationZ < -90;
        //spriteRenderer.flipX = rotationZ > 90 || rotationZ < -90;

        if (!canFire)
        {
            timer += Time.deltaTime;
            if(timer > timeBetweenFiring)
            {
                canFire = true;
                timer = 0;
            }
        }

        if(Input.GetMouseButton(0) && canFire) //left mouse click
        {
            canFire = false;
            Instantiate(bullet, bulletTransform.position, Quaternion.identity);
        }
    }
}
