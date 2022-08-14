using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// タイルデータ
/// </summary>
[CreateAssetMenu(menuName = "TileData")]
public class TileData : ScriptableObject
{
    [SerializeField]
    private GameObject prefab;

    [SerializeField]
    private float tileSize;

    public GameObject Prefab => prefab;

    public float TileSize => tileSize;
}
