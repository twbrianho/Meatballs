using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class CraftingResource
{
    public GameObject Material;
    public int Amount = 0;

    public CraftingResource(GameObject material, int amount)
    {
        this.Material = material;
        this.Amount = amount;
    }
}