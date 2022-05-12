using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo
{
    public class GameSceneManager : MonoBehaviour
    {
        [SerializeField] string stageName = "Demo";
        async void Start()
        {
            var datamanager = DataManager.Create();
            await datamanager.Load(stageName);
        }

        void Update()
        {

        }
    }
}
