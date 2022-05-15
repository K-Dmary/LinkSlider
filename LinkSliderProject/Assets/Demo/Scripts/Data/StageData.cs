using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo {
    /// <summary>
    /// ステージデータ
    /// </summary>
    [CreateAssetMenu(menuName = "Demo/StageData")]
    [System.Serializable]
    public class StageData : ScriptableObject
    {
        [Header("ステージ名")]
        [SerializeField]
        private string stageName;

        [Header("サイズ：縦 × 横")]
        [SerializeField]
        private Vector2Int size;

        [Header("パネル")]
        [SerializeField]
        private List<PanelData> panelDatas = new List<PanelData>();

        [Header("パネルの大きさ")]
        [SerializeField]
        private float panelSize;

        /// <summary>
        /// 名前
        /// </summary>
        public string Name => stageName;

        /// <summary>
        /// 縦×横
        /// </summary>
        public Vector2Int Size => size;

        /// <summary>
        /// パネルデータ
        /// </summary>
        public List<PanelData> PanelDatas => panelDatas;

        /// <summary>
        /// パネルの数
        /// </summary>
        public int PanelCount => panelDatas.Count;

        public IEnumerable GetPanels()
        {
            for (int i = 0; i < PanelCount; i++)
            {
                yield return panelDatas[i];
            }
        }
    }
}
