namespace StateMachine
{
    public abstract class State
    {
        public bool isInit;
        
        protected FSM p_owner;
        protected DictWrapper p_sharedData;
        
        public abstract void DoEnter();
        public abstract void DoExit();
        public abstract void DoUpdate();
        public abstract void DoFixedUpdate();

        public void Init(FSM owner, DictWrapper sharedData)
        {
            p_owner = owner;
            p_sharedData = sharedData;
            isInit = true;
        }
    }
}