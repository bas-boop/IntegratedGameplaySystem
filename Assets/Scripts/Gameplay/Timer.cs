using System;
    
namespace Gameplay
{
    public sealed class Timer
    {
        public Action OnTimerDone;
        
        private float _duration;
        private float _currentTime;
        private bool _isRunning;

        public Timer(float durationSeconds) => SetTime(durationSeconds);

        public void SetTime(float seconds)
        {
            _duration = seconds;
            _currentTime = seconds;
            _isRunning = false;
        }

        public void Start() => _isRunning = true;

        public void Stop() => _isRunning = false;

        public void Reset()
        {
            _currentTime = _duration;
            _isRunning = false;
        }

        public void Tick(float deltaTime)
        {
            if (!_isRunning
                || _currentTime <= 0)
                return;

            _currentTime -= deltaTime;

            if (_currentTime > 0)
                return;
            
            _currentTime = 0;
            _isRunning = false;
            OnTimerDone?.Invoke();
        }

        public float GetCurrentTime() => _currentTime;

        public bool IsDone() => _currentTime <= 0f;
    }

}