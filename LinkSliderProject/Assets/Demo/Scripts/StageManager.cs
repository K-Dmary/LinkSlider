using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Demo
{ 
    public class StageManager
    {
        public static StageManager Instance;

        public static void Create()
        {
            if(Instance != null)
            {
                Debug.Log("インスタンスが存在します。");
            }

            Instance = new StageManager();
        }

        private StageData stageData;
        private StagePanelsManager stagePanels;

        public void Load(AssetReference address)
        {
            Addressables.LoadAssetAsync<StageData>(address.AssetGUID).Completed += asset =>
            {
                Debug.Log(address);
                stageData = asset.Result;
                stagePanels = new StagePanelsManager(stageData.PanelsData);
            };
            
        }
    }
}
