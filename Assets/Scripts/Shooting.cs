using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    private Camera Camera; //maybe make a separate camera for this?
    private Vector3 mousePosition;
    private player player;
    public GameObject bullet;
    public Transform bulletTransform;
    public bool canFire;
    public float timer;
    public float timeBetweenFiring;

    void Start()
    {
        Camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

    }

    // Update is called once per frame
    void Update()
    {
        mousePosition = Camera.ScreenToWorldPoint(Input.mousePosition);
        Vector3 rotation = mousePosition - transform.position;

        float rotationZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        //UnityEngine.Debug.Log(rotationZ);
        transform.rotation = Quaternion.Euler(0, 0, rotationZ);

        if (rotationZ > 90 || rotationZ < -90)
        {
            //UnityEngine.Debug.Log("Sprite should rotate left");
        }
        else
        {
            //UnityEngine.Debug.Log("Sprite should rotate right");
        }
        //flip sprite when left or right
        //if (moveDelta.x > 0)
        //    transform.localScale = Vector3.one;
        //else if (moveDelta.x < 0)
        //    transform.localScale = new Vector3(-1, 1, 1);

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
