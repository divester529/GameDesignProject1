using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room
{
    public Vector2 gridPos;
    public bool t, l, r, b;

    public Room(Vector2 centerPos, string name)
    {
        gridPos = centerPos;
        if (name.Contains("T")) t = true;
        if (name.Contains("L")) l = true;
        if (name.Contains("R")) r = true;
        if (name.Contains("B")) b = true;
    }
}
