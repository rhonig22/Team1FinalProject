using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class UnlockRequirement
{
    public RequirementType RequirementType;
    public int MinValue;
    public string[] Recipes;
}


public enum RequirementType
{
    Stars,
    Score
}