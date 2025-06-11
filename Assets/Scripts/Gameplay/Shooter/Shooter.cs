using UnityEngine;

using GameSystem;
using ObjectPooling;
using Visuals;

namespace Gameplay.Shooter
{
    public sealed class Shooter : MonoBehaviour
    {
        private const string NAME = "Bullet";
        
        private Bullet _bullet;
        private float _shootInterval = 0.5f;
        private bool _isShooting;
        private Transform _firePoint;
        private ObjectPool<Bullet> _bulletPool;

        public void Init(Transform firePoint)
        {
            _firePoint = firePoint;
            
            GameobjectComponentLibrary.CreateGameObject(NAME);
            _bullet = GameobjectComponentLibrary.AddComponent<Bullet>(NAME);
            SpriteRenderer spriteRenderer = GameobjectComponentLibrary.AddComponent<SpriteRenderer>(NAME);
            SpriteMaker.MakeSprite(spriteRenderer, ShapeType.CIRCLE, Color.blue);
            GameobjectComponentLibrary.GetGameObject(NAME).transform.localScale = Vector3.one * 0.3f;

            _bulletPool = new (transform, _bullet);
            _bulletPool.OnStart();

            _bullet.gameObject.SetActive(false);
        }
        
        public void ActivateShoot()
        {
            if (_isShooting)
                return;
            
            _isShooting = true;
            Shoot();
        }
        
        private void Shoot()
        {
            _isShooting = true;
            _bulletPool.GetObject(_firePoint.position, _firePoint.rotation, null);
            //Instantiate(_bullet, _firePoint.position, _firePoint.rotation); // objectpooling
            Invoke(nameof(SetIsShooting), _shootInterval);
        }

        private void SetIsShooting()
        {
            _isShooting = false;
        }
    }
}