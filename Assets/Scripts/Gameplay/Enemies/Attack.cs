using UnityEngine;

using StateMachine;

namespace Gameplay.Enemies
{
    public sealed class Attack : State
    {
        private const float TIME_TO_CHASE = 15;
        private const float MAX_SPEED = 15;
        
        private Rigidbody2D _ourRb;
        private Transform _ourTransform;
        private Transform _attackTransform;
        private Timer _chaseTimer;

        public Attack(Transform attackTransform)
        {
            _attackTransform = attackTransform;
        }
        
        public override void DoEnter()
        {
            _ourRb = p_sharedData.Get<Rigidbody2D>("Rb");
            _ourTransform = p_sharedData.Get<Transform>("Transform");
            
            _chaseTimer = new (TIME_TO_CHASE);
            _chaseTimer.OnTimerDone += DoneChasing;
            _chaseTimer.Start();
        }

        public override void DoExit()
        {
            _ourRb.linearVelocity = Vector2.zero; 
        }

        public override void DoUpdate()
        {
            _chaseTimer.Tick(Time.deltaTime);
        }

        public override void DoFixedUpdate()
        {
            if (_attackTransform == null)
                return;

            _ourRb.AddForce(_attackTransform.position - _ourTransform.position);

            if (_ourRb.linearVelocity.magnitude > MAX_SPEED)
                _ourRb.linearVelocity = _ourRb.linearVelocity.normalized * MAX_SPEED;
        }

        private void DoneChasing()
        {
            _chaseTimer.OnTimerDone = null;
            _chaseTimer.Stop();
            
            p_owner.SwitchState(p_sharedData.Get<Idle>("Idle"));
        }
    }
}