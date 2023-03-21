using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Ресурс
/// </summary>
[CreateAssetMenu(fileName = "New Resource", menuName = "LavaProject/Resources Data")]
public class ResourceObject : ScriptableObject
{
    [field: SerializeField]
    public string Name = default;
    [field: SerializeField]
    public GameObject Prefab = default;
    [field: SerializeField]
    public Sprite Icon = default;
}
