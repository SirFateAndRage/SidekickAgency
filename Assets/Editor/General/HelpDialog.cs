using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/// <summary>
/// Generic help dialog for all windows
/// </summary>
public class HelpDialog : PopupWindowContent
{
    private static string _text;
    public static Color helpButtonColor = new Color(.5f, 1, .3f, .7f);

    public static void Show()
    {
        var currentEvent = Event.current;
        var rect = new Rect(currentEvent.mousePosition.x,
                            currentEvent.mousePosition.y - 250,
                            250,
                            250);
        PopupWindow.Show(rect, new HelpDialog());
    }

    /// <summary>
    /// Shows dialog containing a text. Supports rich text.
    /// </summary>
    /// <param name="text">The text to be displayed.</param>
    public static void Show(string text)
    {
        _text = text;
        Show();
    }

    public override void OnGUI(Rect rect)
    {
        GUIStyle textStyle = new GUIStyle();
        textStyle.richText = true;
        textStyle.wordWrap = true;
        textStyle.alignment = TextAnchor.MiddleCenter;
        textStyle.padding = new RectOffset(10, 10, 0, 0);

        EditorGUILayout.TextArea(_text, textStyle);
    }
}
