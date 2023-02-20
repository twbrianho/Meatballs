using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryControll : MonoBehaviour
{


    public GameObject Inventory;
    public bool inventoryIsClosed;

    // Start is called before the first frame update
    void Start()
    {
        inventoryIsClosed = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("i"))
        {
            if (inventoryIsClosed == true)
            {
                Inventory.SetActive(true);
                inventoryIsClosed = false;

                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

            }
            else
            {
                Inventory.SetActive(false);
                inventoryIsClosed = true;

                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }

}