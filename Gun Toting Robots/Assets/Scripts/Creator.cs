using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creator : MonoBehaviour {

    private GameObject GOAttachedToMouse;
    private GameObject[] GridObjects;


	void Start ()
    {

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

        // Left Mouse Up
        if (Input.GetMouseButtonDown(0))
        {
            GOAttachedToMouse = null;
        }
	}

    public void PartSpawned(GameObject PartObject)
    {
        GOAttachedToMouse = PartObject;
    }
}
