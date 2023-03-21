using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// Ресурсы, собранные игроком
/// </summary>
public class PlayerResources : MonoBehaviour
{
    /// <summary>
    /// Изменение количетсво ресурсов
    /// </summary>
    public event Action onResourceCountChange = delegate { };

    /// <summary>
    /// Отдача ресурсов споту
    /// </summary>
    public event Action onResourceRemoving = delegate { };

    /// <summary>
    /// Собранные ресурсы
    /// </summary>
    public Dictionary<string, List<ResourceObject>> ResourceObjects { get; private set; } = new Dictionary<string, List<ResourceObject>>();

    /// <summary>
    /// Отдает ли ресурсы перс
    /// </summary>
    public bool IsGettingRecources { get; private set; } = false;

    private const float SPREAD = 0.5F;

    [SerializeField]
    private Transform _resourcePosition = default;
    [SerializeField]
    private Transform _resourceParent = default;
    [SerializeField]
    private float _removingResourceTime = 0.5f;
    [SerializeField]
    private float _removingResourceBetweenTime = 0.2f;
    [SerializeField]
    private float _startDelayTime = 0.2f;

    private GameObject _spotObject = default;
    private List<GameObject> _spotPool = new List<GameObject>();
    private List<GameObject> _currentSpotPool = new List<GameObject>();
    private Coroutine _removingResourcesCoroutine = null;

    /// <summary>
    /// Добавить ресурс
    /// </summary>
    /// <param name="resourceObject"></param>
    public void AddResources(ResourceObject resourceObject)
    {
        if (ResourceObjects.ContainsKey(resourceObject.Name))
        {
            ResourceObjects[resourceObject.Name].Add(resourceObject);
        }
        else
        {
            ResourceObjects.Add(resourceObject.Name, new List<ResourceObject> { resourceObject });
        }

        onResourceCountChange();
    }

    /// <summary>
    /// Изменить ресурсы
    /// </summary>
    /// <param name="newResources"></param>
    public void ChangeResources(Dictionary<string, List<ResourceObject>> newResources)
    {
        ResourceObjects = newResources;
        onResourceCountChange();
    }

    /// <summary>
    /// Начать отправлять ресурсы
    /// </summary>
    /// <param name="resourceName"></param>
    /// <param name="spotPosition"></param>
    public void StartRemoveResources(string resourceName, Transform spotPosition)
    {
        if(!IsGettingRecources)
        {
            onResourceRemoving();
            StopRemoveResources();
            IsGettingRecources = true;
            _removingResourcesCoroutine = StartCoroutine(Removing(resourceName, spotPosition));
        }
    }

    /// <summary>
    /// Остановить отправление ресурсов
    /// </summary>
    public void StopRemoveResources()
    {
        IsGettingRecources = false;
        if (_removingResourcesCoroutine != null)
        {
            StopCoroutine(_removingResourcesCoroutine);
            _removingResourcesCoroutine = null;
        }
    }

    private GameObject GetFromPool(string resourceName)
    {
        foreach(GameObject poolObject in _spotPool)
        {
            if (!poolObject.activeSelf && poolObject.GetComponent<SpotResource>().ResourceObject.Name == resourceName)
            {
                poolObject.SetActive(true);
                return poolObject;
            }
        }

        _spotObject = Instantiate(ResourceObjects[resourceName][0].SpotPrefab, _resourceParent);
        _spotPool.Add(_spotObject);
        return _spotObject;
    }

    private IEnumerator Removing(string resourceName, Transform spotPosition)
    {
        _currentSpotPool.Clear();
        while (ResourceObjects[resourceName].Count > 0)
        {
            GameObject spotObject = GetFromPool(resourceName);
            spotObject.transform.position = _resourcePosition.position + new Vector3(Random.Range(-SPREAD,SPREAD), Random.Range(-SPREAD, SPREAD), Random.Range(-SPREAD, SPREAD));
            _currentSpotPool.Add(spotObject);
            ResourceObjects[resourceName].RemoveAt(0);
            onResourceCountChange();
            yield return new WaitForSeconds(_removingResourceBetweenTime);
        }

        yield return new WaitForSeconds(_startDelayTime);

        foreach(GameObject poolObject in _currentSpotPool)
        {
            LeanTween.move(poolObject, spotPosition.position, _removingResourceTime);
            yield return new WaitForSeconds(_removingResourceBetweenTime);
        }
        StopRemoveResources();
    }
}
