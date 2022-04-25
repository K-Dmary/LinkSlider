using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Demo
{
    public class GameSceneManager : MonoBehaviour
    {
        [Header("Stage")]
        [SerializeField] private AssetReference stageDataAddress;
        void Start()
        {
            StageManager.Create();
            StageManager.Instance.Load(stageDataAddress);
        }

        void Update()
        {

        }
    }
}
