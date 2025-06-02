using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameSystem
{
    public sealed class GameobjectComponentLibrary : MonoBehaviour
    {
        private static readonly Dictionary<string, GameObject> _gameObjects = new ();

        public static GameObject GetGameObject(string gameObjectName)
        {
            if (_gameObjects.ContainsKey(gameObjectName))
                return _gameObjects[gameObjectName];

            return CreateGameObject(gameObjectName);
        }
        
        public static T AddComponent<T>(string gameObjectName) where T : Component
        {
            if (_gameObjects.ContainsKey(gameObjectName))
                return _gameObjects[gameObjectName].AddComponent<T>();;
            
            return null;
        }

        public static GameObject CreateGameObject(string gameObjectName)
        {
            GameObject newGameObject = new (gameObjectName);
            _gameObjects.Add(newGameObject.name, newGameObject);
            return _gameObjects.Last().Value;
        }

        public static GameObject AddCamera()
        {
            Camera cam = FindFirstObjectByType<Camera>();
            _gameObjects.Add(cam.name, cam.gameObject);
            Debug.Log(_gameObjects.Last().Value.gameObject.name);
            return cam.gameObject;
        }

        public static void SetParent(string child, string parent)
        {
            if (!_gameObjects.ContainsKey(child)
                || !_gameObjects.ContainsKey(parent))
            {
                Debug.LogWarning($"{child} or {parent} does not exist - {nameof(GameobjectComponentLibrary)}");
                return;
            }
            
            _gameObjects[child].transform.SetParent(_gameObjects[parent].transform);
        }
    }
}