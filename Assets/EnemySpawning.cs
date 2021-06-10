using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawning : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] float secondsBetweenSpawns = 4f;
    [SerializeField] Text scoreText;

    [SerializeField] GameObject enemyParent;

    public List<GameObject> enemies = new List<GameObject>();

    private void Start()
    {
        scoreText.text = enemies.Count.ToString();
        StartCoroutine(spawnEnemy());
    }
    private void Update()
    {
        scoreText.text = enemies.Count.ToString();
    }

    IEnumerator spawnEnemy()
    {
        while (true)
        {
            GameObject curEnemy = Instantiate(enemyPrefab, new Vector3(0f, 5f, 0f), Quaternion.identity);
            GetComponent<AudioSource>().Play();
            enemies.Add(curEnemy);
            curEnemy.transform.parent = enemyParent.transform;
            yield return new WaitForSeconds(secondsBetweenSpawns);
        }
    }
}
