using UnityEngine;

namespace Gameplay.Collision
{
    public class BoxTrigger : Trigger
    {
        public Vector3 size = Vector3.one;

        public Bounds Bounds => new (transform.position, size);

        public override (bool, GameObject) IsColliding(Trigger other)
        {
            (bool, GameObject) tuple = (false, null);
            
            switch (other)
            {
                case SphereTrigger sphere:
                    sphere.IsColliding(this);
                    break;
                
                case BoxTrigger box:
                    tuple = (Bounds.Intersects(box.Bounds), other.gameObject);
                    break;
            }

            if (tuple.Item1) // checking the bool
                OnCollisionSucces(other);

            return tuple;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireCube(transform.position, size);
        }
    }
}