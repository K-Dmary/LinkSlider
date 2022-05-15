using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo {
    /// <summary>
    /// �X�e�[�W���̃I�u�W�F�N�g�Ǘ�
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
        /// �X�e�[�W�̓ǂݍ���
        /// </summary>
        internal void LoadStage()
        {
            Vector2 size = DataManager.Stage.Size;
            floor.transform.localScale = new Vector3(size.y * 0.1f, 1, size.x * 0.1f);
        }

        /// <summary>
        /// �p�l���̓ǂݍ���
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
