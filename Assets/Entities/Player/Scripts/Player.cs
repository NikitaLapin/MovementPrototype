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
        public event Action<Vector2> InputChanged;
        public event Action<bool> RunChanged;
        public event Action<bool> JumpChanged;

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
            _inputHandler.JumpChanged += OnJumpChanged;
            _inputHandler.RunChanged += OnRunChanged;
        }

        private void OnDisable()
        {
            _inputHandler.InputChanged -= OnInputChanged;
            _inputHandler.JumpChanged -= OnJumpChanged;
            _inputHandler.RunChanged -= OnRunChanged;
        }

        private void OnRunChanged(bool isRunning) => RunChanged?.Invoke(isRunning);
        private void OnJumpChanged(bool isJumped) => JumpChanged?.Invoke(isJumped);
        private void OnInputChanged(Vector2 newInput) => InputChanged?.Invoke(newInput);
    }
}
