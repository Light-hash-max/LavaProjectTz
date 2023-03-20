using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Состояние спокойствия
/// </summary>
public class IdleState : BaseState
{
    private string TRIGGER_NAME = "Idle";

    public override void EnterState(StateManager stateManager)
    {
        base.EnterState(stateManager);
        _animator.SetTrigger(TRIGGER_NAME);
    }

}