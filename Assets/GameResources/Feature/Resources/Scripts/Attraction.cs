using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Притягивает ресурс и игроку и исчезает
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class Attraction : MonoBehaviour
{
    [field: SerializeField]
    public ResourceObject _resourceObject = default;
    [SerializeField]
    public float _availableTime = 3f;

    private Rigidbody _rigidbody = default;
    private PlayerResources _playerResources = default;
    private bool _isAvailable = false;
    private bool _isAdded = false;

    private void Awake() => _rigidbody = GetComponent<Rigidbody>();

    private void OnEnable()
    {
        _rigidbody.velocity = Vector3.zero;
        _isAvailable = false;
        _isAdded = false;
        StartCoroutine(AvalibaleTime());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerResources>(out _playerResources) && _isAvailable && !_isAdded)
        {
            _rigidbody.AddForce(((_playerResources.transform.position - transform.position).normalized + Vector3.up) * 200f);
            _playerResources.AddResources(_resourceObject);
            StartCoroutine(DisableCoroutine());
            _isAdded = true;
        }
    }

    private IEnumerator DisableCoroutine()
    {
        yield return new WaitForSeconds(0.2f);
        gameObject.SetActive(false);
    }

    private IEnumerator AvalibaleTime()
    {
        yield return new WaitForSeconds(_availableTime);
        _isAvailable = true;
    }
}
