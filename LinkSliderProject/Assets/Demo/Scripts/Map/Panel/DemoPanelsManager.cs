using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Demo
{
    public class DemoPanelsManager : MonoBehaviour
    {
        [SerializeField] private GameObject paretnt;
        [SerializeField] private PanelInfoScriptableObject panelInfo;

        [Space]
        [SerializeField] private List<Panel> panels;
        
        private IntReactiveProperty targetPanelNumRp = new IntReactiveProperty(0);
        
        public int TargetPanelNum { get => targetPanelNumRp.Value; set => targetPanelNumRp.Value = value; }

        public bool IsRangeOfPanels(int value) => panels.Count != 0 && 0 <= value && value < panels.Count;
        public Panel TargetPanel
        {
            get
            {
               if(!IsRangeOfPanels(TargetPanelNum))
                {
                    return null;
                }

                return panels[TargetPanelNum];
            }
        }


        public void InstantiatePanel()
        {
            GameObject instance = Instantiate(panelInfo.panelPredfab);

            float size = panelInfo.Size * 0.1f;
            Vector3 scale = new Vector3(size, 1, size);
            instance.transform.localScale = scale;

            instance.transform.parent = paretnt.transform;

            panels.Add(new Panel(instance));
        }

        public void DeleteEndPanel()
        {
            if(!IsRangeOfPanels(panels.Count - 1))
            {
                return;
            }

            Panel endPanel = panels[panels.Count - 1];
            endPanel.Dispose();
            panels.Remove(endPanel);
        }


        [System.Serializable]
        public class Panel
        {
            [SerializeField] GameObject panel;
            private Vector2 coordinates;
            private float Speed;

            public Panel(GameObject obj)
            {
                panel = obj;
                Speed = 1;
            }

            public void Up()
            {
                coordinates.y += Speed * Time.deltaTime;
                Debug.Log("Up : " + coordinates);
                SetPanelPosition();
            }

            public void Down()
            {
                coordinates.y -= Speed * Time.deltaTime;
                Debug.Log("Down : " + coordinates);
                SetPanelPosition();
            }

            public void Right()
            {
                coordinates.x += Speed * Time.deltaTime;
                Debug.Log("Right : " + coordinates);
                SetPanelPosition();
            }

            public void Left()
            {
                coordinates.x -= Speed * Time.deltaTime;
                Debug.Log("Left : " + coordinates);
                SetPanelPosition();
            }

            private void SetPanelPosition() => panel.transform.localPosition = new Vector3(coordinates.x, 0, coordinates.y);

            public void Dispose() => DestroyImmediate(panel);
        }
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(DemoPanelsManager))]
    public class DemoMapEditor : Editor
    {
        DemoPanelsManager map;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            map = target as DemoPanelsManager;

            if (GUILayout.Button("CreatePanel"))
            {
                map.InstantiatePanel();
            }

            if (GUILayout.Button("DeletePanel"))
            {
                map.DeleteEndPanel();
            }
        }
    }
#endif
}
