using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace Demo {
    [CreateAssetMenu(menuName = "Demo/Stage/StageData")]
    [System.Serializable]
    public class StageData : ScriptableObject
    {
        [SerializeField]
        private string stageName;

        [SerializeField]
        private Vector2Int size;

        [SerializeField]
        private Cells cells;

        [SerializeField]
        private PanelsData panelsData;

        public PanelsData PanelsData => panelsData; 

        public void CreateStageData()
        {
            cells.RemoveAll();
            for(int y = 0; y < size.y; y++)
            {
                for(int x = 0; x < size.x; x++)
                {
                    cells.Add(new Cell(new Vector2Int(x, y), true));
                }
            }
        }

#if UNITY_EDITOR
        public void CreatePanelsData()
        {
            foreach (var asset in AssetDatabase.LoadAllAssetsAtPath(AssetDatabase.GetAssetPath(this)))
            {
                if (AssetDatabase.IsSubAsset(asset) && asset.GetType() == typeof(PanelsData))
                {
                    return;
                }
            }
            var createasset = CreateInstance<PanelsData>();
            createasset.name = "PanelsData";
            panelsData = createasset;
            AssetDatabase.AddObjectToAsset(createasset, this);
            AssetDatabase.SaveAssets();
            AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(this));
        }

        public void RemovePanelsData()
        {
            foreach (var asset in AssetDatabase.LoadAllAssetsAtPath(AssetDatabase.GetAssetPath(this)))
            {
                if (AssetDatabase.IsSubAsset(asset) && asset.GetType() == typeof(PanelsData))
                {
                    DestroyImmediate(asset, true);
                }
            }
            panelsData = null;
            AssetDatabase.Refresh();
            AssetDatabase.SaveAssets();
        }
#endif

    }
}
