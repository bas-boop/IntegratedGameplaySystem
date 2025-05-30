namespace StateMachine
{
    public abstract class State
    {
        public FSM owner;
        public bool isInit;
        public DictWrapper sharedData;
        
        public abstract void DoEnter();
        public abstract void DoExit();
        public abstract void DoUpdate();

        public void Init(FSM owner, DictWrapper sharedData)
        {
            this.owner = owner;
            this.sharedData = sharedData;
            isInit = true;
        }
    }
}