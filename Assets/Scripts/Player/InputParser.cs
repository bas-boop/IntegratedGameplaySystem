using System.Collections.Generic;
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

            InputAction jumpAction = actionMap.AddAction("Jump", binding: "<Keyboard>/space");
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
            _inputActionAsset["Jump"].performed += JumpAction;
        }

        private void RemoveListeners()
        {
            _inputActionAsset["Jump"].performed -= JumpAction;
        }
        
        #region Context
        
        private void JumpAction(InputAction.CallbackContext context) => Debug.Log("jump");

        #endregion
    }
}