using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using System.Linq;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Demo
{
    public enum MoveType
    {
        Up,
        Down,
        Right,
        Left,
    }

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

        private Panel target = null;

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
        /// ����Ώۂ̃p�l�����ړ�������
        /// </summary>
        /// <param name="type">����̎��</param>
        public void Move(MoveType type)
        {
            if (target == null) return;
            switch (type)
            {
                case MoveType.Up:
                    target.Up();
                    break;
                case MoveType.Down:
                    target.Down();
                    break;
                case MoveType.Right:
                    target.Right();
                    break;
                case MoveType.Left:
                    target.Left();
                    break;
            }
        }

        /// <summary>
        /// ����Ώۂ̃p�l�������߂�
        /// </summary>
        /// <param name="index">�C���f�b�N�X���W</param>
        public void SetTargetPanel(Vector2Int index) => target = GetPanel(index);

        /// <summary>
        /// �p�l���T��
        /// </summary>
        /// <param name="index">�C���f�b�N�X���W</param>
        /// <returns>�Ώۂ̃p�l��</returns>
        private Panel GetPanel(Vector2Int index)
        {
            List<Panel> ps = panels.Where(p => p.IndexPosition == index).ToList();
            if (ps.Count == 1) return ps[0];
            return null;
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

            // �ǂݎ���p�̕\�������p�l��
            public GameObject Obj { get => panel; }

            // index���W�̃v���p�e�B
            public Vector2Int IndexPosition
            {
                get => Vector2Int.FloorToInt(indexPositionRp.Value);
                set
                {
                    Vector2Int index = new Vector2Int(value.x, value.y);

                    if (gridData.Width <= index.x) index.x = gridData.Width - 1;
                    if (index.x < 0) index.x = 0;

                    if (gridData.Height <= index.y) index.y = gridData.Height - 1;
                    if (index.y < 0) index.y = 0;

                    indexPositionRp.Value = index;
                }
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
