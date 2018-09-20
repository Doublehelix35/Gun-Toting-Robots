using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    float Speed = 100f;
    private GameObject PlayerRef;

	void Start ()
    {
        PlayerRef = GameObject.FindGameObjectWithTag("Player");
	}

	void Update ()
    {
        transform.position = PlayerRef.transform.position;
	}
}
