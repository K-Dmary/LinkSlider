using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// グリッド管理クラス
/// </summary>
public class GridManager
{
    private static readonly string prefabAddress = "GridData_Demo";
    private GameObject gridPrefab;
    private KeyValuePair<string, Grid> currentGrid;
    private GameObject parent;

    public GridManager(GameObject parent)
    {
        this.parent = parent;
        currentGrid = new KeyValuePair<string, Grid>();
    }

    ~GridManager()
    {
        var loader = new GridPrefabLoader();
        loader.UnLoad(gridPrefab);
    }

    public Grid Grid => currentGrid.Value;

    public async UniTask<Grid> LoadData(string dataName = "")
    {
        if(dataName == "")
        {
            dataName = prefabAddress;
        }

        if (currentGrid.Key == dataName)
        {
            return currentGrid.Value;
        }

        var loader = new GridPrefabLoader();
        gridPrefab = await loader.Load(dataName);
        var gridObject = MonoBehaviour.Instantiate(gridPrefab);
        gridObject.transform.parent = parent.transform;
        var grid = new Grid(gridObject);
        currentGrid = new KeyValuePair<string, Grid>(dataName, grid);
        return grid;
    }

    public Vector2 ConvertGridToWorldPosition(Vector2Int gridPosition)
    {
        var grid = currentGrid.Value;
        var size = (Vector2)grid.Size;
        var cellsize = grid.CellSize;
        var index_x = (int)Mathf.Clamp(gridPosition.x, 0, grid.Size.x);
        var index_y = (int)Mathf.Clamp(gridPosition.y, 0, grid.Size.y);

        var worldLeftDownPosition = size * -0.5f * cellsize + new Vector2(0.5f, 0.5f) * cellsize;
        var worldPosition = worldLeftDownPosition + new Vector2(index_x, index_y) * cellsize;
        return worldPosition;
    }
}

/// <summary>
/// グリッドクラス
/// </summary>
public class Grid
{
    private GameObject gridObject;

    public Grid(GameObject gridObject)
    {
        this.gridObject = gridObject;
    }

    public Vector2Int Size { get; private set; }

    public float CellSize { get; private set; }

    public void SetSize(Vector2Int size)
    {
        Size = size;
        var scalex = size.x;
        var scaley = size.y;

        gridObject.transform.localScale = new Vector3(scalex, 1, scaley);
    }

    public void SetCellSize(float cellSize)
    {
        CellSize = cellSize;
        SetSize(Size);
    }
}

/// <summary>
/// グリッドデータ読み込みクラス
/// </summary>
public class GridPrefabLoader : LoaderBase<GameObject>
{
}