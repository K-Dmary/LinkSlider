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
        [SerializeField] private GridDataSctiptableObject gridData;

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
            if (!gridData.IsExist)
            {
                return;
            }

            gridData.Generate();
            GameObject instance = Instantiate(panelInfo.panelPredfab);

            float size = panelInfo.Size * 0.1f;
            Vector3 scale = new Vector3(size, 1, size);
            instance.transform.localScale = scale;
            instance.transform.parent = paretnt.transform;
            panels.Add(new Panel(instance, gridData));
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
            [SerializeField, HideInInspector]
            private Vector2ReactiveProperty indexPositionRp;

            [SerializeField, HideInInspector] 
            private GridDataSctiptableObject gridData;
            
            private Vector2 IndexPosition { get => indexPositionRp.Value; set => indexPositionRp.Value = value; }
            public GameObject Obj { get => panel; }
            private float IndexPositionX
            {
                get => IndexPosition.x;
                set
                {
                    if(value < 0 || gridData.Width <= value)
                    {
                        return;
                    }
                    Vector2 index = IndexPosition;
                    index.x = value;
                    IndexPosition = index;
                }
            }
            private float IndexPositionY
            {
                get => IndexPosition.y;
                set
                {
                    if (value < 0 || gridData.Height <= value)
                    {
                        return;
                    }
                    Vector2 index = IndexPosition;
                    index.y = value;
                    IndexPosition = index;
                }
            }

            public Panel(GameObject obj, GridDataSctiptableObject data)
            {
                panel = obj;
                gridData = data;

                indexPositionRp = new Vector2ReactiveProperty();
                indexPositionRp.Subscribe(_ =>
                {
                    panel.transform.localPosition = gridData.GetMassPos(IndexPosition);
                }).AddTo(panel);
            }

            public void Up()
            {
                IndexPositionY += 1;
                Debug.Log("Up : " + IndexPosition);
            }

            public void Down()
            {
                IndexPositionY -= 1;
                Debug.Log("Down : " + IndexPosition);
            }

            public void Right()
            {
                IndexPositionX += 1;
                Debug.Log("Right : " + IndexPosition);
            }

            public void Left()
            {
                IndexPositionX -= 1;
                Debug.Log("Left : " + IndexPosition);
            }

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
