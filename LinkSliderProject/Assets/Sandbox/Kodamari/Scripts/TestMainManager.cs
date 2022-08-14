using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sandbox.Kodama
{
    public class TestMainManager : MonoBehaviour
    {
        [SerializeField]
        private TestUI ui;

        void Start()
        {
            ui.Register();
        }

        void Update()
        {

        }
    }
}
