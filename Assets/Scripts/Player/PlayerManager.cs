using System.Collections.Generic;
using UnityEngine;

using GameSystem;

namespace Player
{
    public class PlayerManager : IGameobject
    {
        private const string NAME = "Player";
        
        private Rigidbody2D _rigidbody2D;
        private BoxCollider2D _boxCollider2D;
        private SpriteRenderer _spriteRenderer;
        private InputParser _inputParser;
        private PlayerMovement _playerMovement;

        private GameObject _thisGameObject;
        
        public void OnStart()
        {
            CreateComponents();
            SetupComponents();
        }

        public void OnUpdate()
        {
            _inputParser.OnUpdate();
        }

        public void OnFixedUpdate() { }

        private void CreateComponents()
        {
            GameobjectComponentLibrary.CreateGameObject(NAME);

            _rigidbody2D = GameobjectComponentLibrary.AddComponent<Rigidbody2D>(NAME);
            _spriteRenderer = GameobjectComponentLibrary.AddComponent<SpriteRenderer>(NAME);
            _boxCollider2D = GameobjectComponentLibrary.AddComponent<BoxCollider2D>(NAME);
            _inputParser = GameobjectComponentLibrary.AddComponent<InputParser>(NAME);
            _playerMovement = GameobjectComponentLibrary.AddComponent<PlayerMovement>(NAME);
            
            _thisGameObject = GameobjectComponentLibrary.GetGameObject(NAME);
        }

        private void SetupComponents()
        {
            _rigidbody2D.gravityScale = 0;

            // square
            /*Texture2D tex = new Texture2D(100, 100);
            tex.SetPixel(0, 0, Color.white);
            tex.Apply();
            _spriteRenderer.sprite = Sprite.Create(tex, new Rect(0, 0, 100, 100), new Vector2(0.5f, 0.5f));*/
            
            // triangle
            Texture2D tex = new Texture2D(100, 100);

            Color clear = new Color(0, 0, 0, 0);
            Color[] pixels = new Color[100 * 100];
            
            for (int i = 0; i < pixels.Length; i++)
                pixels[i] = clear;
            
            tex.SetPixels(pixels);

            Color color = Color.white;
            for (int y = 0; y < 100; y++)
            {
                for (int x = 0; x <= y; x++)
                {
                    tex.SetPixel(x, y, color);
                }
            }

            tex.Apply();
            _spriteRenderer.sprite = Sprite.Create(tex, new Rect(0, 0, 100, 100), new Vector2(0.5f, 0.5f));
            
            _boxCollider2D.size = Vector2.one;
            
            _inputParser.SetInputReferences(new Dictionary<string, MonoBehaviour>()
            {
                {"Move", _playerMovement}
            });
            _inputParser.OnStart();
        }
    }
}