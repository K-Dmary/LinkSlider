using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Demo
{
    /// <summary>
    /// �p�l���S�̂��Ǘ�����N���X
    /// </summary>
    public class DemoPanelsManager : MonoBehaviour
    {
        // �p�l�����܂Ƃ߂Ă����e�I�u�W�F�N�g
        [SerializeField] private GameObject paretnt;
        // �p�l���̃T�C�Y�Ȃǂ̏��f�[�^
        [SerializeField] private PanelInfoScriptableObject panelInfo;
        // �O���b�h�f�[�^
        [SerializeField] private GridDataSctiptableObject gridData;

        // �p�l�����X�g
        [Space]
        [SerializeField] private List<Panel> panels;

        // �p�l���̐���
        public void GeneratePanel()
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

        /// <summary>
        /// �p�l�����X�g�̍Ō�̗v�f���폜����
        /// </summary>
        public void DeleteEndPanel()
        {
            if(panels.Count == 0)
            {
                return;
            }

            Panel endPanel = panels[panels.Count - 1];
            endPanel.Dispose();
            panels.Remove(endPanel);
        }

        /// <summary>
        /// �p�l���P�̂̏��N���X
        /// </summary>
        [System.Serializable]
        public class Panel
        {
            // �\�������p�l��
            [SerializeField] GameObject panel;

            // index���W�̃��A�N�e�B�u�v���p�e�B
            [SerializeField, HideInInspector]
            private Vector2ReactiveProperty indexPositionRp;

            // gridData
            [SerializeField, HideInInspector] 
            private GridDataSctiptableObject gridData;

            // �ǂݎ���p�̕\�������p�l��
            public GameObject Obj { get => panel; }

            // index���W�̃v���p�e�B
            private Vector2Int IndexPosition
            {
                get => Vector2Int.FloorToInt(indexPositionRp.Value);
                set
                {
                    Vector2 index = new Vector2(value.x, value.x);

                    if (index.x < 0) index.x = 0;
                    if (gridData.Width <= index.x) index.x = gridData.Width- 1;

                    if (index.y < 0) index.y = 0;
                    if (gridData.Height <= index.y) index.y = gridData.Height - 1;

                    indexPositionRp.Value = index;
                }
            }

            //�R���X�g���N�^
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

            //��Ɉړ�
            public void Up()
            {
                Vector2Int nextIndex = IndexPosition;
                nextIndex.y++;
                IndexPosition = nextIndex;
                Debug.Log("Up : " + IndexPosition);
            }

            //���Ɉړ�
            public void Down()
            {
                Vector2Int nextIndex = IndexPosition;
                nextIndex.y--;
                IndexPosition = nextIndex;
                Debug.Log("Down : " + IndexPosition);
            }

            //�E�Ɉړ�
            public void Right()
            {
                Vector2Int nextIndex = IndexPosition;
                nextIndex.x++;
                IndexPosition = nextIndex;
                Debug.Log("Right : " + IndexPosition);
            }

            //���Ɉړ�
            public void Left()
            {
                Vector2Int nextIndex = IndexPosition;
                nextIndex.x--;
                IndexPosition = nextIndex;
                Debug.Log("Left : " + IndexPosition);
            }

            //�������̊J��
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

            if (GUILayout.Button("GeneratePanel"))
            {
                map.GeneratePanel();
            }

            if (GUILayout.Button("DeletePanel"))
            {
                map.DeleteEndPanel();
            }
        }
    }
#endif
}
