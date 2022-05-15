using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo {
    /// <summary>
    /// �X�e�[�W�f�[�^
    /// </summary>
    [CreateAssetMenu(menuName = "Demo/StageData")]
    [System.Serializable]
    public class StageData : ScriptableObject
    {
        [Header("�X�e�[�W��")]
        [SerializeField]
        private string stageName;

        [Header("�T�C�Y�F�c �~ ��")]
        [SerializeField]
        private Vector2Int size;

        [Header("�p�l��")]
        [SerializeField]
        private List<PanelData> panelDatas = new List<PanelData>();

        [Header("�p�l���̑傫��")]
        [SerializeField]
        private float panelSize;

        /// <summary>
        /// ���O
        /// </summary>
        public string Name => stageName;

        /// <summary>
        /// �c�~��
        /// </summary>
        public Vector2Int Size => size;

        /// <summary>
        /// �p�l���f�[�^
        /// </summary>
        public List<PanelData> PanelDatas => panelDatas;

        /// <summary>
        /// �p�l���̐�
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
