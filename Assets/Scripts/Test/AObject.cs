using GameSystem;
using UnityEngine;

namespace Test
{
    public class AObject : IGameobject
    {
        public void OnStart()
        {
            Debug.Log("Start");
        }

        public void OnUpdate()
        {
            Debug.Log("Update");
        }
    }
}