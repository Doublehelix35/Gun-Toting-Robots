using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour {

    [SerializeField] BackgroundScrolling[] Backgrounds;

    Vector2 LastFramePos;

    // Use this for initialization
    void Start()
    {
        LastFramePos = new Vector2(transform.position.x, transform.position.y); // Starting position
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 CurPos = new Vector2(transform.position.x, transform.position.y); // Current position

        if (LastFramePos == CurPos) // if havent moved, exit
        {
            return;
        }

        // Calc pos
        Vector2 pos;
        pos = LastFramePos - CurPos;
        pos.x = pos.x - (int)pos.x;
        pos.y = pos.y - (int)pos.y;

        foreach (BackgroundScrolling s in Backgrounds)
        {
            s.MoveTexture(pos);
        }

        // Update last frame pos
        LastFramePos = CurPos;
    }
}
