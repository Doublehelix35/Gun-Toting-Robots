using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public float Attack = 1f;
    public float Speed = 1f;
    float BaseSpeed = 1500f;
    float LifeSpan = 3f;

	void Start ()
    {
        Destroy(gameObject, LifeSpan);
	}

	void Update ()
    {
        GetComponent<Rigidbody2D>().AddForce(transform.up * Speed * BaseSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        Destroy(gameObject, 0.0001f);
    }
}
