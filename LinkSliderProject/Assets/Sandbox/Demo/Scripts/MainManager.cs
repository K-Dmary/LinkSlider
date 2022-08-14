using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// エントリポイント
/// </summary>
public class MainManager : MonoBehaviour
{
    // Start is called before the first frame update
    async void Start()
    {
        MapManager.Create(this.gameObject);
        await MapManager.instance.SetUp();
        await MapManager.instance.LoadMap("MapData_Demo");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
