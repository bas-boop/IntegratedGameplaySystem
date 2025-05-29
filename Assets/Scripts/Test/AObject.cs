using GameSystem;
using UnityEngine;

namespace Test
{
    public class AObject : IGameobject
    {
        public void OnStart()
        {
            Debug.Log("Start");

            GameobjectComponentLibrary.CreateGameObject("newSprite");
            GameobjectComponentLibrary.AddComponent<SpriteRenderer>("newSprite");
        }

        public void OnUpdate()
        {
            Debug.Log("Update");
        }
    }
}