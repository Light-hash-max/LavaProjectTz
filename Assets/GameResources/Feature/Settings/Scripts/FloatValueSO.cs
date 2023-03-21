using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// —ќшка дл€ float
/// </summary>
[CreateAssetMenu(fileName = "New FloatValueSO", menuName = "LavaProject/FloatValueSO")]
public class FloatValueSO : ScriptableObject
{
    [field: SerializeField]
    public float Value { get; private set; } = default;
}
