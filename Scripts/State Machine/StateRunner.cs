using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class StateRunner<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] private List<State<T>> _states;

    public State<T> _activeState;
    private T _parent;

    protected virtual void Awake()
    {
        _parent = GetComponent<T>();
    }

    protected virtual void Start()
    {
        if (_states.Count > 0)
        {
            var copy = ScriptableObject.Instantiate(_states[0]);
            SetState(copy);
        }
    }

    public void SetState(State<T> newStateType)
    {
        _activeState?.Exit();

        _activeState = newStateType;
        _activeState?.Enter(_parent);
    }

    protected virtual void Update()
    {
        _activeState?.Tick(Time.deltaTime);
        _activeState?.ChangeState();
    }

    private void FixedUpdate()
    {
        _activeState?.FixedTick(Time.fixedDeltaTime);
    }
}