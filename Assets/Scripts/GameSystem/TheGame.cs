using System.Collections.Generic;
using UnityEngine;

using Event;
using Gameplay.Collision;
using Gameplay.Enemies;
using Player;

namespace GameSystem
{
    public sealed class TheGame : MonoBehaviour
    {
        private PlayerManager _playerManager;
        private EnemyManager _enemyManager;
        private EnemyManager _enemyManager1;
        private EnemyManager _enemyManager2;
        private EnemyUi _enemyUi;

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

            _enemyUi = new (3, GameobjectComponentLibrary.GetUiElement("Enemies"));
            _enemyUi.UpdateUi();
            
            AddEvents();
        }

        private void CreateObjects()
        {
            _playerManager = new ();
            
            _enemyManager = new EnemyManager.EnemyBuilder()
                .SetName("TheSquareEnemy")
                .SetStartPosition(Vector2.one * 4)
                .SetSize(2)
                .Build();
            
            _enemyManager1 = new EnemyManager.EnemyBuilder()
                .SetName("TheSquareEnemy1")
                .SetStartPosition(Vector2.one * 25)
                .SetSize(5)
                .Build();
            
            _enemyManager2 = new EnemyManager.EnemyBuilder()
                .SetName("TheSquareEnemy2")
                .SetStartPosition(Vector2.one * -10)
                .SetSize(0.75f)
                .Build();
        }

        private void AddObjects()
        {
            _gameobjects.Add(_playerManager);
            _gameobjects.Add(_enemyManager);
            _gameobjects.Add(_enemyManager1);
            _gameobjects.Add(_enemyManager2);
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

        private void AddEvents()
        {
            EventObserver.AddListener(ObserverEventType.ENEMY_COUNT, () => _enemyUi.UpdateUi());
            
            EventObserver.AddListener(ObserverEventType.GAME_BEGIN,
                () => GameobjectComponentLibrary.GetUiElement("Controls").alpha = 0);
            
            EventObserver.AddListener(ObserverEventType.GAME_END_LOSE,
                () => GameobjectComponentLibrary.GetUiElement("GameLost").alpha = 1);
            
            EventObserver.AddListener(ObserverEventType.GAME_END_WON,
                () => GameobjectComponentLibrary.GetUiElement("GameLost").alpha = 1);
            EventObserver.AddListener(ObserverEventType.GAME_END_WON,
                () => GameobjectComponentLibrary.GetUiElement("GameLost").text = "You won");
        }
    }
}
