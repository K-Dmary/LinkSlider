using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo
{
    [System.Serializable]
    public class PanelData
    {
        [SerializeField]
        private Vector2Int position;

        public Vector2 Position => position; 
    }
}
