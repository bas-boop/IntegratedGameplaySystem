using TMPro;

using Event;

namespace Gameplay.Enemies
{
    public sealed class EnemyUi
    {
        private int _currentCount;
        private TMP_Text _text;
        
        public EnemyUi(int count, TMP_Text text)
        {
            _currentCount = count + 1;
            _text = text;
        }
        
        public void UpdateUi()
        {
            _currentCount--;

            _text.text = $"Enemies remaining: {_currentCount}";
            
            if (_currentCount == 0)
                EventObserver.InvokeEvent(ObserverEventType.GAME_END_WON);
        }
    }
}