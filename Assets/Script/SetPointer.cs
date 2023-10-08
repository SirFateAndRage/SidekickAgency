using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SetPointer : MonoBehaviour
{
    [SerializeField] Texture2D _pointer;

    void Start()
    {
        Cursor.SetCursor(_pointer, Vector2.zero, CursorMode.ForceSoftware);
    }

}
