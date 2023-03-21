using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// —ќшка дл€ int
/// </summary>
[CreateAssetMenu(fileName = "New IntValueSO", menuName = "LavaProject/IntValueSO")]
public class IntValueSO : ScriptableObject
{
    [field: SerializeField]
    public int Value { get; private set; } = default;
}
