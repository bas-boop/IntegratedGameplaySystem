﻿namespace GameSystem
{
    public interface IGameobject
    {
        public void OnStart();
        public void OnUpdate();
        public void OnFixedUpdate();
    }
}