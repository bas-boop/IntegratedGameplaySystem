using GameSystem;
using UnityEngine;
using Visuals;

namespace Gameplay.Enemies
{
    public class EnemyManager : IGameobject
    {
        private string _name = "Enemy";
        private string _visual = "PlayerVisual";

        private Vector2 _startPosition;
        
        private Rigidbody2D _rigidbody2D;
        private BoxCollider2D _boxCollider2D;
        private SpriteRenderer _spriteRenderer;

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
            _spriteRenderer = GameobjectComponentLibrary.AddComponent<SpriteRenderer>(_visual);
            
            _thisGameObject = GameobjectComponentLibrary.GetGameObject(_name);
        }
        
        private void SetupComponents()
        {
            _rigidbody2D.gravityScale = 0;
            
            _boxCollider2D.size = Vector2.one;

            SpriteMaker.MakeSprite(_spriteRenderer, ShapeType.SQUARE, Color.red);

            _thisGameObject.transform.position = _startPosition;
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