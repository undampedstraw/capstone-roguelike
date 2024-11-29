using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public float offset;

    public GameObject projectile;

    public Transform shotPoint;
    public Animator camAnim;

    private float timeBtwShots;
    public float startTimeBtwShots;

    private void Update()
    {
        // Handles the weapon rotation
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);

<<<<<<< Updated upstream
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
=======
        if (timeBtwShots <= 0)
>>>>>>> Stashed changes
        {
            if (Input.GetMouseButton(0))
            {
                Instantiate(projectile, shotPoint.position, transform.rotation);
                timeBtwShots = startTimeBtwShots;
            }
        }
<<<<<<< Updated upstream

        if(Input.GetMouseButton(0) && canFire) //left mouse click
        {
            canFire = false;
            Instantiate(bullet, bulletTransform.position, Quaternion.identity);
        }
=======
        else {
            timeBtwShots -= Time.deltaTime;
        }

       
>>>>>>> Stashed changes
    }
}
