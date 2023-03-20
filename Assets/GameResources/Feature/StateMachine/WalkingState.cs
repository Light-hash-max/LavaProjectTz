using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ױמהüבא
/// </summary>
public class WalkingState : BaseState
{
    private string TRIGGER_NAME = "Walk";
    public override void EnterState(StateManager stateManager)
    {
        base.EnterState(stateManager);
        _animator.SetTrigger(TRIGGER_NAME);
    }

}
