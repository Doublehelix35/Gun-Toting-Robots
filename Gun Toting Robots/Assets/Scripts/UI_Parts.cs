using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Parts : MonoBehaviour {

    public GameObject PrefabToSpawn;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Mouse0)) // Left mouse click
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 MousePos = new Vector2(pos.x, pos.y);

            var Click = Physics2D.OverlapPoint(MousePos);

            if (Click && Click.transform == transform) // If clicked then show game object
            {
                Instantiate(PrefabToSpawn, new Vector3(MousePos.x, MousePos.y, 0), Quaternion.identity);
            }
        }
    }

}
