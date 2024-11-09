using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAim : MonoBehaviour
{
    private Vector3 mousePosition { get; set; }
    private Camera Camera;
    public bool isAttacking;

    private void Start()
    {
        Camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    private void Update()
    {
        if (isAttacking)
            return;
        mousePosition = Camera.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        mousePosition.Normalize();
        float rotationZ = Mathf.Atan2(mousePosition.y, mousePosition.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotationZ);

        Vector3 weaponScale = transform.localScale;
        if (rotationZ > 90 || rotationZ < -90)
            weaponScale.y = -1;
        else
            weaponScale.y = 1;
        transform.localScale = weaponScale;
    }

    public void resetIsAttacking()
    {
        isAttacking = false;
    }
}
