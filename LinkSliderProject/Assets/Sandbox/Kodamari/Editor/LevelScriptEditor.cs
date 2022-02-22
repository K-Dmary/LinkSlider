using System.Collections;
using System.Collections.Generic;
using UnityEditor;

[CustomEditor(typeof(LevelScript))]
public class LevelScriptEditor : Editor
{
    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();

        LevelScript myTarget = (LevelScript)target;

        myTarget.experience = EditorGUILayout.IntField("Experience", myTarget.experience);
        EditorGUILayout.LabelField("Level", myTarget.Level.ToString());
    }
}
