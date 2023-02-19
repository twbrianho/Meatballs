using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{

    public Item Item;

    // Should this be seperate between pickable and non pickable?

    void Pickup()
    {
        InventoryController.Instance.Add(Item);
        //Destroy item

    }
    
    private void OnMouseDown()
    {
        Pickup();
    }


}
