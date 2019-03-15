using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventoryManager : MonoBehaviour
{
    public Image[] inventorySlots;
    public int[] inventoryAmounts;
    public Text[] amountTexts;
    private int currentSelectedSlot = 0;

	// Use this for initialization
	void Start ()
    {
        UpdateSelectedSlot();
	}

    // When game object is enabled
    void OnEnable()
    {
        PlayerScript.OnEventGetBlock += GetSelectedSlot;
        VoxelBlock.OnEventBlockPickup += AddToInventory;
    }
    // When game object is disabled
    void OnDisable()
    {
        PlayerScript.OnEventGetBlock -= GetSelectedSlot;
        VoxelBlock.OnEventBlockPickup -= AddToInventory;
    }

    // Update is called once per frame
    void Update ()
    {
        if (Input.mouseScrollDelta.y != 0)
        {
            if (Input.mouseScrollDelta.y > 0)
            {
                currentSelectedSlot++;

                if (currentSelectedSlot >= inventorySlots.Length)
                {
                    currentSelectedSlot = 0;
                }
            }
            else
            {
                currentSelectedSlot--;

                if (currentSelectedSlot < 0)
                {
                    currentSelectedSlot = inventorySlots.Length - 1;
                }
            }

            UpdateSelectedSlot();
        }
	}

    private void UpdateSelectedSlot()
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            inventorySlots[i].color = Color.black;
        }
        inventorySlots[currentSelectedSlot].color = Color.white;
    }


    /*
    ==================================================
    Returning Information For The Player Controller
    ==================================================
    */
    private bool GetSelectedSlot(out int blockType)
    {
        blockType = currentSelectedSlot;
        if (inventoryAmounts[currentSelectedSlot] != 0)
        {
            inventoryAmounts[currentSelectedSlot]--;
            amountTexts[currentSelectedSlot].text = inventoryAmounts[currentSelectedSlot].ToString();
            return true;
        }
        else
        {
            return false;
        }
    }

    private void AddToInventory(int blockType)
    {
        int i = blockType - 1;

        inventoryAmounts[i]++;
        amountTexts[i].text = inventoryAmounts[i].ToString();
    }
}
