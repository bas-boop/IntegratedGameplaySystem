using System.Collections.Generic;
using Gameplay.Collision;
using UnityEngine;

using GameSystem;
using Gameplay.Shooter;
using Visuals;

namespace Player
{
    public class PlayerManager : IGameobject
    {
        private const string NAME = "Player";
        private const string VISUAL = "PlayerVisual";
        private const string FIREPOINT = "PlayerFirepoint";
        
        private Rigidbody2D _rigidbody2D;
        private BoxCollider2D _boxCollider2D;
        private SpriteRenderer _spriteRenderer;
        private InputParser _inputParser;
        private PlayerMovement _playerMovement;
        private Shooter _shooter;
        private SphereColliderX _collider;
        
        private CameraFollower _cameraFollower;

        private GameObject _thisGameObject;
        private Bullet _bullet;
        
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
            GameobjectComponentLibrary.CreateGameObject(FIREPOINT);
            GameobjectComponentLibrary.SetParent(VISUAL, NAME);
            GameobjectComponentLibrary.SetParent(FIREPOINT, NAME);

            _rigidbody2D = GameobjectComponentLibrary.AddComponent<Rigidbody2D>(NAME);
            _boxCollider2D = GameobjectComponentLibrary.AddComponent<BoxCollider2D>(NAME);
            _inputParser = GameobjectComponentLibrary.AddComponent<InputParser>(NAME);
            _playerMovement = GameobjectComponentLibrary.AddComponent<PlayerMovement>(NAME);
            _shooter = GameobjectComponentLibrary.AddComponent<Shooter>(NAME);
            _collider = GameobjectComponentLibrary.AddComponent<SphereColliderX>(NAME);
            _spriteRenderer = GameobjectComponentLibrary.AddComponent<SpriteRenderer>(VISUAL);
            
            _thisGameObject = GameobjectComponentLibrary.GetGameObject(NAME);
            _thisGameObject.tag = Tags.PLAYER_TAG;
            GameobjectComponentLibrary.GetGameObject(VISUAL).transform.rotation = Quaternion.Euler(0, 0, 225);
            GameobjectComponentLibrary.GetGameObject(FIREPOINT).transform.position = new (0, 0.7f);

            GameobjectComponentLibrary.AddCamera();
            _cameraFollower = GameobjectComponentLibrary.AddComponent<CameraFollower>("MainCamera");
        }

        private void SetupComponents()
        {
            _rigidbody2D.gravityScale = 0;
            _rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
            
            _boxCollider2D.size = new (1, 0.5f);
            _boxCollider2D.offset = new (0, 0.25f);
            
            SpriteMaker.MakeSprite(_spriteRenderer, ShapeType.TRIANGLE, Color.green);
            
            _shooter.Init(GameobjectComponentLibrary.GetGameObject(NAME).transform);
            
            _inputParser.SetInputReferences(new Dictionary<string, MonoBehaviour>()
            {
                {"Move", _playerMovement},
                {"Shoot", _shooter},
            });
            
            _inputParser.OnStart();

            _cameraFollower.SetObjectToFollow(_thisGameObject.transform);
        }
    }
}