using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour {

    // Values to be saved
    public int Gold = 14;
    string RobotName = "";

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
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

        // Level Difficulty


        // Write the object to the file and close it afterwards
        bf.Serialize(file, data);
        file.Close();
    }

    // Loading
    void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            // Create a binary formatter and open the save file
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);

            // Create an object to store information from the file in and then close the file
            PlayerData data = (PlayerData)bf.Deserialize(file);
            file.Close();

            Gold = data.GoldSave;
            RobotName = data.RobotNameSave;
            
        }
    }


    public void DeleteSave()
    {
        if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            File.Delete(Application.persistentDataPath + "/playerInfo.dat");
        }
    }
}
