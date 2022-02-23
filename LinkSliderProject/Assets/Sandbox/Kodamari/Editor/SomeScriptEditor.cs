using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SomeScript))]
public class SomeScriptEditor : Editor
{
    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();

        DrawDefaultInspector();

        SomeScript myScript = (SomeScript)target;
        if (GUILayout.Button("BuildObject"))
        {
            myScript.BuildObject();
        }
    }
}