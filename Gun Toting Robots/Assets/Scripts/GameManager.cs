using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public GameObject PlayerRef;
    List<GameObject> EnemyList = new List<GameObject>();
    public Text GoldUI;
    public Text NameUI;
    public Text HealthUI;
    public Text EnemyUI;

    // Values to be saved
    public int Gold = 14;
    string RobotName = "";

    void Awake ()
    {
        // Load data
        Load();

        // Update ui text
        GoldUI.text = "" + Gold;
        NameUI.text = RobotName;
        EnemyUI.text = "" + 0;

    }
	
	void Update ()
    {
		
	}

    public bool UpdateGold(int goldValueChange)
    {
        Gold += goldValueChange;

        GoldUI.text = "" + Gold;

        if (Gold < 0) // Reverese and throw error
        {
            Gold -= goldValueChange;

            GoldUI.text = "" + Gold;
            return false;
        }
        else
        {
            return true;
        }
    }

    public void GameOver()
    {
        // Save
        Save();

        // Load main menu
        SceneManager.LoadScene("MainMenu");
    }

    public void UpdateHealthUI(string newUIText)
    {
        HealthUI.text = newUIText;
    }

    public void AddEnemyToList(GameObject EnemyToAdd)
    {
        if (!EnemyList.Contains(EnemyToAdd))
        {
            EnemyList.Add(EnemyToAdd);
            EnemyUI.text = EnemyList.Count.ToString();
        }
    }

    public void RemoveEnemyFromList(GameObject EnemyToRemove)
    {
        if (EnemyList.Contains(EnemyToRemove))
        {
            EnemyList.Remove(EnemyToRemove);
            EnemyUI.text = EnemyList.Count.ToString();

            // Add gold
            UpdateGold(EnemyToRemove.GetComponent<Enemy>().GoldValue);
        }
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

            // Load gold
            Gold = data.GoldSave;

            // Load robot name
            RobotName = data.RobotNameSave;

            // Load robot composition
            PlayerRef.GetComponent<Player>().SetRobotComposition(data.RobotObjectsSave);
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
