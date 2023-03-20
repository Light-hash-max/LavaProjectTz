using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Управление состояниями
/// </summary>
[RequireComponent(typeof(Animator))]
public class StateManager : MonoBehaviour
{
    public Animator Animator { get; private set; } = default;

    public BaseState CurrentState = default;

    public IdleState _idleState = new IdleState();
    public AttackingState _attackingState = new AttackingState();
    public WalkingState _walkingState = new WalkingState();

    protected virtual void Start()
    {
        Animator = GetComponent<Animator>();
        CurrentState = _idleState;
        CurrentState.EnterState(this);
    }

    /// <summary>
    /// Изменить состояние
    /// </summary>
    /// <param name="newState"></param>
    public void SwitchState(BaseState newState)
    {
        if (newState != CurrentState)
        {
            CurrentState = newState;
            CurrentState.EnterState(this);
        }
    }
}
