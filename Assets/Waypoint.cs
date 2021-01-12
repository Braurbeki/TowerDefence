﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{

    Vector2Int gridPos;
    const int gridSize = 10;

    public Vector2Int getGridPos()
    {
        return new Vector2Int(
            Mathf.RoundToInt(transform.position.x / gridSize),
            Mathf.RoundToInt(transform.position.z / gridSize)
            );
    }
   
    public int getGridSize()
    {
        return gridSize;
    }

    public void setTopColor(Color color)
    {
        MeshRenderer topRender = transform.Find("Top").GetComponent<MeshRenderer>();
        topRender.material.color = color;
    }
}