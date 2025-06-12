using UnityEngine;

using Gameplay.Collision;
using Gameplay.HealthSystem;
using GameSystem;
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
        private BoxTrigger _collider;
        private Health _health;

        private GameObject _thisGameObject;
        
        public void OnStart()
        {
            CreateComponents();
            SetupComponents();
        }

        public void OnUpdate()
        {
            
        }

        public void OnFixedUpdate()
        {
            
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