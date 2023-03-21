using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Передвижение персонажа
/// </summary>
[RequireComponent(typeof(NavMeshAgent),typeof(StateManager))]
public class PlayerMovement : MonoBehaviour
{
    /// <summary>
    /// Начало атаки
    /// </summary>
    public event Action<bool> onStartAttacking = delegate { };


    /// <summary>
    /// Атакует ли перс
    /// </summary>
    public bool IsAttacking = false;

    /// <summary>
    /// Двигается ли перс
    /// </summary>
    public bool IsMoving => Joystick.Horizontal != 0 || Joystick.Vertical != 0;

    [field: SerializeField]
    public VariableJoystick Joystick { get; private set; } = default;

    private NavMeshAgent _navMeshAgent = default;
    private StateManager _stateManager = default;

    private Vector3 _movementPosition = default;

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _stateManager = GetComponent<StateManager>();
    }

    private void Update()
    {
        _movementPosition = IsMoving ? new Vector3(-Joystick.Horizontal, 0, -Joystick.Vertical) : Vector3.zero;
        _stateManager.SwitchState(IsMoving ? _stateManager._walkingState : IsAttacking ? _stateManager._attackingState : _stateManager._idleState);

        onStartAttacking(!IsMoving && IsAttacking);

        _navMeshAgent.SetDestination(transform.position + _movementPosition);
    }
}
