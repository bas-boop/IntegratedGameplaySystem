using UnityEngine;

namespace Gameplay.Collision
{
    public class BoxCollider : Collider
    {
        public Vector3 size = Vector3.one;

        public Bounds Bounds => new (transform.position, size);

        public override (bool, GameObject) IsColliding(Collider other)
        {
            (bool, GameObject) tuple = (false, null);
            
            switch (other)
            {
                case SphereCollider sphere:
                    sphere.IsColliding(this);
                    break;
                
                case BoxCollider box:
                    tuple = (Bounds.Intersects(box.Bounds), other.gameObject);
                    break;
            }
            
            if (tuple.Item1)
                OnCollision?.Invoke(other.gameObject);

            return tuple;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireCube(transform.position, size);
        }
    }
}