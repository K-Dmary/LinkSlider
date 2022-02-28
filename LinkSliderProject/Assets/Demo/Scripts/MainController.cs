using InputSystemManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo
{
    public class MainController : MonoBehaviour
    {
        private DemoInputManager input;

        [SerializeField] DemoPanelsManager panelsManager;

        private void LoadInput()
        {
            input = new DemoInputManager();
            input.Load();

            //Fireƒ{ƒ^ƒ““o˜^—á
            input.RegistFire(() => Debug.Log("Fire"));
        }

        private void LoadMap()
        {
            Debug.Log("Load Map");
        }

        private void Start()
        {
            LoadInput();
            LoadMap();
        }

        private void PanelController()
        {
            if (input.Up) panelsManager.TargetPanel?.Up();
            if (input.Down) panelsManager.TargetPanel?.Down();
            if (input.Right) panelsManager.TargetPanel?.Right();
            if (input.Left) panelsManager.TargetPanel?.Left();
        }

        // Update is called once per frame
        private void Update()
        {
            PanelController();
        }
    }
}
