using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    public static InventoryController Instance;
    public List<ItemManagement> Items = new List<ItemManagement>(); //A list of class ItemManagement which is Item,int
    public List<CraftingRecipe> Recipes = new List<CraftingRecipe>();

    public Transform ItemContent;
    public GameObject InventoryItem;

    public Transform AlwaysOnItemContent;
    public GameObject AlwaysOnInventoryItem;
    


    void Awake()
    {
        Instance = this;   

        
    }

    public ItemManagement Find(Item itemToFind) //Use to look through inventory for an item, returns null if not found
    {

        ItemManagement itemFound;
        try
        {
            itemFound = Items.Find((x) => x.Material.name == itemToFind.name); // Throws nullexceptions if not found! // There is a weird bug that throws null if ihere is an empty space BEFORE the item??
            //Debug.Log(itemToFind.id);
            //Debug.Log("found" + itemFound.Material.id);
        } 


        catch (NullReferenceException)
        {   
            
            return null;
            
        }
        //Debug.Log(itemFound);
        return itemFound;
    }

    // Add items
    public void AddToInventory(Item itemToAdd)
    {
        ItemManagement itemFound;
        try
        {
           itemFound = Find(itemToAdd);
           itemFound.Amount += 1;
        }

        catch(NullReferenceException)
        {
            Items.Add(new ItemManagement { Material = itemToAdd, Amount = 1 });
        }
            
    }

    // Remove items
    public void RemoveFromInventory(Item itemToRemove, int amountToRemove)
    {
        ItemManagement itemFound;
        try
        {
            itemFound = Find(itemToRemove);
            itemFound.Amount -= amountToRemove;
            if (itemFound.Amount <= 0)
            {
                Items.Remove(itemFound);
            }
        }

        catch (NullReferenceException)
        {
            Debug.Log("NULL ERROR!!!");//This should not arise if you code properly

        }
    }

    // Learn Recipe
/*    public void Learn(CraftingRecipe recipe)
    {
        Recipes.Add(recipe);
    }*/

    // Make items
    public void Craft(CraftingRecipe recipe)
    {
        List<ItemManagement> Cost = recipe.cost; //imports the recipe cost ItemManagement into variable name Cost here
        bool canAfford = true;
        foreach (var cost in Cost) //loops through the items in Cost and breaks if there is any insufficient
        {
            ItemManagement inventoryItem;
            try
            {
                inventoryItem = Find(cost.Material);
                if(inventoryItem.Amount < cost.Amount)
                {
                    canAfford = false;
                    break;
                }
            }

            catch(NullReferenceException)
            {
                canAfford = false;
                break;
            }

        }
        Debug.Log("Cannot afford!");

        if (canAfford) //if enough resources
        {
            foreach (var cost in Cost) //loops through the items in Cost and removes as you go along
            {
                RemoveFromInventory(cost.Material, cost.Amount); 
            }
            AddToInventory(recipe.product);
        }

    }


    public void ListItems()
    {
        foreach (var item in Items)
        {
            GameObject obj = Instantiate(InventoryItem, ItemContent);
            var itemName = obj.transform.Find("ItemName").GetComponent<Text>();
            var itemIcon = obj.transform.Find("Image").GetComponent<Image>();
            var itemAmount = obj.transform.Find("Amount").GetComponent<Text>();

            itemName.text = item.Material.name;
            itemIcon.sprite = item.Material.icon;
            itemAmount.text = item.Amount.ToString();
        }
    }


    public void UpdateAlwaysOnItems()
    {

        foreach (Transform child in AlwaysOnItemContent.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        foreach (var item in Items)//limited to 9 items
        {
            GameObject obj = Instantiate(AlwaysOnInventoryItem, AlwaysOnItemContent);
            //var itemName = obj.transform.Find("ItemName").GetComponent<Text>(); no name is shown
            var itemIcon = obj.transform.Find("Image").GetComponent<Image>();
            var itemAmount = obj.transform.Find("Amount").GetComponent<Text>();

            //itemName.text = item.Material.name;
            itemIcon.sprite = item.Material.icon;
            itemAmount.text = item.Amount.ToString();
        }
    }

}
