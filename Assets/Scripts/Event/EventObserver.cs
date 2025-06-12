using System;
using System.Collections.Generic;

namespace Event
{
    public static class EventObserver
    {
        private static readonly Dictionary<ObserverEventType, Action> _eventDict = new();

        public static void AddListener(ObserverEventType type, Action func)
        {
            _eventDict.TryAdd(type, null); // checks of the type is already in the dict or not
            _eventDict[type] += func;
        }
        
        public static void RemoveListener(ObserverEventType type, Action func)
        {
            if (_eventDict.ContainsKey(type)
                && _eventDict[type] != null)
            {
                _eventDict[type] -= func;
            }
        }
        
        public static void InvokeEvent(ObserverEventType type)
        {
            _eventDict[type]?.Invoke();
        }
    }
}