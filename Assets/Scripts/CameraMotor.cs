using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotor : MonoBehaviour
{
    private Transform lookAt;
    public float boundX = 0.15f;
    public float boundY = 0.05f;
    public float cursorThreshold;
    public float ignoreCursorRadius;

    private static CameraMotor instance;
    private Camera mainCamera;
    public static CameraMotor Instance {  get { return instance; } }

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(this.gameObject);
        else
            instance = this;
    }

    private void Start()
    {
        //DontDestroyOnLoad(gameObject);
        lookAt = GameObject.Find("Player").transform;
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    private void LateUpdate()
    {
        Vector3 delta = Vector3.zero;

        //check if inside bounds of x axis
        float deltaX = lookAt.position.x - transform.position.x;
        if(deltaX > boundX || deltaX < -boundX)
        {
            if(transform.position.x < lookAt.position.x)
            {
                delta.x = deltaX - boundX;
            }
            else
            {
                delta.x = deltaX + boundX;
            }
        }

        //check if inside bounds of y axis
        float deltaY = lookAt.position.y - transform.position.y;
        if (deltaY > boundY || deltaY < -boundY)
        {
            if (transform.position.y < lookAt.position.y)
            {
                delta.y = deltaY - boundY;
            }
            else
            {
                delta.y = deltaY + boundY;
            }
        }

        transform.position += new Vector3(delta.x, delta.y, 0);

        followCursor();
    }

    private void followCursor()
    {
        Vector3 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector3 distanceFromPlayer = mousePos - lookAt.position;

        Cursor.lockState = CursorLockMode.Confined;

        //not really working will fix later. unused for now
        //maybe if in radius, move camera back to center the player?
        //if (distanceFromPlayer.x < ignoreCursorRadius && distanceFromPlayer.y < ignoreCursorRadius)
        //{
        //    UnityEngine.Debug.Log("ignore cursor!");
        //    return; 
        //}

        Vector3 targetPos = (lookAt.position + mousePos) / 3.0f;

        

        targetPos.x = Mathf.Clamp(targetPos.x, -cursorThreshold + lookAt.position.x, cursorThreshold + lookAt.position.x);
        targetPos.y = Mathf.Clamp(targetPos.y, -cursorThreshold + lookAt.position.y, cursorThreshold + lookAt.position.y);
        targetPos.z = -1.0f;

        this.transform.position = targetPos;
    }
}
