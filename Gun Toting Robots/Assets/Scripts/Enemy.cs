using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public GameObject BulletPrefab;

    // Stats
    public float Health = 5f;
    float CurrentHealth = 5f;
    public float Attack = 1f;
    public float Speed = 1f;
    float BaseSpeed = 100f;
    public float ShootCooldown;
    public float BulletSpeed;

    float LastShootTime;

    GameObject PlayerRef;

	void Start ()
    {
        // Modify stats with gausian randomness
        GenerateStats();

        // Init Values
        CurrentHealth = Health;
        LastShootTime = Time.time;
        PlayerRef = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void GenerateStats()
    {
        Health = GausianModifier(Health);
        Attack = GausianModifier(Attack);
        Speed = GausianModifier(Speed);
        ShootCooldown = GausianModifier(ShootCooldown);
        BulletSpeed = GausianModifier(BulletSpeed);
    }

    float GausianModifier(float ValueToModify)
    {
        float NewValue = 0f;
        float randTotal = 0f;

        // 10 random numbers from a range of 0.1 to 3
        for(int i = 0; i < 10; i++)
        {
            float rand = Random.Range(0.1f, 3f);
            randTotal += rand;
        }

        randTotal = randTotal / 10;

        NewValue = ValueToModify * randTotal;

        return NewValue;
    }
}
