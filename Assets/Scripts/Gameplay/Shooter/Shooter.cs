using UnityEngine;

namespace Gameplay.Shooter
{
    public sealed class Shooter : MonoBehaviour
    {
        private GameObject _bullet;
        private float _shootInterval = 0.5f;
        private bool _isShooting;
        private Transform _firePoint;
        
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
            //Instantiate(_bullet, _firePoint.position, _firePoint.rotation); // objectpooling
            Invoke(nameof(SetIsShooting), _shootInterval);
        }

        private void SetIsShooting()
        {
            _isShooting = false;
        }
    }
}