﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Part : MonoBehaviour {

    public enum PartTypes {Armour, Gun};
    public PartTypes partType;
    public GameObject BulletPrefab;
    private GameObject PlayerRef;

    // Stats
    public float Health = 5f;
    internal float CurrentHealth;
    public float Attack = 1f;
    //public float SpeedBoost = 0f;
    public float ShootCooldown = 1f;
    public float BulletSpeed = 1f;
    float LastShootTime;

	void Start ()
    {
        // Init Values
        CurrentHealth = Health;
        LastShootTime = Time.time;
        PlayerRef = GameObject.FindGameObjectWithTag("Player");
	}

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Bullet")
        {
            // Lose Health
            TakeDamage(col.gameObject.GetComponent<Bullet>().Attack);
        }
    }

    void TakeDamage(float damageToTake)
    {
        CurrentHealth -= damageToTake;

        if (CurrentHealth <= 0)
        {
            // Message player script
            PlayerRef.GetComponent<Player>().RemovePart(gameObject);

            // Destroy part
            Destroy(gameObject);
        }
        
    }

    public void ShootGun()
    {
        if(Time.time > LastShootTime + ShootCooldown)
        {
            GameObject Bullet = Instantiate(BulletPrefab, transform.position, transform.rotation);

            // Change the z to 1
            Bullet.transform.position = new Vector3(transform.position.x, transform.position.y, 1f);
            Bullet.GetComponent<Bullet>().Attack = Attack;
            Bullet.GetComponent<Bullet>().Speed = BulletSpeed;

            // Update Last shoot time
            LastShootTime = Time.time;
        }
    }
}
