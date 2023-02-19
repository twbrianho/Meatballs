using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Recipe", menuName = "Recipe/Create New Recipe")]

public class Recipe : ScriptableObject
{
    public int id;
    public string name;
    public string description;
    public Sprite icon;


}
