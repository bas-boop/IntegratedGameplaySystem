using UnityEngine;

namespace StateMachine
{
    public class TestFSM
    {
        private FSM fsm;
        private IdleState idleState;

        public void Yes()
        {
            idleState = new IdleState();
            
            fsm = new FSM(new State[]{idleState});
            var s = fsm.sharedData.Get<IdleState>("IdleState");
            Debug.Log(s);
            fsm.sharedData.Log();
        }
    }
}