using StateMachine;
using UnityEngine;

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
            _ourRb = sharedData.Get<Rigidbody2D>("Rb");
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
                owner.SwitchState(sharedData.Get<Idle>("Idle"));
        }

        private void PickNewWanderPosition()
        {
            float x = Random.Range(_wanderArea.xMin, _wanderArea.xMax);
            float y = Random.Range(_wanderArea.yMin, _wanderArea.yMax);
            _positionToWanderTo = new (x, y);
            
            Debug.Log($"Current pos: {_ourRb.position}\nTo: {_positionToWanderTo}");
        }
    }
}