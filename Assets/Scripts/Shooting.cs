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

    public float offset;

    public GameObject projectile;

    public Transform shotPoint;
    public Animator camAnim;

    private float timeBtwShots;
    public float startTimeBtwShots;

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
        //Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        //float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);

        mousePosition = Camera.ScreenToWorldPoint(Input.mousePosition);
        Vector3 rotation = mousePosition - transform.position;

        float rotationZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        //UnityEngine.Debug.Log(rotationZ);
        transform.rotation = Quaternion.Euler(0, 0, rotationZ);
        Quaternion bulletRotation = Quaternion.Euler(0, 0, rotationZ + offset);

        int[] directions = new int[2];

        directions = lookDirection(directions, rotationZ);
        //UnityEngine.Debug.Log("x: " + directions[0] + ", y: " + directions[1]);
        //player.spriteRenderer.flipX = rotationZ > 90 || rotationZ < -90;
        player.childSprite.flipX = (rotationZ > 120) || (rotationZ < -120);
        player.animator.SetFloat("HorizontalView", directions[0]);
        player.animator.SetFloat("VerticalView", directions[1]);



        if (timeBtwShots <= 0)
        {
            if (Input.GetMouseButton(0))
            {
                Instantiate(projectile, shotPoint.position, bulletRotation);
                timeBtwShots = startTimeBtwShots;
            }
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }

    }


    private void FireWindVolley(float baseRotationZ)
    {
        float spreadAngle = 15f; // The angle of deviation for the side bullets
        Quaternion centerRotation = Quaternion.Euler(0, 0, baseRotationZ);
        Quaternion leftRotation = Quaternion.Euler(0, 0, baseRotationZ - spreadAngle);
        Quaternion rightRotation = Quaternion.Euler(0, 0, baseRotationZ + spreadAngle);

        Instantiate(projectile, shotPoint.position, centerRotation); // Center bullet
        Instantiate(projectile, shotPoint.position, leftRotation);   // Left bullet
        Instantiate(projectile, shotPoint.position, rightRotation); // Right bullet
    }
    
    private int[] lookDirection(int[] directions, float rotationZ)
    {
        if(rotationZ >= -30 && rotationZ < 30)
        {
            directions[0] = 0;
            directions[1] = 0;
        }
        else if ((rotationZ >= 30 && rotationZ < 60) || (rotationZ >= 120 && rotationZ < 150))
        {
            directions[0] = 1;
            directions[1] = 1;
        }
        else if (rotationZ >= 60 && rotationZ < 120)
        {
            directions[0] = 0;
            directions[1] = 1;
        }
        else if ((rotationZ >= -60 && rotationZ < -30) || (rotationZ >= -150 && rotationZ < -120))
        {
            directions[0] = 1;
            directions[1] = -1;
        }
        else if (rotationZ >= -120 && rotationZ < -60)
        {
            directions[0] = 0;
            directions[1] = -1;
        }
        else
        {
            directions[0] = 0;
            directions[1] = 0;
        }

        return directions;
    }
}
