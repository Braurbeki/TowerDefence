using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{

    public bool isExplored = false, isPlaceable = true;
    public Waypoint ExploredFrom;

    [SerializeField] Color ExploredColor;

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

    void OnMouseOver()
    {
        if( Input.GetMouseButtonDown(0) && isPlaceable )
        {
            FindObjectOfType<TowerFactory>().addTower(this);
        }
        
    }
}
