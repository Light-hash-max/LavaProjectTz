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
    [field: SerializeField]
    public VariableJoystick Joystick { get; private set; } = default;

    private bool _isMoving => Joystick.Horizontal != 0 || Joystick.Vertical != 0;

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
        _movementPosition = _isMoving ? new Vector3(-Joystick.Horizontal, 0, -Joystick.Vertical) : Vector3.zero;
        _stateManager.SwitchState(_isMoving ? _stateManager._walkingState : _stateManager._idleState);

        _navMeshAgent.SetDestination(transform.position + _movementPosition);
    }
}
