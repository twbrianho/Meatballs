using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Crafting Recipe", menuName = "Scriptable Objects/Create New Recipe")]

public class CraftingRecipe : ScriptableObject
{
    public int id;
    public string name;
    public string description;
    public Sprite icon;
    public List<CraftingResource> cost;
    public GameObject productPrefab;
}
