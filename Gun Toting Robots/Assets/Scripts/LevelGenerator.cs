using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {

    // Prefabs
    public GameObject MeowzerPrefab;

    public float SpawnRange = 20f;
    public float SpawnRangeIncrement = 5f;
    public int SpawnCount = 2;
    public int SpawnCountIncrement = 2;
    public float StatsIncrement = 1.1f;

    void Awake ()
    {
        SpawnEnemies();
	}

    public void SpawnEnemies()
    {
        float randX = Random.Range(-SpawnRange, SpawnRange);   
        float randY = Random.Range(-SpawnRange, SpawnRange);

        for(int i = 0; i < SpawnCount; i++)
        {
            GameObject Enemy = Instantiate(MeowzerPrefab, new Vector3(randX, randY, 0f), Quaternion.identity);
            Enemy.GetComponent<Enemy>().EnemyStatsIncrement = StatsIncrement;
        }

        // Increase Spawn Range
        SpawnRange += SpawnRangeIncrement;

        // Increase Spawn Count
        SpawnCount += SpawnCountIncrement;

        // Increase Stats Increment
        StatsIncrement += 0.1f;
    }    
}
