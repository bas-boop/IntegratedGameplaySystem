using UnityEngine;

namespace Gameplay.Collision
{
    public class BoxColliderX : ColliderX
    {
        public Vector3 size = Vector3.one;

        public Bounds Bounds => new (transform.position, size);

        public override (bool, GameObject) IsColliding(ColliderX other)
        {
            (bool, GameObject) tuple = (false, null);
            
            switch (other)
            {
                case SphereColliderX sphere:
                    sphere.IsColliding(this);
                    break;
                
                case BoxColliderX box:
                    tuple = (Bounds.Intersects(box.Bounds), other.gameObject);
                    break;
            }

            if (tuple.Item1) // checking the bool
            {
                OnCollision?.Invoke(other.gameObject);
                other.OnCollision?.Invoke(gameObject);
            }

            return tuple;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireCube(transform.position, size);
        }
    }
}