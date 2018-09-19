using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public float Attack = 1f;
    public float Speed = 1f;
    float BaseSpeed = 20f;
    float LifeSpan = 5f;

	void Start ()
    {
        Destroy(gameObject, LifeSpan);
	}

	void Update ()
    {
        transform.Translate(Vector3.forward * Speed * BaseSpeed * Time.deltaTime);
	}
}
