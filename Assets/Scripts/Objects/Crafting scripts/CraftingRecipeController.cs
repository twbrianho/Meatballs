using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingRecipeController : MonoBehaviour
{

    public CraftingRecipe craftingRecipe;


    // Start is called before the first frame update
    void Craft()
    {
        InventoryController.Instance.Craft(craftingRecipe);
        //Destroy item

    }

    private void OnMouseDown()
    {
        Craft();
    }

}
