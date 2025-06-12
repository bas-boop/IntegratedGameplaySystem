using System;
using UnityEngine;

namespace Gameplay.Collision
{
    public abstract class Trigger : MonoBehaviour
    {
        public float wasActivetedTime = 1;
        
        private Action<GameObject> _onCollision;
        
        public abstract (bool, GameObject) IsColliding(Trigger other);

        public void AddListener(Action<GameObject> collisionAction) => _onCollision += collisionAction;
        
        protected void TurnColliderBackOn()
        {
            this.enabled = true;
        }
        
        private void TurnColliderOff()
        {
            this.enabled = false;
            Invoke(nameof(TurnColliderBackOn), wasActivetedTime);
        }

        protected void OnCollisionSucces(Trigger other)
        {
            _onCollision?.Invoke(other.gameObject);
            other._onCollision?.Invoke(gameObject);

            this.enabled = false;
            Invoke(nameof(TurnColliderBackOn), wasActivetedTime);
            other.TurnColliderOff();
        }
    }
}