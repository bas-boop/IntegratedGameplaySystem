using System;
using System.Collections.Generic;

namespace Event
{
    public static class EventObserver
    {
        private static readonly Dictionary<ObserverEventType, Action> eventDict = new();

        public static void AddListener(ObserverEventType type, Action func)
        {
            eventDict.TryAdd(type, null); // checks of the type is already in the dict or not
            eventDict[type] += func;
        }
        
        public static void RemoveListener(ObserverEventType type, Action func)
        {
            if (eventDict.ContainsKey(type)
                && eventDict[type] != null)
                eventDict[type] -= func;
        }
        
        public static void InvokeEvent(ObserverEventType type)
        {
            if (eventDict.TryGetValue(type, out Action value))
                value?.Invoke();
        }
    }
}