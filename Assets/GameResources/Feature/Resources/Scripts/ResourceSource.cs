using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Источник ресурсов
/// </summary>
public class ResourceSource : MonoBehaviour
{
    /// <summary>
    /// Текущий номер фазы
    /// </summary>
    public int CurrentPhase { get; private set; } = default;

    [field: SerializeField]
    public ResourceObject _resourceObject = default;
    [SerializeField]
    private float _spawnTime = 1f;
    [SerializeField]
    private float _recoveryTime = 4f;
    [SerializeField]
    private float _recoveryValuePerTime = 0.1f;
    [SerializeField]
    private Transform _spawnPorision = default;
    [SerializeField]
    private float _radious = 3f;
    [SerializeField]
    private float _height = 4f;
    [SerializeField]
    private float[] _phasesScale = { 0.5f, 0.8f, 1f };
    [SerializeField]
    private int _phasePrefabCount = 2;
    [SerializeField]
    private Transform _parent = default;

    private float[] randomSign = { 1, -1 };
    private Vector3 _spawnDirection = default;
    private float x = default;
    private float z = default;
    private Coroutine _spawnCoroutine = null;
    private Coroutine _recoveryCoroutine = null;
    private int _currentPhaseNumber = 0;
    private float _recoveryValue = 0f;
    private List<GameObject> _pool = new List<GameObject>();

    private GameObject _prefab = default;

    private void Start() => CurrentPhase = _phasesScale.Length - 1;

    /// <summary>
    /// Начать спаун
    /// </summary>
    public void StartSpawn()
    {
        StopRecovery();
        StopSpawn();
        _spawnCoroutine = StartCoroutine(SpawnCoroutine());
    }

    private IEnumerator SpawnCoroutine()
    {
        while (enabled && CurrentPhase > 0)
        {
            yield return new WaitForSeconds(_spawnTime);
            GetResources();
            _currentPhaseNumber++;
            if (_currentPhaseNumber >= _phasePrefabCount && CurrentPhase > 0)
            {
                DecreaseResource();
                _currentPhaseNumber = 0;
                if (CurrentPhase <= 0)
                {
                    StopSpawn();
                }
            }
        }

        StopSpawn();
    }

    private void StartRecovery()
    {
        StopRecovery();
        _recoveryCoroutine = StartCoroutine(Recovery());
    }

    private void StopRecovery()
    {
        if (_recoveryCoroutine != null)
        {
            StopCoroutine(_recoveryCoroutine);
            _recoveryCoroutine = null;
        }
        _recoveryValue = 0f;
    }

    private IEnumerator Recovery()
    {
        while (enabled)
        {
            yield return new WaitForSeconds(_recoveryTime * _recoveryValuePerTime);
            _recoveryValue += _recoveryValuePerTime;

            if (_recoveryValue >= 1)
            {
                IncreaseResource();
                _recoveryValue = 0F;
            }

            if (CurrentPhase >= _phasesScale.Length - 1)
            {
                StopRecovery();
            }
        }
    }

    private void DecreaseResource()
    {
        CurrentPhase--;
        LeanTween.scale(gameObject, Vector3.one * _phasesScale[CurrentPhase], _spawnTime / 2).setEase(LeanTweenType.easeInOutSine);
    }

    private void IncreaseResource()
    {
        CurrentPhase++;
        LeanTween.scale(gameObject, Vector3.one * _phasesScale[CurrentPhase], _spawnTime / 2).setEase(LeanTweenType.easeInOutSine);
    }

    /// <summary>
    /// Остановить спаун
    /// </summary>
    public void StopSpawn()
    {
        if (_spawnCoroutine != null)
        {
            StopCoroutine(_spawnCoroutine);
            _spawnCoroutine = null;
        }
        if (CurrentPhase < _phasesScale.Length - 1)
        {
            StartRecovery();
        }
    }

    private void GetObject()
    {
        foreach (GameObject poolObject in _pool)
        {
            if (!poolObject.activeSelf)
            {
                poolObject.SetActive(true);
                _prefab = poolObject;
                return;
            }
        }

        _prefab = Instantiate(_resourceObject.Prefab, _parent);
        _pool.Add(_prefab);
    }

    private void GetResources()
    {
        GetObject();
        _prefab.transform.position = _spawnPorision.position;

        x = Random.Range(0, _radious);
        z = Mathf.Sqrt(_radious * _radious - x * x);
        _spawnDirection = Vector3.up * _height + Vector3.right * x * randomSign[Random.Range(0, randomSign.Length)]
                                            + Vector3.forward * z * randomSign[Random.Range(0, randomSign.Length)];

        _prefab.GetComponent<Rigidbody>().velocity = _spawnDirection;
    }
}
