using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairControl : MonoBehaviour
{
    public Texture2D crosshair;

    // called before the first frame of the game
    void Start()
    {
        Cursor.SetCursor(crosshair, Vector2.zero, CursorMode.ForceSoftware);
    }
}
