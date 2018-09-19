using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Part : MonoBehaviour {

    public enum PartTypes {Armour, Gun};
    public PartTypes partType;
    public GameObject BulletPrefab;

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
	}

	void Update ()
    {
		
	}

    private void OnCollisionEnter2D(Collision2D col)
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

        if(CurrentHealth <= 0)
        {
            // Message player script

            // Destroy part
            Destroy(gameObject);
        }
    }

    public void ShootGun()
    {
        if(Time.time > LastShootTime + ShootCooldown)
        {
            GameObject Bullet = Instantiate(BulletPrefab, transform.position, Quaternion.identity);

            Bullet.GetComponent<Bullet>().Attack = Attack;
            Bullet.GetComponent<Bullet>().Speed = BulletSpeed;

            // Update Last shoot time
            LastShootTime = Time.time;
        }
    }
}
