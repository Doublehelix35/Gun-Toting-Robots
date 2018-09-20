using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    int Gold = 0;
    public int MinGold = 30;

    public Text GoldUI;

    // Moving object refs
    public GameObject[] ObjectsToMove;
    float SizeToScale = 0.2f;

    // Rotating object refs
    public GameObject[] ObjectsToRotate;
    float RotateRange = 10f;
    float RotateOffset = 5f;

    void Start ()
    {
        Load();

        // Update gold ui text
        GoldUI.text = "" + Gold;
    }
	
	void Update ()
    {
		for(int i = 0; i < ObjectsToMove.Length; i++)
        {
            Vector3 ScaledVec = new Vector3(Mathf.PingPong(Time.time / 6, SizeToScale) + 1f,
                                        Mathf.PingPong(Time.time / 6, SizeToScale) + 1f,
                                        0f);
            ObjectsToMove[i].transform.localScale = ScaledVec;
        }

        for (int i = 0; i < ObjectsToRotate.Length; i++)
        {
            if(i % 2 == 0)
            {
                ObjectsToRotate[i].transform.localEulerAngles = new Vector3(0, 0, Mathf.PingPong(Time.time * 15, RotateRange) - RotateOffset);
            }
            else
            {
                ObjectsToRotate[i].transform.localEulerAngles = new Vector3(0, 0, -Mathf.PingPong(Time.time * 15, RotateRange) + RotateOffset);
            }            
        }
    }

    public void PlayButton()
    {
        SceneManager.LoadScene("Creator");
    }

    public void CreditsButton()
    {
        SceneManager.LoadScene("Credits");
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ResetSave()
    {
        // Override save
        Save();

        // Load reset save
        Load();

        // Update gold ui text
        GoldUI.text = "" + Gold;
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
        data.GoldSave = MinGold;

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

            // Check Player has more than minimum gold
            if (Gold < MinGold)
            {
                Gold = MinGold;
            }
        }
    }
}
