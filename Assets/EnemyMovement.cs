using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    [SerializeField] float movementPeriod = .5f;
    [SerializeField] ParticleSystem deathParticlePrefab;

    PathFinder pathFinder;
    EnemySpawning enemySpawning;


    private void Start()
    {
        enemySpawning = FindObjectOfType<EnemySpawning>();
        pathFinder = FindObjectOfType<PathFinder>();
        var path = pathFinder.getPath();
        StartCoroutine(FollowPath(path));
    }

    IEnumerator FollowPath(List<Waypoint> path)
    {
        print("Starting patrol...");
        foreach (Waypoint block in path)
        {
            transform.position = new Vector3(block.transform.position.x, 5f, block.transform.position.z);
            yield return new WaitForSeconds(movementPeriod);
        }
        DestroyOnEnd();
    }

    private void DestroyOnEnd()
    {
        var vfx = Instantiate(deathParticlePrefab, gameObject.transform.position, Quaternion.identity);
        vfx.Play();
        float destroyDelay = deathParticlePrefab.main.duration;
        enemySpawning.enemies.Remove(gameObject);
        Destroy(vfx.gameObject, destroyDelay);
        Destroy(gameObject);

    }
}
