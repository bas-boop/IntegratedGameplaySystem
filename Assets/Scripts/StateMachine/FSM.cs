using System.Collections.Generic;
using UnityEngine;

namespace StateMachine
{
    public sealed class FSM
    {
        private State _currentState;
        public readonly DictWrapper sharedData = new ();

        public FSM(IEnumerable<State> states)
        {
            foreach (State state in states)
            {
                sharedData.Set(state.GetType().Name, state);
            }
        }
        
        public void UpdateState()
        {
            if (_currentState != null)
                _currentState.DoUpdate();
        }
        
        public void FixedUpdateState()
        {
            if (_currentState != null)
                _currentState.DoFixedUpdate();
        }

        public void SwitchState(State targetState)
        {
            if (!targetState.isInit)
                targetState.Init(this, sharedData);
            
            _currentState?.DoExit();
            _currentState = targetState;
            _currentState.DoEnter();
        }
    }
}