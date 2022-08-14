using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Sandbox.Kodama
{
    public class TestUI : MonoBehaviour
    {
        [SerializeField]
        private Button loadButton;

        [SerializeField]
        private Button UnLoadButton;

        [SerializeField]
        private AddressableTest test;

        public void Register()
        {
            loadButton.onClick.AddListener(() => test.Load());
            UnLoadButton.onClick.AddListener(() => test.UnLoad());
        }
    }
}
