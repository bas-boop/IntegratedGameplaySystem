using System.Collections.Generic;
using Player;
using UnityEngine;

using Test;

namespace GameSystem
{
    public sealed class TheGame : MonoBehaviour
    {
        private AObject _aObject;
        private PlayerManager _playerManager;

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
            _playerManager = new ();
        }

        private void AddObjects()
        {
            _gameobjects.Add(_aObject);
            _gameobjects.Add(_playerManager);
        }
    }
}
