using System;
using UnityEngine;

namespace Gameplay.Collision
{
    public abstract class ColliderX : MonoBehaviour
    {
        public Action<GameObject> OnCollision;
        
        public abstract (bool, GameObject) IsColliding(ColliderX other);

        public void AddListener(Action<GameObject> collisionAction) => OnCollision += collisionAction;
    }
}