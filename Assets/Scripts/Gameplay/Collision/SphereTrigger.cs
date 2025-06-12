using UnityEngine;

namespace Gameplay.Collision
{
    public class SphereTrigger : Trigger
    {
        public float radius = 0.5f;

        public override (bool, GameObject) IsColliding(Trigger other)
        {
            (bool, GameObject) tuple = (false, null);
            
            switch (other)
            {
                case SphereTrigger sphere:
                    float dist = Vector3.Distance(transform.position, sphere.transform.position);
                    tuple = (dist < radius + sphere.radius, other.gameObject);
                    break;
                
                case BoxTrigger box:
                    Vector3 closest = Vector3.Max(box.Bounds.min,
                                  Vector3.Min(transform.position, box.Bounds.max));
                    float distSq = (closest - transform.position).sqrMagnitude;
                    tuple = (distSq < radius * radius, other.gameObject);
                    break;
            }

            if (tuple.Item1) // checking the bool
                OnCollisionSucces(other);

            return tuple;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position, radius);
        }
    }
}