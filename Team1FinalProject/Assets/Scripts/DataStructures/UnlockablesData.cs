using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Unlockable
{
    public bool Unlocked;
    public string Name;
    public float UnlockedTime;
}

[Serializable]
public class UnlockablesData
{
    public List<Unlockable> UnlockablesList;
}