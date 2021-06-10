using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] Transform objectToPan;
    [SerializeField] int shootingRange = 100;

    ParticleSystem.EmissionModule bulletsEmission;
    Transform targetEnemy;

    public Waypoint baseWaypoint;
    private void Start()
    {
        initBulletsEmission();
    }

    private void initBulletsEmission()
    {
        ParticleSystem bullets = gameObject.GetComponentInChildren<ParticleSystem>();
        bulletsEmission = bullets.emission;
        bulletsEmission.enabled = false;
    }

    void Update()
    {
        setTargetEnemy();
        if(targetEnemy)
        {
            CheckForDistance();
        }
        else
        {
            bulletsEmission.enabled = false;
        }
    }

    private void CheckForDistance()
    {
        var curDist = Vector3.Distance(targetEnemy.position, objectToPan.position);
        if(curDist < shootingRange)
        {
            FireAtEnemy();
        }
        else
        {
            bulletsEmission.enabled = false;
        }
    }

    private void FireAtEnemy()
    {
        RotateTower();
        bulletsEmission.enabled = true;
    }

    private void RotateTower()
    {
        objectToPan.LookAt(targetEnemy);
    }

    public void setTargetEnemy()
    {
        var sceneEnemies = FindObjectsOfType<EnemyDamage>();
        if(sceneEnemies.Length == 0) { return; }

        Transform closestEnemy = sceneEnemies[0].transform;

        foreach(EnemyDamage testEnemy in sceneEnemies)
        {
            closestEnemy = GetClosest(closestEnemy, testEnemy.transform);
        }

        targetEnemy = closestEnemy;
    }

    private Transform GetClosest(Transform closestEnemy, Transform testEnemy)
    {
        float closestEnemyDist = Vector3.Distance(closestEnemy.position, objectToPan.position);
        float testEnemyDist = Vector3.Distance(testEnemy.position, objectToPan.position);
        if ( testEnemyDist < closestEnemyDist )
        {
            return testEnemy;
        }
        
        return closestEnemy;

    }
}
