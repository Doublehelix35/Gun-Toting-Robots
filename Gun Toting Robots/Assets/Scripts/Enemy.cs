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
    float BaseSpeed = 50f;
    public float ShootCooldown = 1f;
    public float BulletSpeed = 1f;
    public float Range = 20f;

    float LastShootTime;

    GameObject PlayerRef;
    GameObject GameManagerRef;

	void Start ()
    {
        // Modify stats with gausian randomness
        GenerateStats();

        // Init Values
        CurrentHealth = Health;
        LastShootTime = Time.time;
        PlayerRef = GameObject.FindGameObjectWithTag("Player");
        GameManagerRef = GameObject.FindGameObjectWithTag("GameController");

        // Add this gameobject to enemy list
        GameManagerRef.GetComponent<GameManager>().AddEnemyToList(gameObject);
    }
    
    void Update ()
    {
        // Face player
        Vector3 direction = (PlayerRef.transform.position - transform.position).normalized;
        direction.z = 0;
        transform.up = direction;

        // Move forward
        float dist = Vector3.Distance(PlayerRef.transform.position, transform.position);
        if (dist > 10f)
        {
            GetComponent<Rigidbody2D>().AddForce(transform.up * Speed * BaseSpeed * Time.deltaTime);
        }

        // Shoot
        if(dist < Range) // Shoot if in range
        {
            if (Time.time > LastShootTime + ShootCooldown)
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
            // Remove this gameobject from enemy list
            GameManagerRef.GetComponent<GameManager>().RemoveEnemyFromList(gameObject);

            // Destroy enemy
            Destroy(gameObject);
        }

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
