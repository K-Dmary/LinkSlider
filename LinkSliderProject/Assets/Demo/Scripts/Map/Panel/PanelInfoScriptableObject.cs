using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo
{
    [CreateAssetMenu(menuName = "Scriptables/Create PanelInfo")]
    public class PanelInfoScriptableObject : ScriptableObject
    {
        [SerializeField] public GameObject panelPredfab;
        [SerializeField] public float Size;
    }
}
