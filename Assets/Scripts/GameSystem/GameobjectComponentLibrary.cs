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
        
        public static GameObject GetGameObject<T>() where T : Component
        {
            return _gameObjects.Values.FirstOrDefault(go => go != null && go.GetComponent<T>() != null);
        }

        public static GameObject[] GetGameObjects<T>() where T : Component
        {
            return _gameObjects.Values
                .Where(go => go != null && go.GetComponent<T>() != null)
                .ToArray();
        }
        
        public static T GetGameObjectComponent<T>() where T : Component
        {
            return _gameObjects.Values
                .Select(go => go.GetComponent<T>())
                .LastOrDefault(component => component != null);
        }
        
        public static T[] GetGameObjectComponents<T>() where T : Component
        {
            return _gameObjects.Values
                .Select(go => go.GetComponent<T>())
                .Where(component => component != null)
                .ToArray();
        }

        public static bool RemoveGameobject(string gameObjectName)
        {
            if (!_gameObjects.TryGetValue(gameObjectName, out GameObject o))
                return false;
            
            Destroy(o);
            return true;

        }
        
        public static T AddComponent<T>(string gameObjectName) where T : Component
        {
            if (_gameObjects.ContainsKey(gameObjectName))
                return _gameObjects[gameObjectName].AddComponent<T>();
            
            CreateGameObject(gameObjectName);
            return _gameObjects[gameObjectName].AddComponent<T>();
        }

        public static GameObject CreateGameObject(string gameObjectName)
        {
            if (_gameObjects.ContainsKey(gameObjectName))
                return _gameObjects[gameObjectName];
            
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