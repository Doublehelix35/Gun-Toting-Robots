using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour {

    float TimeStart;
    public float TimeToWait = 1f;

    void Start()
    {
        TimeStart = Time.time;
    }

    void Update ()
    {
        if (TimeStart + TimeToWait < Time.time)
        {
            // Load main menu
            SceneManager.LoadScene("MainMenu");
        }
	}
}
