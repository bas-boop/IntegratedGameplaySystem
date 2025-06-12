using System.Collections.Generic;
using UnityEngine;

using Gameplay.Collision;
using Gameplay.HealthSystem;
using GameSystem;
using StateMachine;
using Visuals;

namespace Gameplay.Enemies
{
    public class EnemyManager : IGameobject
    {
        private string _name = "Enemy";
        private string _visual = "EnemyVisual";

        private Vector2 _startPosition;
        
        private Rigidbody2D _rigidbody2D;
        private BoxCollider2D _boxCollider2D;
        private SpriteRenderer _spriteRenderer;
        private GameObject _thisGameObject;

        private BoxTrigger _collider;
        private Health _health;
        private FSM _fsm;
        private List<State> _states;
        
        public void OnStart()
        {
            CreateComponents();
            SetupComponents();
        }

        public void OnUpdate()
        {
            _fsm.UpdateState();
        }

        public void OnFixedUpdate()
        {
            _fsm.FixedUpdateState();
        }

        private void CreateComponents()
        {
            GameobjectComponentLibrary.CreateGameObject(_name);
            GameobjectComponentLibrary.CreateGameObject(_visual);
            GameobjectComponentLibrary.SetParent(_visual, _name);

            _rigidbody2D = GameobjectComponentLibrary.AddComponent<Rigidbody2D>(_name);
            _boxCollider2D = GameobjectComponentLibrary.AddComponent<BoxCollider2D>(_name);
            _collider = GameobjectComponentLibrary.AddComponent<BoxTrigger>(_name);
            _spriteRenderer = GameobjectComponentLibrary.AddComponent<SpriteRenderer>(_visual);
            
            _thisGameObject = GameobjectComponentLibrary.GetGameObject(_name);
            _thisGameObject.tag = Tags.ENEMY_TAG;

            _health = new (3);
            //_health.AddDamageListener(() => Debug.Log("pain"));
            _health.AddDieListener(Remove);
            
            _collider.AddListener(OnCol);

            _states = new List<State>()
            {
                new Idle(),
                new Wander(),
                new Attack(GameobjectComponentLibrary.GetGameObject("Player").transform),
            };
            _fsm = new (_states);
            
            _fsm.sharedData.Set("Idle", _states[0]);
            _fsm.sharedData.Set("Wander", _states[1]);
            _fsm.sharedData.Set("Attack", _states[2]);
            _fsm.sharedData.Set("Transform", _thisGameObject.transform);
            _fsm.sharedData.Set("Rb", _rigidbody2D);
            
            _fsm.SwitchState(_states[0]);
        }
        
        private void SetupComponents()
        {
            _rigidbody2D.gravityScale = 0;
            
            _boxCollider2D.size = Vector2.one;

            SpriteMaker.MakeSprite(_spriteRenderer, ShapeType.SQUARE, Color.red);

            _thisGameObject.transform.position = _startPosition;
        }

        private void OnCol(GameObject other)
        {
            _health.Damage(1);
        }

        private void Remove()
        {
            _fsm = null;
            _collider = null;
            GameobjectComponentLibrary.RemoveGameobject(_visual);
            GameobjectComponentLibrary.RemoveGameobject(_name);
        }
        
        public class EnemyBuilder
        {
            private readonly EnemyManager _enemy = new ();

            public EnemyBuilder SetName(string name)
            {
                _enemy._name = name;
                _enemy._visual = "Viual" + name;
                return this;
            }
            
            public EnemyBuilder SetStartPosition(Vector2 startPos)
            {
                _enemy._startPosition = startPos;
                return this;
            }

            public EnemyManager Build()
            {
                return _enemy;
            }
        }
    }
}