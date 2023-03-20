using UnityEngine;

/// <summary>
/// Основное состояние
/// </summary>
public abstract class BaseState
{
    protected Animator _animator = default;

    /// <summary>
    /// Вход в состояние
    /// </summary>
    /// <param name="sparrow"></param>
    public virtual void EnterState(StateManager stateManager) => _animator = stateManager.Animator;
}
