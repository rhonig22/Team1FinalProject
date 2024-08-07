using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class RecipeEntry
{
    public bool Unlocked;
    public int HighScore;
    public int Stars;
    public string Name;
}

[Serializable]
public class RecipeData
{
    public List<RecipeEntry> RecipeList;
}