using System;
using UnityEngine;

namespace Gameplay.Collision
{
    public abstract class Collider : MonoBehaviour
    {
        public Action<GameObject> OnCollision;
        
        public abstract (bool, GameObject) IsColliding(Collider other);
    }
}