using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Завод
/// </summary>
public class Spot : MonoBehaviour
{
    [SerializeField]
    private ResourceObject _neededResource = default;
    [SerializeField]
    private ResourceObject _createdRecource = default;
    [SerializeField]
    private float _startDeleyWork = 3f;
    [SerializeField]
    private float _workTime = 1f;
    [SerializeField]
    private Transform _parent = default;
    [SerializeField]
    private Transform _position = default;

    private PlayerResources _playerResources = null;
    private int _countRecources = 0;
    private List<GameObject> _pool = new List<GameObject>();
    private Coroutine _workCoroutine = null;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerResources>(out _playerResources))
        {
            if(!_playerResources.GetComponent<PlayerMovement>().IsMoving && _playerResources.ResourceObjects.ContainsKey(_neededResource.Name) && !_playerResources.IsGettingRecources)
            {
                _countRecources += _playerResources.ResourceObjects[_neededResource.Name].Count;
                if (_workCoroutine != null)
                {
                    StopCoroutine(_workCoroutine);
                    _workCoroutine = null;
                }
                _workCoroutine = StartCoroutine(StartWork());

                _playerResources.StartRemoveResources(_neededResource.Name, transform);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<PlayerResources>(out _playerResources))
        {
            _playerResources = null;
        }
    }

    private GameObject GetFromPool()
    {
        foreach (GameObject poolObject in _pool)
        {
            if (!poolObject.activeSelf)
            {
                poolObject.SetActive(true);
                return poolObject;
            }
        }

        GameObject spotObject = Instantiate(_createdRecource.Prefab, _parent);
        _pool.Add(spotObject);
        return spotObject;
    }

    private IEnumerator StartWork()
    {
        yield return new WaitForSeconds(_startDeleyWork);

        while (_countRecources > 0)
        {
            GameObject spotObject = GetFromPool();
            spotObject.transform.position = _position.position;
            _countRecources--;
            yield return new WaitForSeconds(_workTime);
        }
    }

    private void Update()
    {
        if (_playerResources != null)
        {
            if (!_playerResources.GetComponent<PlayerMovement>().IsMoving &&
                _playerResources.ResourceObjects.ContainsKey(_neededResource.Name) &&
                !_playerResources.IsGettingRecources)
            {

                _countRecources += _playerResources.ResourceObjects[_neededResource.Name].Count;
                if (_workCoroutine != null)
                {
                    StopCoroutine(_workCoroutine);
                    _workCoroutine = null;
                }
                _workCoroutine = StartCoroutine(StartWork());

                _playerResources.StartRemoveResources(_neededResource.Name, transform);
            }
        }
    }
}
