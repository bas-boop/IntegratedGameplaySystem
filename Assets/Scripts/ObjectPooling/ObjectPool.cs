#nullable enable

using System.Collections.Generic;
using UnityEngine;

using GameSystem;

namespace ObjectPooling
{
    public class ObjectPool<T> : IGameobject where T : MonoBehaviour, IPoolable<T>
    {
        private const int INITIAL_SIZE = 10;
        
        private T _prefab;
        private int _countPool;
        
        private readonly Queue<T> _objectQueue = new ();
        private readonly List<T> _dequeuedObjects = new ();
        private Transform _parentTransform;

        public ObjectPool(Transform parent, T prefab)
        {
            _parentTransform = parent;
            _prefab = prefab;
        }
        
        public void OnStart() => CreateObjects(INITIAL_SIZE, null);

        public void OnUpdate() {}

        public void OnFixedUpdate() {}

        /// <summary>
        /// Retrieves an object from the pool and positions it at the specified location.
        /// If no objects are available, it creates one more object.
        /// </summary>
        /// <param name="position">The position where the object will be placed.</param>
        /// <param name="rotation">The rotation of the object.</param>
        /// <param name="targetParent">An optional parent for the object.</param>
        /// <returns>The pooled object.</returns>
        public T GetObject(
            Vector3 position,
            Quaternion rotation,
            Transform targetParent)
        {
            T pooledObject = _objectQueue.Count == 0 
                ? CreateObject(targetParent)
                : _objectQueue.Dequeue();
            
            pooledObject.Activate(position, rotation);
            _dequeuedObjects.Add(pooledObject);
            return pooledObject;
        }

        /// <summary>
        /// Retrieves a specified number of objects from the pool and positions them at the specified location.
        /// If not enough objects are available, it creates more objects.
        /// </summary>
        /// <param name="amount">The number of objects to retrieve from the pool.</param>
        /// <param name="position">The position where the objects will be placed.</param>
        /// <param name="rotation">The rotation of the objects.</param>
        /// <param name="targetParent">An optional parent for the objects.</param>
        /// <returns>A list of pooled objects.</returns>
        public List<T> GetObjects(
            int amount,
            Vector3 position,
            Quaternion rotation,
            Transform targetParent)
        {
            List<T> pooledObjects = new ();

            for (int i = 0; i < amount; i++)
            {
                T obj = GetObject(position, rotation, targetParent);
                pooledObjects.Add(obj);
            }

            return pooledObjects;
        }

        /// <summary>
        /// Returns an object to the pool.
        /// </summary>
        /// <param name="obj">The object to be returned to the pool.</param>
        public void ReturnObject(T obj)
        {
            if (_objectQueue.Contains(obj))
                return;
            
            obj.Deactivate();
            _objectQueue.Enqueue(obj);
            _dequeuedObjects.Remove(obj);
        }

        /// <summary>
        /// Returns a list of objects to the pool.
        /// </summary>
        /// <param name="objs">The list of objects to be returned to the pool.</param>
        public void ReturnObjects(List<T> objs)
        {
            foreach (T obj in objs)
                ReturnObject(obj);
        }

        /// <summary>
        /// Clears the current pool and make a new pool with the initial size.
        /// </summary>
        public void ResetPool()
        {
            int l = _objectQueue.Count;

            for (int i = l - 1; i >= 0; i--)
            {
                T obj = _objectQueue.Dequeue();
                obj.Delete();
            }
            
            foreach (T obj in _dequeuedObjects)
                obj.Delete();

            _objectQueue.Clear();
            _dequeuedObjects.Clear();
            CreateObjects(INITIAL_SIZE, null!);
        }
        
        private T CreateObject(Transform? targetParent)
        {
            GameObject obj = GameobjectComponentLibrary.CreateGameObject(_prefab.name + _countPool);
            T poolable = GameobjectComponentLibrary.AddComponent<T>(_prefab.name + _countPool);
            
            poolable.Create(this, _prefab.name + _countPool);
            _countPool++;
            obj.gameObject.SetActive(false);
            
            return GameobjectComponentLibrary.GetGameObjectComponent<T>();
        }

        private void CreateObjects(int amount, Transform targetParent)
        {
            for (int i = 0; i < amount; i++)
                _objectQueue.Enqueue(CreateObject(targetParent));
        }
    }
}