using UnityEngine;

using Event;
using GameSystem;

namespace Player
{
    public class PlayerMovement : MonoBehaviour, IGameobject
    {
        private const float SPEED = 5f;
        private const float ROTATION_SPEED = 5f;
        private const float MAX_SPEED = 10f;
        
        private Vector2 _input;
        private Rigidbody2D _rigidbody2D;
        private Transform _transform;

        public void SetInput(Vector2 input)
        {
            _input = input;
            
            if (input != Vector2.zero)
                EventObserver.InvokeEvent(ObserverEventType.GAME_BEGIN);
        }

        public void OnStart()
        {
            _transform = GameobjectComponentLibrary.GetGameObject("Player").transform;
            _rigidbody2D = GameobjectComponentLibrary.GetGameObject("Player").GetComponent<Rigidbody2D>();
        }

        public void OnUpdate() { }

        public void OnFixedUpdate()
        {
            if (_rigidbody2D == null)
                return;
            
            _rigidbody2D.AddForce(_input * SPEED, ForceMode2D.Force);
            
            if (_rigidbody2D.linearVelocity.magnitude > MAX_SPEED)
                _rigidbody2D.linearVelocity = _rigidbody2D.linearVelocity.normalized * MAX_SPEED;

            if (_input.sqrMagnitude <= 0.01f)
                return;
            
            float angle = Mathf.Atan2(_input.y, _input.x) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(0f, 0f, angle - 90);
            _transform.rotation = Quaternion.Lerp(_transform.rotation, targetRotation, ROTATION_SPEED * Time.fixedDeltaTime);
        }
    }
}