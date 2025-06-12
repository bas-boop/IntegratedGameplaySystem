using System;
using UnityEngine;

namespace Gameplay.Collision
{
    public abstract class Trigger : MonoBehaviour
    {
        public Action<GameObject> OnCollision;
        public float WasActivetedTime = 1;
        
        public abstract (bool, GameObject) IsColliding(Trigger other);

        public void AddListener(Action<GameObject> collisionAction) => OnCollision += collisionAction;
        
        protected void TurnColliderBackOn()
        {
            this.enabled = true;
        }
        
        private void TurnColliderOff()
        {
            this.enabled = false;
            Invoke(nameof(TurnColliderBackOn), WasActivetedTime);
        }

        protected void OnCollisionSucces(Trigger other)
        {
            OnCollision?.Invoke(other.gameObject);
            other.OnCollision?.Invoke(gameObject);

            this.enabled = false;
            Invoke(nameof(TurnColliderBackOn), WasActivetedTime);
            other.TurnColliderOff();
        }
    }
}