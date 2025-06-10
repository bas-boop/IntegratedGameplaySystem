using Gameplay.Collision;
using UnityEngine;

using GameSystem;
using ObjectPooling;
using Visuals;

namespace Gameplay.Shooter
{
    public class Bullet : MonoBehaviour, IPoolable<Bullet>
    {
        private SphereColliderX _collider;
        private SpriteRenderer _spriteRenderer;
        private Rigidbody2D _rb;

        public ObjectPool<Bullet> ObjectPool { get; set; }
        public string Name { get; set; }

        public void Create(ObjectPool<Bullet> objectPool, string name)
        {
            ObjectPool = objectPool;
            Name = name;
            
            GameobjectComponentLibrary.CreateGameObject(Name);
            GameobjectComponentLibrary.AddComponent<Bullet>(Name);
            SpriteRenderer spriteRenderer = GameobjectComponentLibrary.AddComponent<SpriteRenderer>(Name);
            SpriteMaker.MakeSprite(spriteRenderer, ShapeType.CIRCLE, Color.blue);
            GameobjectComponentLibrary.GetGameObject(Name).transform.localScale = Vector3.one * 0.3f;
            
            _rb = GameobjectComponentLibrary.AddComponent<Rigidbody2D>(Name);
            _rb.gravityScale = 0;
            
            _collider = GameobjectComponentLibrary.AddComponent<SphereColliderX>(Name);
            _collider.radius = 0.15f;
            _collider.AddListener(OnCollide);
        }

        public void Delete()
        {
            GameobjectComponentLibrary.RemoveGameobject(Name);
            Destroy(gameObject);
        }

        public void Activate(Vector3 position, Quaternion rotation)
        {
            transform.position = position;
            transform.rotation = rotation;
            
            gameObject.SetActive(true);
            _rb.linearVelocityY = 5;
        }

        public void Deactivate()
        {
            gameObject.SetActive(false);
            _rb.linearVelocityY = 0;
        }

        public void ReturnToPool()
        {
            ObjectPool.ReturnObject(this);
        }

        private void OnCollide(GameObject other)
        {
            if (other.name == "TheSquareEnemy")
                ReturnToPool();
        }
    }
}