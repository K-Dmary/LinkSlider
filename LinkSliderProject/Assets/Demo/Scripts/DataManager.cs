using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Demo
{ 
    public class DataManager
    {
        public static DataManager instance; 

        public static DataManager Create()
        {
            instance = new DataManager();
            return instance;
        }
        
        private StageData stageData;
        public async UniTask Load(string stagename)
        {
            var handle = Addressables.LoadAssetAsync<StageData>(stagename);
            await handle.Task;
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                stageData = handle.Result;
                Debug.Log(stageData.PanelCount);
            }
        }
    }
}
