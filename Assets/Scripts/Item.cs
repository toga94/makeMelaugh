using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemType itemType = ItemType.Trap;
    public Tool tool = Tool.Signal;
}

public enum ItemType
{
    Trap,
    UsableTool
}

public enum Tool
{
    Signal,
    Slap
}