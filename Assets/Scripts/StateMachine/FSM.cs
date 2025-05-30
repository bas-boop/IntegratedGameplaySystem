namespace StateMachine
{
    public sealed class FSM
    {
        private State _currentState;
        public readonly DictWrapper sharedData = new ();

        public FSM(State[] states)
        {
            foreach (State state in states)
            {
                sharedData.Set(state.GetType().Name, state);
            }
        }
        
        public void UpdateState() => _currentState.DoUpdate();

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