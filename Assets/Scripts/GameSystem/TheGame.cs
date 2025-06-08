using System.Collections.Generic;
using UnityEngine;

using Gameplay.Enemies;
using Player;
using StateMachine;
using Collider = Gameplay.Collision.Collider;
using BoxCollider = Gameplay.Collision.BoxCollider;
using SphereCollider = Gameplay.Collision.SphereCollider;

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
            GameobjectComponentLibrary.AddComponent<SphereCollider>("yes");
            GameobjectComponentLibrary.AddComponent<BoxCollider>("no");
            GameobjectComponentLibrary.AddComponent<BoxCollider>("no2");
            GameobjectComponentLibrary.AddComponent<BoxCollider>("no3");
            GameobjectComponentLibrary.AddComponent<BoxCollider>("no4");
            GameobjectComponentLibrary.AddComponent<BoxCollider>("no5");
            GameobjectComponentLibrary.AddComponent<BoxCollider>("no6");
        }

        private void AddObjects()
        {
            _gameobjects.Add(_playerManager);
            _gameobjects.Add(_enemyManager);
        }

        private void UpdateCollision()
        {
            Collider[] allColliders = GameobjectComponentLibrary.GetGameObjectComponents<Gameplay.Collision.Collider>();

            for (int i = 0; i < allColliders.Length; i++)
            {
                Collider colliderA = allColliders[i];
                Debug.Log(colliderA);

                for (int j = i + 1; j < allColliders.Length; j++)
                {
                    Collider colliderB = allColliders[j];
                    Debug.Log(colliderB);
                    
                    if (colliderA.IsColliding(colliderB).Item1)
                    {
                        Debug.Log(colliderA + " " + colliderB);
                    }
                }
            }
        }
    }
}
