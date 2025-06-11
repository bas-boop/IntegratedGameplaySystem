using System;

namespace Gameplay.HealthSystem
{
    public class Health
    {
        public int CurrentHealth { get; private set; }
        
        private readonly int _startHealth;

        private Action _onHeal;
        private Action _onDamage;
        private Action _onDie;

        public Health(int startHealth)
        {
            _startHealth = startHealth;
            CurrentHealth = startHealth;
        }

        public void AddHealListener(Action targetAction) => _onHeal += targetAction;

        public void AddDamageListener(Action targetAction) => _onDamage += targetAction;

        public void AddDieListener(Action targetAction) => _onDie += targetAction;

        public void AddHealth(int amount)
        {
            CurrentHealth += amount;

            if (CurrentHealth > _startHealth)
                CurrentHealth -= CurrentHealth - _startHealth;
            
            _onHeal?.Invoke();
        }

        public void RemoveHealth(int amount)
        {
            CurrentHealth -= amount;
            
            _onDamage?.Invoke();
            
            if (CurrentHealth <= 0)
                Die();
        }

        private void Die()
        {
            _onDie?.Invoke();

            _onHeal = null;
            _onDamage = null;
            _onDie = null;
        }
    }
}