using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {

    // Prefabs
    public GameObject MeowzerPrefab;

    public float SpawnRange = 20f;
    public int SpawnCount = 2;

	void Awake ()
    {
        SpawnEnemies(SpawnCount);
	}
	
	void Update ()
    {
		
	}

    void SpawnEnemies(int NumberToSpawn)
    {
        float randX = Random.Range(-SpawnRange, SpawnRange);   
        float randY = Random.Range(-SpawnRange, SpawnRange);

        for(int i = 0; i < NumberToSpawn; i++)
        {
            Instantiate(MeowzerPrefab, new Vector3(randX, randY, 0f), Quaternion.identity);
        }        
    }
}
