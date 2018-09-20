using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    GameObject[,] PartObjects;
    float PartStartOffset = -4.5f; // Bottom left of grid of parts

    // Stats
    public float Speed = 1f;
    float BaseSpeed = 100f;
    float TurnSpeed = 1f;
    float MaxHealth;
    float CurrentHealth;

    // Upgrade stats


	void Start ()
    {
        // Init part objects
		PartObjects = new GameObject[10, 10];
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                // Spawn GO
                PartObjects[i, j] = new GameObject(i + "," + j);

                // Set GO position
                PartObjects[i, j].transform.position = new Vector3(transform.position.x + PartStartOffset + i, transform.position.y + PartStartOffset + j, 0f);
                PartObjects[i, j].transform.parent = transform;
            }
        }

        // Get max health and init current health
        MaxHealth = CalculateMaxHealth();
        CurrentHealth = MaxHealth;
    }
	
	void Update ()
    {
        // Input

        // Left or A
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            GetComponent<Rigidbody2D>().AddForce(-transform.right * Speed * BaseSpeed * Time.deltaTime);
        }
        // Right or D
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            GetComponent<Rigidbody2D>().AddForce(transform.right * Speed * BaseSpeed * Time.deltaTime);
        }

        // Up or W
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            GetComponent<Rigidbody2D>().AddForce(transform.up * Speed * BaseSpeed * Time.deltaTime);
        }
        // Down or S
        else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            GetComponent<Rigidbody2D>().AddForce(-transform.up * Speed * BaseSpeed * Time.deltaTime);
        }
    }
    void FixedUpdate()
    {
        // Update current health
        UpdateCurrentHealth();
    }

    void FireGuns()
    {
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                if (PartObjects[i, j] == null || PartObjects[i, j].tag != "Part") { break; } // Break if no part found
                if (PartObjects[i, j].GetComponent<Part>().partType == Part.PartTypes.Gun)
                {
                    PartObjects[i, j].GetComponent<Part>().ShootGun();
                }
            }
        }
    }

    public void UpdateCurrentHealth()
    {
        float currentHealth = 0;
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                if(PartObjects[i, j] == null || PartObjects[i, j].tag != "Part") { break; } // Break if no part found
                if (PartObjects[i, j].GetComponent<Part>().partType == Part.PartTypes.Armour)
                {
                    currentHealth += PartObjects[i, j].GetComponent<Part>().CurrentHealth;
                }
            }
        }
        CurrentHealth = currentHealth;
    }

    float CalculateMaxHealth()
    {
        float totalHealth = 0f;
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                if (PartObjects[i, j] == null || PartObjects[i, j].tag != "Part") { break; } // Break if no part found
                if (PartObjects[i, j].GetComponent<Part>().partType == Part.PartTypes.Armour)
                {
                    totalHealth += PartObjects[i, j].GetComponent<Part>().Health;
                }
            }
        }
        return totalHealth;
    }


    public void AddPart(GameObject partToAdd)
    {

    }

    public void RemovePart(GameObject partToRemove)
    {
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                if(PartObjects[i, j] == partToRemove)
                {
                    // Override position with empty game object
                    PartObjects[i, j] = new GameObject(i + "," + j);

                    // Set GO position
                    PartObjects[i, j].transform.position = new Vector3(transform.position.x + PartStartOffset + i, transform.position.y + PartStartOffset + j, 0f);
                    PartObjects[i, j].transform.parent = transform;
                }
            }
        }
    }
}
