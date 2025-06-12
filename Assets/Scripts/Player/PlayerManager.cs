using System.Collections.Generic;
using UnityEngine;

using Event;
using GameSystem;
using Gameplay.Collision;
using Gameplay.HealthSystem;
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
        private SphereTrigger _collider;
        private Health _health;
        
        private CameraFollower _cameraFollower;

        private GameObject _thisGameObject;
        private Bullet _bullet;
        
        public void OnStart()
        {
            CreateComponents();
            SetupComponents();
            UpdateHealthUi();
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
            _collider = GameobjectComponentLibrary.AddComponent<SphereTrigger>(NAME);
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
            _collider.AddListener(OnCol);
            
            SpriteMaker.MakeSprite(_spriteRenderer, ShapeType.TRIANGLE, Color.green);
            
            _health = new (3);
            _health.AddDamageListener(UpdateHealthUi);
            _health.AddDieListener(Die);
            
            _shooter.Init(GameobjectComponentLibrary.GetGameObject(NAME).transform);
            
            _inputParser.SetInputReferences(new Dictionary<string, MonoBehaviour>()
            {
                {"Move", _playerMovement},
                {"Shoot", _shooter},
            });
            
            _inputParser.OnStart();

            _cameraFollower.SetObjectToFollow(_thisGameObject.transform);
        }

        private void OnCol(GameObject other)
        {
            if (other.CompareTag(Tags.ENEMY_TAG))
                _health.Damage(1);
        }
        
        private void Die()
        {
            EventObserver.InvokeEvent(ObserverEventType.GAME_END_LOSE);
            
            _collider = null;
            GameobjectComponentLibrary.RemoveGameobject(VISUAL);
            GameobjectComponentLibrary.RemoveGameobject(FIREPOINT);
            GameobjectComponentLibrary.RemoveGameobject(NAME);
        }

        private void UpdateHealthUi()
        {
            GameobjectComponentLibrary.GetUiElement("Health").text =
                $"Player health: {_health.CurrentHealth}/{_health.StartHealth}";
        }
    }
}