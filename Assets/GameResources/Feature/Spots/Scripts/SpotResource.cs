using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Ресурс для завода
/// </summary>
public class SpotResource : MonoBehaviour
{
    [field: SerializeField]
    public ResourceObject ResourceObject { get; private set; } = default;

    [SerializeField]
    private float _timeToDisable = 3f;

    private void OnEnable() => StartCoroutine(DisableObject());

    private IEnumerator DisableObject()
    {
        yield return new WaitForSeconds(_timeToDisable);
        gameObject.SetActive(false);
    }
}
