using UnityEngine;

using GameSystem;

namespace Player
{
    public sealed class CameraFollower : MonoBehaviour, IGameobject
    {
        private Transform _followTarget;
        private float _smoothSpeed = 5f;

        public void OnStart() { }

        public void OnUpdate() { }

        public void OnFixedUpdate()
        {
            if (!_followTarget)
                return;

            Vector3 targetPosition = new (_followTarget.position.x, _followTarget.position.y, -10);
            transform.position = Vector3.Lerp(transform.position, targetPosition, _smoothSpeed * Time.deltaTime);
        }

        public void SetObjectToFollow(Transform followTarget)
        {
            _followTarget = followTarget;
        }
    }
}