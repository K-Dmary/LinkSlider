using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

/// <summary>
/// マップ管理クラス
/// </summary>
public class MapManager
{
    public static MapManager instance;
    MapData mapdata;

    public static void Create(GameObject parent)
    {
        instance = new MapManager(parent);
    }

    private TileManager tileManager;
    private GridManager gridManager;

    public MapManager(GameObject parent)
    {
        var tileManagerParent = new GameObject("TileManager");
        tileManagerParent.transform.parent = parent.transform;
        var gridManagerParent = new GameObject("GridManager");
        gridManagerParent.transform.parent = parent.transform;

        gridManager = new GridManager(gridManagerParent);
        tileManager = new TileManager(tileManagerParent, gridManager);
    }

    public TileManager TileManager => tileManager;

    public async UniTask SetUp()
    {
        await gridManager.LoadData();
        await tileManager.LoadData();
    } 

    public async UniTask LoadMap(string mapName)
    {
        var loader = new MapLoader();
        mapdata = await loader.Load(mapName);

        gridManager.Grid.SetSize(mapdata.Size);
        gridManager.Grid.SetCellSize(tileManager.TileSize);
        tileManager.Load(mapdata.Tiles);
    }

    ~MapManager()
    {
        var loader = new MapLoader();
        loader.UnLoad(mapdata);
    }
}

/// <summary>
/// マップデータ読み込みクラス
/// </summary>
public class MapLoader : LoaderBase<MapData>
{
}
