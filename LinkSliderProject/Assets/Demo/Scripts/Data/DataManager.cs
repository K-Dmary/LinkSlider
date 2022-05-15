using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Demo
{ 
    /// <summary>
    /// データの読み込み
    /// </summary>
    public class DataManager
    {
        private static StageData stageData;

        internal static StageData Stage => stageData;
        
        /// <summary>
        /// 読み込み
        /// </summary>
        /// <param name="stagename">データ名</param>
        /// <returns>UniTask</returns>
        public static async UniTask LoadStageData(string stagename)
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
