using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] List<Waypoints> path;

    private void Start()
    {
        StartCoroutine(FollowPath());
    }

    IEnumerator FollowPath()
    {
        print("Starting patrol...");
        foreach (Waypoints block in path)
        {
            print("Visiting block " + block.name + "...");
            transform.position = block.transform.position;
            yield return new WaitForSeconds(1f);
        }
        print("Ending patrol...");
    }
}
