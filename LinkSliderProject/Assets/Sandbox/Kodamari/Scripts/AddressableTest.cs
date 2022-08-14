using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Sandbox.Kodama
{
    public class AddressableTest : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer spriteRenderer;
        [SerializeField]
        private GameObject obj;

        private AsyncOperationHandle<ScriptableTest> handle;

        // Start is called before the first frame update
        private void Start()
        {
            
        }

        public async void Load()
        {
            handle = Addressables.LoadAssetAsync<ScriptableTest>("Test");
            var test = await handle.Task;
            obj = Instantiate(test.Prefab);
            spriteRenderer.sprite = test.Sprite;
        }

        public void UnLoad()
        {
            Addressables.Release(handle);
        }
    }
}
