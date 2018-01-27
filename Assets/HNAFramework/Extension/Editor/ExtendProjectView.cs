using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class ExtendProjectView : Editor
{
    [MenuItem("Assets/HNA Extension/Open Project Settings")]
    public static void OpenPlayerSettings()
    {
        EditorApplication.ExecuteMenuItem("Edit/Project Settings/Player");
    }
    [MenuItem("Assets/HNA Extension/Locate Game Logic Data %g")]
    public static void LocateGameInstanceData()
    {
        PingResource("GameLogicManagerData");
    }
    [MenuItem("Assets/HNA Extension/Inspect Current %i")]
    public static void InspectCurrentSelection()
    {
        if (Selection.activeObject == null) return;
        InspectTarget(Selection.activeObject);
    }

    public static void PingResource(string resourceName)
    {
        var target = Resources.Load(resourceName);
        EditorGUIUtility.PingObject(target);
        InspectTarget(target);
    }

    public static void InspectTarget(Object target, bool useAdditiveInspector = true)
    {
        if (!useAdditiveInspector)
        {
            Selection.activeObject = target;
            return;
        }
        var inspectorType = typeof(Editor).Assembly.GetType("UnityEditor.InspectorWindow");
        var inspectorInstance = ScriptableObject.CreateInstance(inspectorType) as EditorWindow;
        inspectorInstance.Show();
        var prevSelection = Selection.activeGameObject;
        Selection.activeObject = target;
        var isLocked = inspectorType.GetProperty("isLocked", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);
        isLocked.GetSetMethod().Invoke(inspectorInstance, new object[] { true });
        Selection.activeGameObject = prevSelection;
    }

    //[MenuItem("Assets/HNA Extension/Edit Selected Credits")]
    //public static void EditCredits()
    //{
    //    var obj = Selection.activeObject;
    //    if (obj != null)
    //    {
    //        var path = AssetDatabase.GetAssetPath(obj.GetInstanceID());
    //        var file = new FileInfo(path);
    //        if (file.Extension == ".xml")
    //        {
    //            var ce = new CreditsEditor();
    //            ce.OpenFile(path);
    //            ce.Show();
    //        }
    //    }
    //}
}
