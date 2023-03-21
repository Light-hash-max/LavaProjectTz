using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Зона, в которой перс начинает атаковать
/// </summary>
[RequireComponent(typeof(ResourceSource))]
public class AttackingArea : MonoBehaviour
{
    private PlayerMovement _playerMovement = default;
    private ResourceSource _resourceSource = default;
    private bool _isAttacking = false;

    private void Awake() => _resourceSource = GetComponent<ResourceSource>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerMovement>(out _playerMovement))
        {
            _playerMovement.IsAttacking = _resourceSource.CurrentPhase > 0;
            _playerMovement.onStartAttacking += ChangeAttackingState;
        }
    }

    private void ChangeAttackingState(bool isAttacking)
    {
        if (_isAttacking != isAttacking)
        {
            _isAttacking = isAttacking;
            if (isAttacking)
            {
                _resourceSource.StartSpawn();
            }
            else
            {
                _resourceSource.StopSpawn();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<PlayerMovement>(out _playerMovement))
        {
            _playerMovement.onStartAttacking -= ChangeAttackingState;
            _playerMovement.IsAttacking = false;
            ChangeAttackingState(false);
        }
    }
}
