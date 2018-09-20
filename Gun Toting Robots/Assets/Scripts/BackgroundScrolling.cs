using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScrolling : MonoBehaviour {

    [Range(0, 1)] public float XSpeed = 1; // Speed to move on the x axis
    [Range(0, 1)] public float YSpeed = 1; // Speed to move on the y axis

    Vector2 offset = Vector2.zero;
    Material mat; // Material to move

    void Awake()
    {
        // Get material on gameobject
        mat = GetComponent<MeshRenderer>().material;
    }

    public void MoveTexture(Vector2 pos)
    {
        // Calculate offset
        offset.x += pos.x * XSpeed;
        offset.y += pos.y * YSpeed;

        if (offset.x > 1f)
        {
            offset.x -= 1f;
        }
        else if (offset.x < -1f)
        {
            offset.x += 1f;
        }
        if (offset.y > 1f)
        {
            offset.y -= 1f;
        }
        else if (offset.y < -1f)
        {
            offset.y += 1f;
        }

        // Set mat offset to new offset
        mat.mainTextureOffset = offset;
    }
}
