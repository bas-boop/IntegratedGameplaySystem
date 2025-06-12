using UnityEngine;

using GameSystem;
using Gameplay.Collision;
using ObjectPooling;
using Visuals;

namespace Gameplay.Shooter
{
    public class Bullet : MonoBehaviour, IPoolable<Bullet>
    {
        private float _speed = 5;
        private float _despawnTime = 20;
        private SphereTrigger _collider;
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
            
            _collider = GameobjectComponentLibrary.AddComponent<SphereTrigger>(Name);
            _collider.radius = 0.15f;
            _collider.AddListener(OnCollide);
            _collider.enabled = false;

            tag = Tags.BULLET_TAG;
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
            _collider.enabled = true;
            
            Vector2 moveDirection = transform.up * _speed;
            _rb.AddForce(moveDirection, ForceMode2D.Impulse);
            
            Invoke(nameof(ReturnToPool), _despawnTime);
        }

        public void Deactivate()
        {
            gameObject.SetActive(false);
            _collider.enabled = false;
            _rb.linearVelocity = Vector2.zero;
        }

        public void ReturnToPool()
        {
            ObjectPool.ReturnObject(this);
        }

        private void OnCollide(GameObject other)
        {
            if (other.CompareTag(Tags.ENEMY_TAG))
                ReturnToPool();
        }
    }
}