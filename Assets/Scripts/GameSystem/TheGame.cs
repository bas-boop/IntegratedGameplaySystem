using System.Collections.Generic;
using UnityEngine;

using Test;

namespace GameSystem
{
    public sealed class TheGame : MonoBehaviour
    {
        private AObject _aObject;

        private List<IGameobject> _gameobjects = new ();
        
        private bool _isStarting = true;
    
        private void Update()
        {
            if (_isStarting)
            {
                OnStart();
                _isStarting = false;
            }
            
            foreach (IGameobject gameobject in _gameobjects)
            {
                gameobject.OnUpdate();
            }
        }

        private void OnStart()
        {
            CreateObjects();
            AddObjects();

            foreach (IGameobject gameobject in _gameobjects)
            {
                gameobject.OnStart();
            }
        }

        private void CreateObjects()
        {
            _aObject = new AObject();
        }

        private void AddObjects()
        {
            _gameobjects.Add(_aObject);
        }
    }
}
