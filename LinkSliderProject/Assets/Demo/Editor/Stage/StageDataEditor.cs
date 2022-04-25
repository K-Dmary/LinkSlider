using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

namespace Demo.Editor
{
    [CustomEditor(typeof(StageData))]
    public class StageDataEditor : UnityEditor.Editor
    {
        private StageData stageData;

        private void OnEnable()
        {
            stageData = target as StageData;
            EditorUtility.SetDirty(stageData);
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (GUILayout.Button("Create"))
            {
                stageData.CreateStageData();
            }

            GUILayout.Space(20);
            if (GUILayout.Button("CreatePanelsData"))
            {
                stageData.CreatePanelsData();
            }
            if (GUILayout.Button("PanelçÌèú"))
            {
                stageData.RemovePanelsData();
            }

            GUILayout.Space(20);
        }
    }
}
