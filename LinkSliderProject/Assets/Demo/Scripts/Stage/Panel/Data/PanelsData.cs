using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Demo
{
    [System.Serializable]
    public class PanelsData : ScriptableObject
    {
        [SerializeField]
        private int a = 5;
        public int A => a;
    }
}
