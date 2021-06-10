using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerFactory : MonoBehaviour
{
    [SerializeField] Tower towerPrefab;
    [SerializeField] int towerLimit = 5;
    [SerializeField] GameObject parentTower;
    Queue<Tower> towersQueue = new Queue<Tower>();
    

    public void addTower(Waypoint baseWaypoint)
    {
        int numTowers = towersQueue.Count;
        if(numTowers < towerLimit)
        {
            InstantiateTower(baseWaypoint);
        }
        else
        {
            MoveExistingTower(baseWaypoint);
        }
    }

    private void MoveExistingTower(Waypoint newBaseWaypoint)
    {
        var oldTower = towersQueue.Dequeue();

        oldTower.baseWaypoint.isPlaceable = true;

        oldTower.baseWaypoint = newBaseWaypoint;

        oldTower.transform.position = newBaseWaypoint.transform.position;

        newBaseWaypoint.isPlaceable = false;

        towersQueue.Enqueue(oldTower);
    }

    private void InstantiateTower(Waypoint baseWaypoint)
    {
        var newTower = Instantiate(towerPrefab, baseWaypoint.transform.position, Quaternion.identity);
        newTower.transform.parent = parentTower.transform;
        newTower.baseWaypoint = baseWaypoint;
        baseWaypoint.isPlaceable = false;
        towersQueue.Enqueue(newTower);
    }
}
