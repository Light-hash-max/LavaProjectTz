using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Атака
/// </summary>
public class AttackingState : BaseState
{
    private string TRIGGER_NAME = "Punch";
    public override void EnterState(StateManager stateManager)
    {
        base.EnterState(stateManager);
        _animator.SetTrigger(TRIGGER_NAME);
    }

}