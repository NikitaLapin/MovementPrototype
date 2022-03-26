using System;
using Entities.Player.Input;
using Entities.Player.Movement.StateMachine;
using UnityEngine;

namespace Entities.Player.Scripts
{
    [RequireComponent(typeof(PlayerStateMachine))]
    [RequireComponent(typeof(CharacterInputHandler))]
    public class Player : MonoBehaviour
    {
        #region Events

        public event Action<Vector2> InputChanged;
        public event Action<bool> RunChanged;
        public event Action<bool> JumpInputChanged;
        public event Action<bool> JumpChanged; 
        public event Action<bool> FallChanged; 

        #endregion

        private CharacterInputHandler _inputHandler;
        private PlayerStateMachine _playerStateMachine;

        private void Awake()
        {
            _playerStateMachine = GetComponent<PlayerStateMachine>();
            _inputHandler = GetComponent<CharacterInputHandler>();
        }

        private void OnEnable()
        {
            _inputHandler.InputChanged += OnInputChanged;
            _inputHandler.JumpChanged += OnJumpInputChanged;
            _inputHandler.RunChanged += OnRunChanged;
            
            _playerStateMachine.JumpChanged += OnJumpChanged;
            _playerStateMachine.FallChanged += OnFallChanged;
        }

        private void OnDisable()
        {
            _inputHandler.InputChanged -= OnInputChanged;
            _inputHandler.JumpChanged -= OnJumpInputChanged;
            _inputHandler.RunChanged -= OnRunChanged;
            
            _playerStateMachine.FallChanged -= OnFallChanged;
            _playerStateMachine.JumpChanged -= OnJumpChanged;
        }

        private void OnRunChanged(bool isRunning) => RunChanged?.Invoke(isRunning);
        private void OnJumpInputChanged(bool isJumping) => JumpInputChanged?.Invoke(isJumping);
        private void OnInputChanged(Vector2 newInput) => InputChanged?.Invoke(newInput);
        private void OnFallChanged(bool isFalling) => FallChanged?.Invoke(isFalling);
        private void OnJumpChanged(bool isJumping) => JumpChanged?.Invoke(isJumping);
    }
}
