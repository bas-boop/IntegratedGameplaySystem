using System;
using UnityEngine;

namespace Gameplay.Collision
{
    public class SphereCollider : Collider
    {
        public float radius = 0.5f;

        public override (bool, GameObject) IsColliding(Collider other)
        {
            (bool, GameObject) tuple = (false, null);
            
            switch (other)
            {
                case SphereCollider sphere:
                    float dist = Vector3.Distance(transform.position, sphere.transform.position);
                    tuple = (dist < radius + sphere.radius, other.gameObject);
                    break;
                
                case BoxCollider box:
                    Vector3 closest = Vector3.Max(box.Bounds.min,
                                  Vector3.Min(transform.position, box.Bounds.max));
                    float distSq = (closest - transform.position).sqrMagnitude;
                    tuple = (distSq < radius * radius, other.gameObject);
                    break;
            }

            if (tuple.Item1)
            {
                OnCollision?.Invoke(other.gameObject);
            }

            return tuple;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position, radius);
        }
    }
}