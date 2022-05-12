using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo {
    [CreateAssetMenu(menuName = "Demo/StageData")]
    [System.Serializable]
    public class StageData : ScriptableObject
    {
        [SerializeField]
        private string stageName;

        [SerializeField]
        private Vector2Int size;

        [SerializeField]
        private List<PanelData> panelsData = new List<PanelData>();

        public int PanelCount => panelsData.Count;
    }
}
