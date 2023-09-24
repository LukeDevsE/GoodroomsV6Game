using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetMouse : MonoBehaviour
{
    public Texture2D cursorTexture;
    public Vector2 hotspot = Vector2.zero;

    private void Start()
    {
        Cursor.SetCursor(cursorTexture, hotspot, CursorMode.ForceSoftware);
    }

    private void Update()
    {
    }
}
