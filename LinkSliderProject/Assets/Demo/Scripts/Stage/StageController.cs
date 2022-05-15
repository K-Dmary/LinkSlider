using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo {
    /// <summary>
    /// ステージ内のオブジェクト管理
    /// </summary>
    public class StageController : MonoBehaviour
    {
        [SerializeField] private GameObject floor;
        [SerializeField] private PanelsController panelsController;

        internal void Load()
        {
            LoadStage();
            LoadPanels();
        }

        /// <summary>
        /// ステージの読み込み
        /// </summary>
        internal void LoadStage()
        {
            Vector2 size = DataManager.Stage.Size;
            floor.transform.localScale = new Vector3(size.y * 0.1f, 1, size.x * 0.1f);
        }

        /// <summary>
        /// パネルの読み込み
        /// </summary>
        internal void LoadPanels()
        {
            panelsController.Dispose();
            foreach (PanelData panel in DataManager.Stage.GetPanels())
            {
                panelsController.Generate(panel);
            }
        }
    }
}
