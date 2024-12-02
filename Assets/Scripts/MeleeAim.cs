using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAim : MonoBehaviour
{
    private Vector3 mousePosition { get; set; }
    private Camera Camera;
    public bool isAttacking;

    public GameObject rangedWeapon;  // Reference to your ranged weapon GameObject
    public GameObject meleeWeapon;   // Reference to your melee weapon GameObject

    public GameObject basicElement;
    public GameObject waterElement;
    public GameObject fireElement;
    public GameObject natureElement;
    public GameObject airElement;

    private GameObject currentElement;

    private void Start()
    {
        Camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        rangedWeapon.SetActive(false);
        DeactivateAllElements();
        currentElement = waterElement;
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
        //Vector3 wandScale = 
        if (rotationZ > 90 || rotationZ < -90)
        {
            weaponScale.y = -1;
        } 
        else
        {
            weaponScale.y = 1;
        }
        transform.localScale = weaponScale;

        if (Input.GetMouseButton(0))
        {
            rangedWeapon = currentElement;
            ActivateRangedWeapon();
        }

        else if (Input.GetMouseButton(1))
        {
            ActivateMeleeWeapon();
        }
    }

    public void resetIsAttacking()
    {
        isAttacking = false;
    }

    void ActivateRangedWeapon()
    {
        // Activate the ranged weapon and deactivate the melee weapon
        if (rangedWeapon != null && !rangedWeapon.activeInHierarchy)
        {
            rangedWeapon.SetActive(true);
        }

        if (meleeWeapon != null && meleeWeapon.activeInHierarchy)
        {
            meleeWeapon.SetActive(false);
        }
    }

    void ActivateMeleeWeapon()
    {
        // Activate the melee weapon and deactivate the ranged weapon
        if (meleeWeapon != null && !meleeWeapon.activeInHierarchy)
        {
            meleeWeapon.SetActive(true);
        }

        if (rangedWeapon != null && rangedWeapon.activeInHierarchy)
        {
            rangedWeapon.SetActive(false);
        }
    }

    public void SetCurrentElement(int elementIndex)
    {

        // Activate the selected element based on the index
        switch (elementIndex)
        {
            case 1:
                currentElement = basicElement;
                break;
            case 2:
                currentElement = fireElement;
                break;
            case 3:
                currentElement = waterElement;
                break;
            case 4:
                currentElement = airElement;
                break;
            case 5:
                currentElement = natureElement;
                break;
            default:
                Debug.LogWarning("Invalid element index!");
                break;
        }
    }

    private void DeactivateAllElements()
    {
        basicElement.SetActive(false);
        waterElement.SetActive(false);
        fireElement.SetActive(false);
        natureElement.SetActive(false);
        airElement.SetActive(false);
    }
}
