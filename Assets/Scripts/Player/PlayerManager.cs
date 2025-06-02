using System.Collections.Generic;
using UnityEngine;

using GameSystem;
using Visuals;

namespace Player
{
    public class PlayerManager : IGameobject
    {
        private const string NAME = "Player";
        private const string VISUAL = "PlayerVisual";
        
        private Rigidbody2D _rigidbody2D;
        private BoxCollider2D _boxCollider2D;
        private SpriteRenderer _spriteRenderer;
        private InputParser _inputParser;
        private PlayerMovement _playerMovement;
        
        private CameraFollower _cameraFollower;

        private GameObject _thisGameObject;
        
        public void OnStart()
        {
            CreateComponents();
            SetupComponents();
        }

        public void OnUpdate()
        {
            _inputParser.OnUpdate();
            _cameraFollower.OnUpdate();
        }

        public void OnFixedUpdate()
        {
            _inputParser.OnFixedUpdate();
            _cameraFollower.OnFixedUpdate();
        }

        private void CreateComponents()
        {
            GameobjectComponentLibrary.CreateGameObject(NAME);
            GameobjectComponentLibrary.CreateGameObject(VISUAL);
            GameobjectComponentLibrary.SetParent(VISUAL, NAME);

            _rigidbody2D = GameobjectComponentLibrary.AddComponent<Rigidbody2D>(NAME);
            _boxCollider2D = GameobjectComponentLibrary.AddComponent<BoxCollider2D>(NAME);
            _inputParser = GameobjectComponentLibrary.AddComponent<InputParser>(NAME);
            _playerMovement = GameobjectComponentLibrary.AddComponent<PlayerMovement>(NAME);
            _spriteRenderer = GameobjectComponentLibrary.AddComponent<SpriteRenderer>(VISUAL);
            
            _thisGameObject = GameobjectComponentLibrary.GetGameObject(NAME);
            GameobjectComponentLibrary.GetGameObject(VISUAL).transform.rotation = Quaternion.Euler(0, 0, 225);

            GameobjectComponentLibrary.AddCamera();
            _cameraFollower = GameobjectComponentLibrary.AddComponent<CameraFollower>("MainCamera");
        }

        private void SetupComponents()
        {
            _rigidbody2D.gravityScale = 0;

            SpriteMaker.MakeSprite(_spriteRenderer, ShapeType.TRIANGLE, Color.green);
            
            _boxCollider2D.size = Vector2.one;
            
            _inputParser.SetInputReferences(new Dictionary<string, MonoBehaviour>()
            {
                {"Move", _playerMovement}
            });
            
            _inputParser.OnStart();

            _cameraFollower.SetObjectToFollow(_thisGameObject.transform);
        }
    }
}