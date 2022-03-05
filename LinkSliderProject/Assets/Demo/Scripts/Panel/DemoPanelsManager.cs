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
    /// パネル全体を管理するクラス
    /// </summary>
    public class DemoPanelsManager : MonoBehaviour
    {
        // パネルをまとめておく親オブジェクト
        [SerializeField] private GameObject paretnt;
        // パネルのサイズなどの情報データ
        [SerializeField] private PanelInfoScriptableObject panelInfo;
        // グリッドデータ
        [SerializeField] private GridDataSctiptableObject gridData;

        // パネルリスト
        [Space]
        [SerializeField] private List<Panel> panels;

        private Panel target = null;

        // パネルの生成
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
        /// パネルリストの最後の要素を削除する
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
        /// 操作対象のパネルを移動させる
        /// </summary>
        /// <param name="type">操作の種類</param>
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
        /// 操作対象のパネルを決める
        /// </summary>
        /// <param name="index">インデックス座標</param>
        public void SetTargetPanel(Vector2Int index) => target = GetPanel(index);

        /// <summary>
        /// パネル探索
        /// </summary>
        /// <param name="index">インデックス座標</param>
        /// <returns>対象のパネル</returns>
        private Panel GetPanel(Vector2Int index)
        {
            List<Panel> ps = panels.Where(p => p.IndexPosition == index).ToList();
            if (ps.Count == 1) return ps[0];
            return null;
        }

        /// <summary>
        /// パネル単体の情報クラス
        /// </summary>
        [System.Serializable]
        public class Panel
        {
            // 表示されるパネル
            [SerializeField] GameObject panel;

            // index座標のリアクティブプロパティ
            [SerializeField, HideInInspector]
            private Vector2ReactiveProperty indexPositionRp;

            // gridData
            [SerializeField, HideInInspector] 
            private GridDataSctiptableObject gridData;

            //コンストラクタ
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

            // 読み取り専用の表示されるパネル
            public GameObject Obj { get => panel; }

            // index座標のプロパティ
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

            //上に移動
            public void Up()
            {
                Vector2Int nextIndex = IndexPosition;
                nextIndex.y++;
                IndexPosition = nextIndex;
                Debug.Log("Up : " + IndexPosition);
            }

            //下に移動
            public void Down()
            {
                Vector2Int nextIndex = IndexPosition;
                nextIndex.y--;
                IndexPosition = nextIndex;
                Debug.Log("Down : " + IndexPosition);
            }

            //右に移動
            public void Right()
            {
                Vector2Int nextIndex = IndexPosition;
                nextIndex.x++;
                IndexPosition = nextIndex;
                Debug.Log("Right : " + IndexPosition);
            }

            //左に移動
            public void Left()
            {
                Vector2Int nextIndex = IndexPosition;
                nextIndex.x--;
                IndexPosition = nextIndex;
                Debug.Log("Left : " + IndexPosition);
            }

            //メモリの開放
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
