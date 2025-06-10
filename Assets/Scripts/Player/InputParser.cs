using System.Collections.Generic;
using Gameplay.Shooter;
using GameSystem;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    [RequireComponent(typeof(PlayerInput))]
    public sealed class InputParser : MonoBehaviour, IGameobject
    {
        private PlayerInput _playerInput;
        private InputActionAsset _inputActionAsset;

        private PlayerMovement _playerMovement;
        private Shooter _shooter;
        
        public void OnStart()
        {
            GetReferences();
            Init();
            AddListeners();
        }

        public void OnUpdate()
        {
            Vector2 input = _inputActionAsset["Move"].ReadValue<Vector2>();
            _playerMovement.SetInput(input);
        }

        public void OnFixedUpdate()
        {
            _playerMovement.OnFixedUpdate();
        }

        public void SetInputReferences(Dictionary<string, MonoBehaviour> components)
        {
            _shooter = (Shooter) components["Shoot"];
            _playerMovement = (PlayerMovement) components["Move"];
            _playerMovement.OnStart();
        }

        private void GetReferences()
        {
            _playerInput = GetComponent<PlayerInput>();
        }

        private void Init()
        {
            InputActionMap actionMap = new InputActionMap("Gameplay");

            InputAction jumpAction = actionMap.AddAction("Shoot", binding: "<Keyboard>/space");
            InputAction moveAction = actionMap.AddAction("Move", InputActionType.Value, "Vector2");

            moveAction.AddCompositeBinding("2DVector")
                .With("Up", "<Keyboard>/w")
                .With("Down", "<Keyboard>/s")
                .With("Left", "<Keyboard>/a")
                .With("Right", "<Keyboard>/d");

            _playerInput.actions = new InputActionAsset();
            _playerInput.actions.AddActionMap(actionMap);
            _inputActionAsset = _playerInput.actions;

            actionMap.Enable();
        }


        private void AddListeners()
        {
            _inputActionAsset["Shoot"].performed += ShootAction;
        }

        private void RemoveListeners()
        {
            _inputActionAsset["Shoot"].performed -= ShootAction;
        }
        
        #region Context
        
        private void ShootAction(InputAction.CallbackContext context) => _shooter.ActivateShoot();

        #endregion
    }
}