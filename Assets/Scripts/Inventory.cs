using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public bool[] slotFull;
    public GameObject[] InvSlots;
    private int selectedSlot = 0;
    public int getSlot() { return selectedSlot; }

    private RectTransform slotSelectSpritePos;

    private float[] slotPositions = {-150f, -65f, 20f, 105f};

    private void Start()
    {
        //slotSelectSpritePos = GetComponent<RectTransform>();
        slotSelectSpritePos = GameObject.Find("SelectSlot").GetComponent<RectTransform>();
    }
    public void Update()
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
        //else
        //    return;
    }
}