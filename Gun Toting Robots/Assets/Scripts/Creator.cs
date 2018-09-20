using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Creator : MonoBehaviour {

    private GameObject GOAttachedToMouse = null;
    private GameObject[,] GridObjects;
    public Vector3 GridBottomLeftPos;
    public Text InputFieldRef;


    // Values to be saved
    public int Gold = 14;
    string RobotName = "";

    void Start ()
    {
        // Init Grid objects
        GridObjects = new GameObject[10, 10];
        for(int i = 0; i < 10; i++)
        {
            for(int j = 0; j < 10; j++)
            {
                // Spawn GO
                GridObjects[i, j] = new GameObject(i + "," + j);

                // Set GO position
                GridObjects[i, j].transform.position = new Vector3(GridBottomLeftPos.x + i, GridBottomLeftPos.y + j, 0f);
            }
        }
	}
	
	void Update ()
    {
        // Move Object attached to mouse
        if(GOAttachedToMouse != null)
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            GOAttachedToMouse.transform.position = new Vector3(pos.x, pos.y, 0f);
        }        

        // Input

        // Left Mouse Down
        if (Input.GetMouseButtonDown(0))
        {
            if(GOAttachedToMouse != null)
            {
                // Find closest grid position
                GOAttachedToMouse.transform.position = FindClosestGridPos(GOAttachedToMouse);

                // Assign GO to GridObjects[]
                AssignGridObject(GOAttachedToMouse);

                // Detach GO from mouse
                GOAttachedToMouse = null;
            }            
        }
	}

    public void PartSpawned(GameObject PartObject)
    {
        GOAttachedToMouse = PartObject;
    }

    Vector3 FindClosestGridPos(GameObject gameObjectToCheck)
    {
        Vector3 ClosestPos = new Vector3(0, 0, 0);
        float ClosestDistance = 10000000f;
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                float dist = Vector3.Distance(GridObjects[i, j].transform.position, gameObjectToCheck.transform.position);
                if(dist < ClosestDistance)
                {
                    ClosestPos = GridObjects[i, j].transform.position;
                    ClosestDistance = dist;
                }
            }
        }
        return ClosestPos;
    }

    void AssignGridObject(GameObject ObjectToBeAssigned)
    {
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                if(GridObjects[i, j].transform.position == ObjectToBeAssigned.transform.position)
                {
                    // Set object to gridobject array position (Overriding whatever was in there before)
                    Destroy(GridObjects[i, j]);
                    
                    GridObjects[i, j] = ObjectToBeAssigned;
                }
            }
        }
    }

    bool CheckGridObjectHasParts()
    {
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                if (GridObjects[i, j].tag == "Part")
                {
                    return true;
                }
            }
        }
        return false;
    }

    public void BattleButton()
    {
        // Set the robot name
        RobotName = InputFieldRef.text;

        // Save data
        Save();

        // Load battle scene
        SceneManager.LoadScene("Battle");

    }

    // Saving
    void Save()
    {
        // Create a binary formatter and a new file
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");

        // Create an object to save information to
        PlayerData data = new PlayerData();
        
        // Gold
        data.GoldSave = Gold;

        // Robot Name
        data.RobotNameSave = RobotName;

        // Robot composition
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                switch(GridObjects[i, j].name)
                {
                    case "Armour_MK1(Clone)":
                        data.RobotObjectsSave[i, j] = 1;
                        break;
                    case "Armour_MK2(Clone)":
                        data.RobotObjectsSave[i, j] = 2;
                        break;
                    case "Armour_MK3(Clone)":
                        data.RobotObjectsSave[i, j] = 3;
                        break;
                    case "Armour_MK4(Clone)":
                        data.RobotObjectsSave[i, j] = 4;
                        break;
                    case "PulseShot(Clone)":
                        data.RobotObjectsSave[i, j] = 5;
                        break;
                    case "CannonShot(Clone)":
                        data.RobotObjectsSave[i, j] = 6;
                        break;
                    case "SplitShot(Clone)":
                        data.RobotObjectsSave[i, j] = 7;
                        break;
                    case "MrWoofy(Clone)":
                        data.RobotObjectsSave[i, j] = 8;
                        break;
                    default:
                        data.RobotObjectsSave[i, j] = 0;
                        break;
                }
            }
        }

        // Write the object to the file and close it afterwards
        bf.Serialize(file, data);
        file.Close();
    }
    
    public void DeleteSave()
    {
        if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            File.Delete(Application.persistentDataPath + "/playerInfo.dat");
        }
    }
}

[System.Serializable]
class PlayerData
{
    public int[,] RobotObjectsSave; // 0 = empty, 1 = mk1, 2 = mk2, 3 = mk3, 4 = mk4, 5 = pulseshot, 6 = cannonshot, 7 = splitshot, 8 = mr.woofy
    public int GoldSave;
    public string RobotNameSave;
 

}
