using GameSystem;
using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        private float _speed = 5;
        
        public void SetInput(Vector2 input)
        {
            Rigidbody2D rb = GameobjectComponentLibrary.GetGameObject("Player").GetComponent<Rigidbody2D>();
            rb.linearVelocity = input * _speed;
        }
    }
}