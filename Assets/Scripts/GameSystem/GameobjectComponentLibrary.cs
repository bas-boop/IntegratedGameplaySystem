using System.Collections.Generic;
using UnityEngine;

namespace GameSystem
{
    public sealed class GameobjectComponentLibrary : MonoBehaviour
    {
        private static readonly Dictionary<string, GameObject> _gameObjects = new ();

        public static T AddComponent<T>(string gameObjectName) where T : Component
        {
            if (_gameObjects.ContainsKey(gameObjectName))
            {
                return _gameObjects[gameObjectName].AddComponent<T>();;
            }
            
            return null;
        }

        public static void CreateGameObject(string gameObjectName)
        {
            GameObject newGameObject = new (gameObjectName);
            _gameObjects.Add(newGameObject.name, newGameObject);
        }
    }
}