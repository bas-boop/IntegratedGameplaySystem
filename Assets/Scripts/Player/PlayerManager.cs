using GameSystem;
using UnityEngine;

namespace Player
{
    public class PlayerManager : IGameobject
    {
        private const string _name = "Player";
        private Rigidbody2D _rigidbody2D;
        private BoxCollider2D _boxCollider2D;
        private SpriteRenderer _spriteRenderer;
        
        public void OnStart()
        {
            GameobjectComponentLibrary.CreateGameObject(_name);

            _rigidbody2D = GameobjectComponentLibrary.AddComponent<Rigidbody2D>(_name);
            _spriteRenderer = GameobjectComponentLibrary.AddComponent<SpriteRenderer>(_name);
            _boxCollider2D = GameobjectComponentLibrary.AddComponent<BoxCollider2D>(_name);

            _rigidbody2D.gravityScale = 0;

            Texture2D tex = new Texture2D(100, 100);
            tex.SetPixel(0, 0, Color.white);
            tex.Apply();

            _spriteRenderer.sprite = Sprite.Create(tex, new Rect(0, 0, 100, 100), new Vector2(0.5f, 0.5f));
            
            _boxCollider2D.size = Vector2.one;
        }

        public void OnUpdate()
        {
            
        }
    }
}