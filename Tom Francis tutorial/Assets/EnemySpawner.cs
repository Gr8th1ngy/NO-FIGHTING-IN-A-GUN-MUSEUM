using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject spawnPoint;
    public float secondsBetweenSpawn;
    public int enemiesToSpawn;

    float secondsSinceLastSpawn;

    private void OnEnable()
    {
        References.enemySpawners.Add(this);
    }

    private void OnDisable()
    {
        References.enemySpawners.Remove(this);
    }

    private void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        secondsSinceLastSpawn = 0;
    }

    // Happens the same number of times for all players. Good place for gameplay critical things.
    private void FixedUpdate()
    {
        if (References.levelManager.alarmSounded && enemiesToSpawn > 0)
        {
            secondsSinceLastSpawn += Time.fixedDeltaTime;
            if (secondsSinceLastSpawn >= secondsBetweenSpawn)
            {
                Instantiate(enemyPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
                secondsSinceLastSpawn = 0;
                enemiesToSpawn--;
            }
        }
    }
}
