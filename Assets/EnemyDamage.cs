using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] GameObject enemyObject;
    [SerializeField] int hitPoints = 10;
    [SerializeField] ParticleSystem hitParticlePrefab;
    [SerializeField] ParticleSystem deathParticlePrefab;

    EnemySpawning enemySpawning;
    private void Start()
    {
        addBoxCollider();
        addRigidBody();
        enemySpawning = FindObjectOfType<EnemySpawning>();
    }

    private void addBoxCollider()
    {
        enemyObject.AddComponent<BoxCollider>();
    }

    private void addRigidBody()
    {
        Rigidbody rigidbody = gameObject.AddComponent<Rigidbody>();
        rigidbody.isKinematic = true;
    }

    private void OnParticleCollision(GameObject other)
    {
        GetComponent<AudioSource>().Play();
        hitPoints--;
        hitParticlePrefab.Play();
        if(hitPoints <= 0)
        {
            KillEnemy();
        }

    }

    private void KillEnemy()
    {
        var vfx = Instantiate(deathParticlePrefab, gameObject.transform.position, Quaternion.identity);
        vfx.Play();
        float destroyDelay = deathParticlePrefab.main.duration;
        enemySpawning.enemies.Remove(gameObject);
        Destroy(vfx.gameObject, destroyDelay);
        Destroy(gameObject);
    }
}
