using UnityEngine;

using StateMachine;

namespace Gameplay.Enemies
{
    public sealed class Idle : State
    {
        private const float TIME_TO_BE_IDLE = 5;
        
        private Timer _idleTimer;

        public override void DoEnter()
        {
            _idleTimer = new (TIME_TO_BE_IDLE);
            _idleTimer.OnTimerDone += DoSomeThing;
            _idleTimer.Start();
        }

        public override void DoExit() { }

        public override void DoUpdate() => _idleTimer.Tick(Time.deltaTime);
        
        public override void DoFixedUpdate() { }

        private void DoSomeThing()
        {
            _idleTimer.OnTimerDone = null;
            _idleTimer.Stop();

            if (CoinFlip.Flip())
                owner.SwitchState(sharedData.Get<Wander>("Wander"));
            else
                owner.SwitchState(sharedData.Get<Attack>("Attack"));
        }
    }
}