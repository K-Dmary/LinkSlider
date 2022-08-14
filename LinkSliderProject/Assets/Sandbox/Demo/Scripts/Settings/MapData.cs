using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// マップデータ
/// </summary>
[CreateAssetMenu(menuName = "MapData")]
public class MapData : ScriptableObject
{
    [SerializeField]
    private string mapName;

    [SerializeField]
    private Vector2Int size;

    [SerializeField]
    private Vector2Int[] tiles;

    [SerializeField]
    private GameObject model;

    public string Name => mapName;

    public Vector2Int Size => size;

    public Vector2Int[] Tiles => tiles;

    public GameObject Model => model;
}
