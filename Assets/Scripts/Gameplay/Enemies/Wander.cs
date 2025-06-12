using UnityEngine;

using StateMachine;

namespace Gameplay.Enemies
{
    public sealed class Wander : State
    {
        private const float SPEED = 10f;
        private const float ARRIVE_THRESHOLD = 0.5f;
        
        private Rigidbody2D _ourRb;
        private Rect _wanderArea;
        private Vector2 _positionToWanderTo;

        public override void DoEnter()
        {
            _ourRb = p_sharedData.Get<Rigidbody2D>("Rb");
            _wanderArea = new Rect(Vector2.one * -25, Vector2.one * 50);
            PickNewWanderPosition();
        }

        public override void DoExit()
        {
            _ourRb.linearVelocity = Vector2.zero; 
        }

        public override void DoUpdate() { }

        public override void DoFixedUpdate()
        {
            Vector2 currentPosition = _ourRb.position;
            Vector2 direction = (_positionToWanderTo - currentPosition).normalized;
            _ourRb.AddForce(direction * (SPEED * Time.deltaTime));

            if (Vector2.Distance(currentPosition, _positionToWanderTo) <= ARRIVE_THRESHOLD)
                p_owner.SwitchState(p_sharedData.Get<Idle>("Idle"));
        }

        private void PickNewWanderPosition()
        {
            float x = Random.Range(_wanderArea.xMin, _wanderArea.xMax);
            float y = Random.Range(_wanderArea.yMin, _wanderArea.yMax);
            _positionToWanderTo = new (x, y);
        }
    }
}