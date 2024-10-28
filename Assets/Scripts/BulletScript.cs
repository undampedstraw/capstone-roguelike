using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private Vector3 mousePosition;
    private Camera Camera;
    private Rigidbody2D rb2D;
    public float speed;
    public float bulletLifetime; 
    // Start is called before the first frame update
    void Start()
    {
        Camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        rb2D = GetComponent<Rigidbody2D>();
        mousePosition = Camera.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePosition - transform.position;
        Vector3 rotation = transform.position - mousePosition;
        rb2D.velocity = new Vector2 (direction.x, direction.y).normalized * speed;
        float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + 90);

        //if bullet has not collided with a wall........ goes in update?
        Destroy(gameObject, bulletLifetime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
