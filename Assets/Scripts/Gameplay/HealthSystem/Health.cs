using System;

namespace Gameplay.HealthSystem
{
    public class Health
    {
        public int CurrentHealth { get; private set; }
        
        public int StartHealth { get; private set; }

        private Action _onHeal;
        private Action _onDamage;
        private Action _onDie;

        public Health(int startHealth)
        {
            StartHealth = startHealth;
            CurrentHealth = startHealth;
        }

        public void AddHealListener(Action targetAction) => _onHeal += targetAction;

        public void AddDamageListener(Action targetAction) => _onDamage += targetAction;

        public void AddDieListener(Action targetAction) => _onDie += targetAction;

        public void Heal(int amount)
        {
            CurrentHealth += amount;

            if (CurrentHealth > StartHealth)
                CurrentHealth -= CurrentHealth - StartHealth;
            
            _onHeal?.Invoke();
        }

        public void Damage(int amount)
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