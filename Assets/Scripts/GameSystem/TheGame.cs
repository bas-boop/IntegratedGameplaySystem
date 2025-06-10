using System.Collections.Generic;
using Gameplay.Collision;
using UnityEngine;

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
        }

        private void CreateObjects()
        {
            _playerManager = new ();
            
            _enemyManager = new EnemyManager.EnemyBuilder()
                .SetName("TheSquareEnemy")
                .SetStartPosition(Vector2.one * 4)
                .Build();
            
            // temp
            GameobjectComponentLibrary.AddComponent<SphereColliderX>("yes");
            GameobjectComponentLibrary.AddComponent<BoxColliderX>("no");
            GameobjectComponentLibrary.AddComponent<BoxColliderX>("no2");
            GameobjectComponentLibrary.AddComponent<BoxColliderX>("no3");
            GameobjectComponentLibrary.AddComponent<BoxColliderX>("no4");
            GameobjectComponentLibrary.AddComponent<BoxColliderX>("no5");
            GameobjectComponentLibrary.AddComponent<BoxColliderX>("no6");
        }

        private void AddObjects()
        {
            _gameobjects.Add(_playerManager);
            _gameobjects.Add(_enemyManager);
        }

        private void UpdateCollision()
        {
            ColliderX[] allColliders = GameobjectComponentLibrary.GetGameObjectComponents<ColliderX>();

            for (int i = 0; i < allColliders.Length; i++)
            {
                ColliderX colliderA = allColliders[i];

                for (int j = i + 1; j < allColliders.Length; j++)
                {
                    ColliderX colliderB = allColliders[j];
                    colliderA.IsColliding(colliderB);
                }
            }
        }
    }
}
