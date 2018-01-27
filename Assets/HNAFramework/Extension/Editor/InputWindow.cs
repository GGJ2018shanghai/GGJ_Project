using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class InputWindow : EditorWindow
{
    string input_label;
    string input_content;

    string btn_ok;
    string btn_cancel;

    Action<string> on_btn_ok;
    Action on_btn_cancel;

    bool trigger_btn_cancel = false;

    static public void ShowWindow(string inputLable, Action<string> on_btn_ok, Action on_btn_cancel = null, string title = "Input Window", string btn_ok = "Ok", string btn_cancel = "Cancel")
    {
        InputWindow window = ScriptableObject.CreateInstance<InputWindow>();
        window.input_label = inputLable;
        window.on_btn_ok = on_btn_ok;
        window.on_btn_cancel = on_btn_cancel;
        window.titleContent = new GUIContent(title);
        window.btn_ok = btn_ok;
        window.btn_cancel = btn_cancel;
        window.maxSize = window.minSize = new Vector2(250, 100);
        window.ShowUtility();
    }

    void OnGUI()
    {
        EditorGUILayout.Space();
        input_content = EditorGUILayout.TextField(input_label, input_content);
        GUILayout.FlexibleSpace();
        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        if (GUILayout.Button(btn_ok)) { if (on_btn_ok != null && !string.IsNullOrEmpty(input_label)) on_btn_ok(input_content); Close(); }
        if (GUILayout.Button(btn_cancel)) { if (on_btn_cancel != null) on_btn_cancel(); trigger_btn_cancel = true; Close(); }
        EditorGUILayout.EndHorizontal();
    }

    void OnDestroy()
    {
        if (!trigger_btn_cancel && on_btn_cancel != null) on_btn_cancel();
    }
}