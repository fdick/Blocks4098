using System;
using System.Collections.Generic;

namespace Code.StateMachine
{
    public class StateMachine
    {
        protected Dictionary<Type, IState> _states;
        protected IState _currentState;

        public StateMachine(params IState[] states)
        {
            _states = new Dictionary<Type, IState>();
            foreach (var s in states)
            {
                _states.TryAdd(s.GetType(), s);
            }
        }

        public void Enter<T>()
        {
            if (_states.TryGetValue(typeof(T), out var outState))
            {
                _currentState?.Exit();

                _currentState = outState;
                outState.Enter();
            }
            else
            {
                throw new KeyNotFoundException();
            }
        }
    }
}