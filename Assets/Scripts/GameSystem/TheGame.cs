using System.Collections.Generic;
using Gameplay.Enemies;
using UnityEngine;

using Player;
using StateMachine;

namespace GameSystem
{
    public sealed class TheGame : MonoBehaviour
    {
        private PlayerManager _playerManager;
        private EnemyManager _enemyManager;

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

        private void FixedUpdate()
        {
            foreach (IGameobject gameobject in _gameobjects)
            {
                gameobject.OnFixedUpdate();
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

            TestFSM t = new TestFSM();
            t.Yes();
        }

        private void CreateObjects()
        {
            _playerManager = new ();
            
            _enemyManager = new EnemyManager.EnemyBuilder()
                .SetName("TheSquare")
                .SetStartPosition(Vector2.one * 4)
                .Build();
        }

        private void AddObjects()
        {
            _gameobjects.Add(_playerManager);
            _gameobjects.Add(_enemyManager);
        }
    }
}
