using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InGameCursor
{
    static Texture2D defaultCursor;
    static Texture2D turgetCursor;
    public static void CursorInit()
    {
        defaultCursor = Resources.Load("Cursors/arrows_v2") as Texture2D;
        turgetCursor = Resources.Load("Cursors/target_v1") as Texture2D;
        Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.ForceSoftware);
    }

    public static void SetDefaultCursor()
    {
        Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.ForceSoftware);
    }

    public static void SetTargetCursor()
    {
        Cursor.SetCursor(turgetCursor, new Vector2(16, 16), CursorMode.ForceSoftware);
    }
}
