using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public static InventoryController Instance;
    public List<Item> Items = new List<Item>();
    public List<CraftingRecipe> Recipes = new List<CraftingRecipe>();

    public Transform ItemContent;
    public GameObject InventoryItem;


    void Awake()
    {
        Instance = this;   
    }

    // Add items
    public void Add(Item item)
    {
        Items.Add(item);
    }

    // Remove items
    public void Remove (Item item)
    {
        Items.Remove(item);
    }

 /*   // Learn Recipe
    public void Learn(CraftingRecipe recipe)
    {
        Recipes.Add(recipe);
    }

    // Make items
    public void Craft(CraftingRecipe recipe)
    {
        Items.Add(recipe.item);
    }
*/
}
