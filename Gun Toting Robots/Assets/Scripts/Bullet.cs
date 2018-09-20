using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public float Attack = 1f;
    public float Speed = 1f;
    float BaseSpeed = 2000f;
    float LifeSpan = 5f;

	void Start ()
    {
        Destroy(gameObject, LifeSpan);
	}

	void Update ()
    {
        GetComponent<Rigidbody2D>().AddForce(transform.up * Speed * BaseSpeed * Time.deltaTime);
    }
}
