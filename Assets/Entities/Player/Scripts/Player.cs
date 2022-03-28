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
        }

        private void OnDisable()
        {
            _inputHandler.InputChanged -= OnInputChanged;
            _inputHandler.JumpChanged -= OnJumpInputChanged;
            _inputHandler.RunChanged -= OnRunChanged;
        }

        private void OnRunChanged(bool isRunning) => RunChanged?.Invoke(isRunning);
        private void OnJumpInputChanged(bool isJumping) => JumpInputChanged?.Invoke(isJumping);
        private void OnInputChanged(Vector2 newInput) => InputChanged?.Invoke(newInput);
    }
}
