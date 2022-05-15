using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo
{
    public class GameSceneManager : MonoBehaviour
    {
        [SerializeField] string stageName = "Demo";
        [SerializeField] StageController stageController;
        async void Start()
        {
            await DataManager.LoadStageData(stageName);
            stageController.Load();
        }

        void Update()
        {

        }
    }
}
