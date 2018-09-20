using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    GameObject GameManagerRef;
    GameObject[,] PartObjects;
    float PartStartOffset = -4.5f; // Bottom left of grid of parts
    int[,] RobotComposition; // Formation of robot

    // Prefabs
    public GameObject Armour_MK1Prefab;
    public GameObject Armour_MK2Prefab;
    public GameObject Armour_MK3Prefab;
    public GameObject Armour_MK4Prefab;
    public GameObject PulseShotPrefab;
    public GameObject CannonShotPrefab;
    public GameObject SplitShotPrefab;
    public GameObject MrWoofyPrefab;

    // Stats
    public float Speed = 1f;
    float BaseSpeed = 100f;
    float TurnSpeed = 1f;
    float MaxHealth;
    float CurrentHealth;

    // Upgrade stats


	void Start ()
    {
        GameManagerRef = GameObject.FindGameObjectWithTag("GameController");
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

        // Face mouse cursor
        Vector3 direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
        direction.z = 0;
        transform.up = direction;

        // Shoot - Left Mouse
        if (Input.GetKey(KeyCode.Mouse0))
        {
            FireGuns();
        }

    }
    void FixedUpdate()
    {
        // Update current health
        if(PartObjects != null)
        {
            UpdateCurrentHealth();
        }        
    }

    void FireGuns()
    {
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                if (PartObjects[i, j].tag == "Part")
                {
                    if (PartObjects[i, j].GetComponent<Part>().partType == Part.PartTypes.Gun)
                    {
                        PartObjects[i, j].GetComponent<Part>().ShootGun();
                    }
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
                if (PartObjects[i, j].tag == "Part")
                {
                    if (PartObjects[i, j].GetComponent<Part>().partType == Part.PartTypes.Armour)
                    {
                        currentHealth += PartObjects[i, j].GetComponent<Part>().CurrentHealth;
                    }
                }
            }
        }
        CurrentHealth = currentHealth;
        GameManagerRef.GetComponent<GameManager>().UpdateHealthUI(Mathf.Floor(CurrentHealth) + " / " + MaxHealth);

        // Check if player is dead
        if(CurrentHealth <= 0)
        {
            GameManagerRef.GetComponent<GameManager>().GameOver();
        }
    }

    float CalculateMaxHealth()
    {
        float totalHealth = 0f;
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                if (PartObjects[i, j].tag == "Part")
                {
                    if (PartObjects[i, j].GetComponent<Part>().partType == Part.PartTypes.Armour)
                    {
                        totalHealth += PartObjects[i, j].GetComponent<Part>().Health;
                    }
                }
            }
        }
        return totalHealth;
    }


    public void SetRobotComposition(int[,] NewComposition)
    {
        RobotComposition = new int[10, 10];
        RobotComposition = NewComposition;

        // Init part objects
        PartObjects = new GameObject[10, 10];
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                switch (RobotComposition[i, j])
                {
                    case 0:
                        // Spawn Empty GO
                        PartObjects[i, j] = new GameObject(i + "," + j);
                        break;
                    case 1:
                        // Spawn Armour MK1
                        GameObject MK1 = Instantiate(Armour_MK1Prefab, transform.position, transform.rotation);
                        PartObjects[i, j] = MK1;
                        break;
                    case 2:
                        // Spawn Armour MK2
                        GameObject MK2 = Instantiate(Armour_MK2Prefab, transform.position, transform.rotation);
                        PartObjects[i, j] = MK2;
                        break;
                    case 3:
                        // Spawn Armour MK3
                        GameObject MK3 = Instantiate(Armour_MK3Prefab, transform.position, transform.rotation);
                        PartObjects[i, j] = MK3;
                        break;
                    case 4:
                        // Spawn Armour MK4
                        GameObject MK4 = Instantiate(Armour_MK4Prefab, transform.position, transform.rotation);
                        PartObjects[i, j] = MK4;
                        break;
                    case 5:
                        // Spawn PulseShot
                        GameObject PS = Instantiate(PulseShotPrefab, transform.position, transform.rotation);
                        PartObjects[i, j] = PS;
                        break;
                    case 6:
                        // Spawn CannonShot
                        GameObject CS = Instantiate(CannonShotPrefab, transform.position, transform.rotation);
                        PartObjects[i, j] = CS;
                        break;
                    case 7:
                        // Spawn SplitShot
                        GameObject SS = Instantiate(SplitShotPrefab, transform.position, transform.rotation);
                        PartObjects[i, j] = SS;
                        break;
                    case 8:
                        // Spawn Mr Woofy
                        GameObject MW = Instantiate(MrWoofyPrefab, transform.position, transform.rotation);
                        PartObjects[i, j] = MW;
                        break;
                    default:
                        // Spawn Empty GO
                        PartObjects[i, j] = new GameObject(i + "," + j);
                        break;
                }


                // Set GO position
                PartObjects[i, j].transform.position = new Vector3(transform.position.x + PartStartOffset + i, transform.position.y + PartStartOffset + j, 0f);
                PartObjects[i, j].transform.parent = transform;
            }
        }

        // Get max health and init current health
        MaxHealth = CalculateMaxHealth();
        CurrentHealth = MaxHealth;
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

