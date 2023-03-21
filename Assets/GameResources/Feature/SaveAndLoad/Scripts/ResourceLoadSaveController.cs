using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ”правл€ет сохаренением и загрузкой данных
/// </summary>
public class ResourceLoadSaveController : MonoBehaviour
{
    [SerializeField]
    private PlayerResources _playerResources = default;
    [SerializeField]
    private ResourceObject[] _resources = default;

    private Storage _storage = default;
    private GameData _gameData = default;
    private Dictionary<string, List<ResourceObject>> resourceObjects = new Dictionary<string, List<ResourceObject>>();
    private List<ResourceData> resourceDatas = new List<ResourceData>();

    private void OnEnable()
    {
        _storage = new Storage();

        resourceDatas.Clear();
        foreach (ResourceObject resource in _resources)
        {
            ResourceData resourceData = new()
            {
                ResourceName = resource.Name,
                ResorceCount = 0
            };
            resourceDatas.Add(resourceData);
        }

        _gameData = _storage.Load(new GameData(resourceDatas.ToArray())) as GameData;

        if (_gameData == null)
        {
            _gameData = new GameData(resourceDatas.ToArray());
        }

        foreach (ResourceData resourceData in _gameData.ResourceData)
        {
            resourceObjects.Add(resourceData.ResourceName, GetList(resourceData.ResourceName, resourceData.ResorceCount));
        }

        _playerResources.ChangeResources(resourceObjects);
    }

    private List<ResourceObject> GetList(string name, int count)
    {
        List<ResourceObject> newList = new List<ResourceObject>();
        foreach(ResourceObject resource in _resources)
        {
            if (resource.Name == name)
            {
                for (int i = 0; i < count; i++)
                {
                    newList.Add(resource);
                }

                return newList;
            }
        }

        return newList;
    }

    private void Awake() => _playerResources.onResourceCountChange += SaveData;

    private void OnDestroy() => _playerResources.onResourceCountChange -= SaveData;

    private void SaveData()
    {
        resourceDatas.Clear();
        foreach (ResourceObject resource in _resources)
        {
            ResourceData resourceData = new()
            {
                ResourceName = resource.Name
            };
            if (_playerResources.ResourceObjects.ContainsKey(resource.Name))
            {
                resourceData.ResorceCount = _playerResources.ResourceObjects[resource.Name].Count;
            }
            else
            {
                resourceData.ResorceCount = 0;
            }
            resourceDatas.Add(resourceData);
        }

        _gameData.ResourceData = resourceDatas.ToArray();

        _storage.Save(_gameData);
    }
}
