using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creator : MonoBehaviour {

    private GameObject GOAttachedToMouse = null;
    private GameObject[,] GridObjects;
    public Vector3 GridBottomLeftPos;
    public int Gold = 14;


	void Start ()
    {
        // Init Grid objects
        GridObjects = new GameObject[10, 10];
        for(int i = 0; i < 10; i++)
        {
            for(int j = 0; j < 10; j++)
            {
                // Spawn GO
                GridObjects[i, j] = new GameObject(i + "," + j);

                // Set GO position
                GridObjects[i, j].transform.position = new Vector3(GridBottomLeftPos.x + i, GridBottomLeftPos.y + j, 0f);
            }
        }
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

        // Left Mouse Down
        if (Input.GetMouseButtonDown(0))
        {
            if(GOAttachedToMouse != null)
            {
                // Find closest grid position
                GOAttachedToMouse.transform.position = FindClosestGridPos(GOAttachedToMouse);

                // Assign GO to GridObjects[]
                AssignGridObject(GOAttachedToMouse);

                // Detach GO from mouse
                GOAttachedToMouse = null;
            }            
        }
	}

    public void PartSpawned(GameObject PartObject)
    {
        GOAttachedToMouse = PartObject;
    }

    Vector3 FindClosestGridPos(GameObject gameObjectToCheck)
    {
        Vector3 ClosestPos = new Vector3(0, 0, 0);
        float ClosestDistance = 10000000f;
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                float dist = Vector3.Distance(GridObjects[i, j].transform.position, gameObjectToCheck.transform.position);
                if(dist < ClosestDistance)
                {
                    ClosestPos = GridObjects[i, j].transform.position;
                    ClosestDistance = dist;
                }
            }
        }
        return ClosestPos;
    }

    void AssignGridObject(GameObject ObjectToBeAssigned)
    {
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                if(GridObjects[i, j].transform.position == ObjectToBeAssigned.transform.position)
                {
                    // Set object to gridobject array position (Overriding whatever was in there before)
                    Destroy(GridObjects[i, j]);
                    
                    GridObjects[i, j] = ObjectToBeAssigned;
                }
            }
        }
    }
}
