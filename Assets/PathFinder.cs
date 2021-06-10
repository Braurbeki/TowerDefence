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
    [SerializeField] List<Waypoint> path = new List<Waypoint>();

    Waypoint searchCenter;
    bool isCalled = false;
    public List<Waypoint> getPath()
    {
        if(!isCalled)
        {
            CalculatePath();
            isCalled = true;
        }
        return path;
    }

    private void CalculatePath()
    {
        LoadBlocks();
        BreadthFirstSearch();
        CreatePath();
    }

    private void CreatePath()
    {
        setAsPath(finishWaypoint);
        Waypoint previous = finishWaypoint.ExploredFrom;
        while(previous != startWaypoint)
        {
            setAsPath(previous);
            previous = previous.ExploredFrom;
        }
        setAsPath(startWaypoint);
        path.Reverse();
    }

    private void setAsPath(Waypoint waypoint)
    {
        path.Add(waypoint);
        waypoint.isPlaceable = false;
    }

    private void BreadthFirstSearch()
    {
        queue.Enqueue(startWaypoint);
        startWaypoint.isExplored = true;
        while(queue.Count > 0 && isRunning)
        {
            searchCenter = queue.Dequeue();
            searchCenter.isExplored = true;
            HaltIfEndPoint();
            ExploreNeighbours();
        }
    }

    private void HaltIfEndPoint()
    {
        if(searchCenter == finishWaypoint)
        {
            isRunning = false;
        }
    }

    private void ExploreNeighbours()
    {
        if(!isRunning) { return; }
        foreach(Vector2Int direction in directions)
        {
            var explorationCoords = searchCenter.getGridPos() + direction;
            if(grid.ContainsKey(explorationCoords))
            {
                if (grid[explorationCoords].isExplored || queue.Contains(grid[explorationCoords]))
                {
                    
                } else
                {
                    QueueNewNeighbours(explorationCoords);
                }
            }
            
        }
    }

    private void QueueNewNeighbours(Vector2Int explorationCoords)
    {
        Waypoint neighbour = grid[explorationCoords];
        queue.Enqueue(neighbour);
        neighbour.ExploredFrom = searchCenter;
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

    public Waypoint getFinishWaypoint()
    {
        return finishWaypoint;
    }
}
