using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class LoaderBase <T>
{
    public Dictionary<string, T> datas;

    public LoaderBase()
    {
        datas = new Dictionary<string, T>();
    }

    public async UniTask<T> Load(string address)
    {
        if (datas.ContainsKey(address))
        {
            return datas[address];
        }

        var handler = Addressables.LoadAssetAsync<T>(address);
        var mapdata = await handler.Task;
        datas.Add(address, mapdata);
        return mapdata;
    }

    public void UnLoad(T mapdata)
    {
        if (datas.ContainsValue(mapdata))
        {
            Addressables.Release(mapdata);
        }
    }
}
