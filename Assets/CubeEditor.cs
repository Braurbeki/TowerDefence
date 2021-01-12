using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[SelectionBase]
[RequireComponent(typeof(Waypoint))]
public class CubeEditor : MonoBehaviour
{
    Waypoint waypoint;
    private void Awake()
    {
        waypoint = GetComponent<Waypoint>();
    }
    void Update()
    {
        SnapToGrid();
        UpdateLabel();
    }

    private void SnapToGrid()
    {
        int gridSize = waypoint.getGridSize();
        transform.position = new Vector3(
            waypoint.getGridPos().x * gridSize, 
            0f, 
            waypoint.getGridPos().y * gridSize
        );
    }

    private void UpdateLabel()
    {
        int gridSize = waypoint.getGridSize();
        TextMesh textMesh = GetComponentInChildren<TextMesh>();
        textMesh.text = waypoint.getGridPos().x  + ";" + waypoint.getGridPos().y;
        gameObject.name = textMesh.text;
    }


}
