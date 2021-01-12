using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    Dictionary<Vector2Int, Waypoint> grid = new Dictionary<Vector2Int, Waypoint>();
    Queue<Waypoint> queue = new Queue<Waypoint>();
    bool isRunning = true;
    Vector2Int[] directions =
    {
        Vector2Int.up,
        Vector2Int.down,
        Vector2Int.left,
        Vector2Int.right
    };

    [SerializeField] Waypoint startWaypoint;
    [SerializeField] Waypoint finishWaypoint;
    void Start()
    {
        LoadBlocks();
        paintStartAndFinish();
        PathFind();
        //ExploreNeighbours();
    }

    private void PathFind()
    {
        queue.Enqueue(startWaypoint);
        while(queue.Count > 0 && isRunning)
        {
            var searchCenter= queue.Dequeue();
            HaltIfEndPoint(searchCenter);
        }
    }

    private void HaltIfEndPoint(Waypoint searchCenter)
    {
        if(searchCenter == finishWaypoint)
        {
            isRunning = false;
        }
    }

    private void paintStartAndFinish()
    {
        startWaypoint.setTopColor(Color.green);
        finishWaypoint.setTopColor(Color.red);
    }

    private void ExploreNeighbours()
    {
        foreach(Vector2Int direction in directions)
        {
            var explorationCoords = startWaypoint.getGridPos() + direction;
            try
            {
                grid[explorationCoords].setTopColor(Color.blue);
            } catch { }
            
        }
    }

    private void LoadBlocks()
    {
        var waypoints = FindObjectsOfType<Waypoint>();
        foreach(Waypoint waypoint in waypoints)
        {
            if(grid.ContainsKey(waypoint.getGridPos()))
            {
                Debug.LogWarning("Skipping overlapping block: " + waypoint);
            }
            else
            {
                grid.Add(waypoint.getGridPos(), waypoint);
            }
        }
    }
}
