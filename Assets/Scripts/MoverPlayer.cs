using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MoverPlayer : Fighter
{
    private BoxCollider2D boxCollider;
    public Rigidbody2D rigidbodyPlayer;
    private Vector3 moveDelta;
    private RaycastHit2D hit;
    //public float ySpeed = 0.75f;
    //public float xSpeed = 1.0f;

    public float moveSpeed;

    private Vector3 movement;



    public SpriteRenderer spriteRenderer;

    //public SpriteRenderer childSprite;

    protected virtual void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidbodyPlayer = GetComponent<Rigidbody2D>();
    }

    protected virtual void UpdateMotor(Vector3 input)
    {

        //TODO: weird issue with getting stuck on walls. tried fixing with physics material 2d to no avail
        rigidbodyPlayer.velocity = input * moveSpeed;

        ////reset move delta
        //moveDelta = new Vector3(input.x * xSpeed, input.y * ySpeed, 0);
        //moveDelta.Normalize();


        ////add push vector
        //moveDelta += pushDirection;
        ////reduce push force every frame by recovery speed
        //pushDirection = Vector3.Lerp(pushDirection, Vector3.zero, pushRecoverySpeed);

        ////collision detection: cast as box first then if box is null, then allow movement
        //hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(0, moveDelta.y), Mathf.Abs(moveDelta.y * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
        //if (hit.collider == null)
        //{
        //    //move sprite
        //    //transform.Translate(0, moveDelta.y * Time.deltaTime, 0);
        //    rigidbodyPlayer.velocity = input * moveSpeed;
        //}

        ////collision detection: cast as box first then if box is null, then allow movement
        //hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(moveDelta.x, 0), Mathf.Abs(moveDelta.x * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
        //if (hit.collider == null)
        //{
        //    //move sprite
        //    //transform.Translate(moveDelta.x * Time.deltaTime, 0, 0);
        //    rigidbodyPlayer.velocity = input * moveSpeed;
        //}
    }
}
