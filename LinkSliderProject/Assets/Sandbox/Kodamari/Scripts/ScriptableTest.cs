using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sandbox.Kodama
{
    [CreateAssetMenu(menuName = "Kodama/ScriptableTest")]
    public class ScriptableTest : ScriptableObject
    {
        [SerializeField]
        private GameObject prefab;

        [SerializeField]
        private Sprite sprite;

        public GameObject Prefab => prefab;

        public Sprite Sprite => sprite;
    }
}
