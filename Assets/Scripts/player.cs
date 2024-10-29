using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

//[RequireComponent(typeof(BoxCollider2D))]
public class player : MonoBehaviour
{
    private BoxCollider2D boxCollider;
    private Vector3 moveDelta;
    private RaycastHit2D hit;

    private Vector3 mousePosition;
    private Camera Camera;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        Camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    private void FixedUpdate()
    { 
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        //reset move delta
        moveDelta = new Vector3(x, y, 0);
        moveDelta.Normalize();

        //flip sprite when left or right based on mouse position
        flipSprite();   

        //collision detection: cast as box first then if box is null, then allow movement
        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(0, moveDelta.y), Mathf.Abs(moveDelta.y * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
        if(hit.collider == null)
        {
            //move sprite
            transform.Translate(0, moveDelta.y * Time.deltaTime, 0);
        }

        //collision detection: cast as box first then if box is null, then allow movement
        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(moveDelta.x, 0), Mathf.Abs(moveDelta.x * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
        if (hit.collider == null)
        {
            //move sprite
            transform.Translate(moveDelta.x * Time.deltaTime, 0, 0);
        }

    }

    private void flipSprite()
    {
        mousePosition = Camera.ScreenToWorldPoint(Input.mousePosition);
        Vector3 rotation = mousePosition - transform.position;

        float rotationZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        spriteRenderer.flipX = rotationZ > 90 || rotationZ < -90;
    }
}
