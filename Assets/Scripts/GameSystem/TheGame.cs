using System.Collections.Generic;
using UnityEngine;

using Event;
using Gameplay.Collision;
using Gameplay.Enemies;
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
            
            UpdateCollision();
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
            
            EventObserver.AddListener(ObserverEventType.GAME_BEGIN,
                () => GameobjectComponentLibrary.GetUiElement("Controls").alpha = 0);
            EventObserver.AddListener(ObserverEventType.GAME_END,
                () => GameobjectComponentLibrary.GetUiElement("GameLost").alpha = 1);
        }

        private void CreateObjects()
        {
            _playerManager = new ();
            
            _enemyManager = new EnemyManager.EnemyBuilder()
                .SetName("TheSquareEnemy")
                .SetStartPosition(Vector2.one * 4)
                .Build();
        }

        private void AddObjects()
        {
            _gameobjects.Add(_playerManager);
            _gameobjects.Add(_enemyManager);
        }

        private void UpdateCollision()
        {
            Trigger[] allColliders = GameobjectComponentLibrary.GetGameObjectComponents<Trigger>();

            for (int i = 0; i < allColliders.Length; i++)
            {
                Trigger colliderA = allColliders[i];

                for (int j = i + 1; j < allColliders.Length; j++)
                {
                    Trigger colliderB = allColliders[j];
                    
                    if (colliderA.enabled
                        && colliderB.enabled)
                        colliderA.IsColliding(colliderB);
                }
            }
        }
    }
}
