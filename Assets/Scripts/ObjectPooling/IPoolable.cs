using UnityEngine;

namespace ObjectPooling
{
    public interface IPoolable<T> where T : MonoBehaviour, IPoolable<T>
    {
        public ObjectPool<T> ObjectPool { get; set; }
        public string Name { get; set; }

        public void Create(ObjectPool<T> objectPool, string name);
        
        public void Delete();
        
        /// <summary>
        /// Activates the pooled object, making it visible and operational.
        /// </summary>
        /// <param name="position">The position to place the object.</param>
        /// <param name="rotation">The rotation of the object.</param>
        public void Activate(Vector3 position, Quaternion rotation);
        
        /// <summary>
        /// Deactivates the pooled object, making it invisible and non-operational.
        /// </summary>
        public void Deactivate();
        
        /// <summary>
        /// Returns the pooled object back to its object pool for reuse.
        /// </summary>
        public void ReturnToPool();
    }
}