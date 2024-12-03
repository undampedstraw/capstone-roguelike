using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public bool[] slotFull;
    public bool[] weaponSlotFull;
    public GameObject[] InvSlots;
    public GameObject[] WeaponSlots;
    private int selectedSlot = 0;
    private int selectedWeaponSlot = 0;
    public int getSlot() { return selectedSlot; }

    public int getWeaponSlot() { return selectedWeaponSlot;  }

    private RectTransform slotSelectSpritePos;
    private RectTransform weaponSlotSelectSpritePos;

    private float[] slotPositions = {-150f, -65f, 20f, 105f};
    private float[] weaponSlotPositions = {-17f, 68f, 153f, 238f, 323f};
    private string[] weaponNames = { "LightWeapon", "WaterWeapon", "NatureWeapon", "FireWeapon", "AirWeapon"};
    private string selectedWeapon;

    MeleeAim playerWeapon;
    player player;

    private void Start()
    {
        //slotSelectSpritePos = GetComponent<RectTransform>();
        slotSelectSpritePos = GameObject.Find("SelectSlot").GetComponent<RectTransform>();
        weaponSlotSelectSpritePos = GameObject.Find("WeaponSelectSlot").GetComponent<RectTransform>();
        playerWeapon = GameObject.Find("MeleeAim").GetComponent<MeleeAim>();
        player = GameObject.Find("Player").GetComponent<player>();

    }
    public void Update()
    {
        handleItemInventory();
        handleWeaponInventory();
    }

    private void handleItemInventory()
    {
        //UnityEngine.Debug.Log(slotSelectSpritePos);
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            slotSelectSpritePos.anchoredPosition = new Vector2(slotPositions[0], 17);
            selectedSlot = 0;
            //UnityEngine.Debug.Log("pressed 1");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            slotSelectSpritePos.anchoredPosition = new Vector2(slotPositions[1], 17);
            selectedSlot = 1;
            //UnityEngine.Debug.Log("pressed 2");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            slotSelectSpritePos.anchoredPosition = new Vector2(slotPositions[2], 17);
            selectedSlot = 2;
            //UnityEngine.Debug.Log("pressed 3");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            slotSelectSpritePos.anchoredPosition = new Vector2(slotPositions[3], 17);
            selectedSlot = 3;
            //UnityEngine.Debug.Log("pressed 4");
        }
    }

    private void handleWeaponInventory()
    {
        if(Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if(selectedWeaponSlot < WeaponSlots.Length - 1)
            {
                selectedWeaponSlot++;

            }
            //UnityEngine.Debug.Log(selectedWeaponSlot);

        }
        else if(Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (selectedWeaponSlot > 0)
            {
                selectedWeaponSlot--;
            }
            //UnityEngine.Debug.Log(selectedWeaponSlot);
        }
        weaponSlotSelectSpritePos.anchoredPosition = new Vector2(weaponSlotPositions[selectedWeaponSlot], 495);
        if (WeaponSlots[selectedWeaponSlot].transform.childCount > 0)
        {
            //UnityEngine.Debug.Log(WeaponSlots[selectedWeaponSlot].transform.GetChild(0).name);
            string current_weapon = WeaponSlots[selectedWeaponSlot].transform.GetChild(0).name;
            if (current_weapon.Contains("(Clone)"))
            {
                int index = current_weapon.IndexOf("(Clone)");
                current_weapon = current_weapon.Substring(0, index);
            }
            //UnityEngine.Debug.Log(playerWeapon.getCurrentElement());
            if(current_weapon != playerWeapon.getCurrentElement())
            {
                playerWeapon.setCurrentElement(current_weapon);
            }

        }
    }
}