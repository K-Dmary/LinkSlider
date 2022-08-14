using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// タイル管理クラス
/// </summary>
public class TileManager
{
    private static readonly string tileDataAddress = "TileData_Demo";
    private Dictionary<Vector2Int, Tile> tiles;
    private TileData tileData;
    private GridManager gridManager;
    private GameObject parent;

    public TileManager(GameObject parent, GridManager gridManager)
    {
        this.parent = parent;
        this.gridManager = gridManager;
        tiles = new Dictionary<Vector2Int, Tile>();
    }

    ~TileManager()
    {
        foreach(var tile in tiles)
        {
            tile.Value.Destroy();
        }

        var loader = new TileLoader();
        loader.UnLoad(tileData);
    }

    public float TileSize => tileData.TileSize;

    public async UniTask LoadData()
    {
        var loader = new TileLoader();
        tileData = await loader.Load(tileDataAddress);
    }

    public void Load(IEnumerable<Vector2Int> postions)
    {
        //消す
        var beforePositions = tiles.Select(tile => tile.Key);
        var deleteTielKeys = beforePositions.Except(postions);
        foreach(var key in deleteTielKeys)
        {
            tiles[key].Destroy();
            tiles.Remove(key);
        }

        //増やす
        foreach(var position in postions)
        {
            Create(position);
        }
    }

    private Tile Create(Vector2Int gridPosition)
    {
        if (tiles.ContainsKey(gridPosition))
        {
            return tiles[gridPosition];
        }

        var tileObject = MonoBehaviour.Instantiate(tileData.Prefab);
        tileObject.transform.parent = parent.transform;
        var tile = new Tile(tileObject);
        tiles.Add(gridPosition, tile);

        //gridから座標を計算する
        var worldPosition = gridManager.ConvertGridToWorldPosition(gridPosition);
        tile.SetPosition(worldPosition);

        return tile;
    }
}

/// <summary>
/// タイルクラス
/// </summary>
public class Tile {
    private GameObject tileObject;

    public Tile(GameObject tileObject)
    {
        this.tileObject = tileObject;
    }

    public void SetPosition(Vector2 postion)
    {
        tileObject.transform.localPosition = new Vector3(postion.x, 0, postion.y);
    }

    public void Destroy()
    {
        MonoBehaviour.Destroy(tileObject);
    }
}

/// <summary>
/// タイルデータ読み込みクラス
/// </summary>
public class TileLoader : LoaderBase<TileData>
{
}
