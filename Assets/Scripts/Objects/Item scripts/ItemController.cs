using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{

    public Item item;

    // Should this be seperate between pickable and non pickable?

    void Pickup()
    {
        InventoryController.Instance.AddToInventory(item);
        //Destroy item
        Debug.Log(item.name + " obtained!");

    }
    
    private void OnMouseDown()
    {
        Pickup();

    }


}
