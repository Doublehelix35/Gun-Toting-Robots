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
        transform.position = new Vector3(PlayerRef.transform.position.x, PlayerRef.transform.position.y, transform.position.z);
	}
}
