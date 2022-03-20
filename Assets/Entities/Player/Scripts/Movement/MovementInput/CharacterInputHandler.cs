using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Entities.Player.Scripts.Movement.MovementInput
{
    public class CharacterInputHandler : MonoBehaviour
    {
        [NonSerialized] public Vector2[] InputLog;
        [NonSerialized] public bool IsRunning;
        [NonSerialized] public bool IsJumped;
        

        [SerializeField] private int sizeOfInputLog = 5;

        private CharacterInput _input;

        private void Awake()
        {
            InputLog = new Vector2[sizeOfInputLog];
            _input = new CharacterInput();

            EnableMovementActions();
            EnableFightActions();
            EnableParkourActions();
        }

        private void OnEnable() => _input.MovementStateMap.Enable();

        private void OnDisable() => _input.Disable();


        public void SwitchMap(Map mapType)
        {
            _input.FightStateMap.Disable();
            _input.MovementStateMap.Disable();
            _input.ParkourStateMap.Disable();

            if (mapType == Map.Movement) _input.MovementStateMap.Enable();
            else if (mapType == Map.Fight) _input.FightStateMap.Enable();
            else if (mapType == Map.Parkour) _input.ParkourStateMap.Enable();
            else throw new ArgumentException();
        }

        private void EnableMovementActions()
        {
            _input.MovementStateMap.Walk.started += OnWalk;
            _input.MovementStateMap.Walk.performed += OnWalk;
            _input.MovementStateMap.Walk.canceled += OnWalk;

            _input.MovementStateMap.Run.started += OnRun;
            _input.MovementStateMap.Run.canceled += OnRun;

            _input.MovementStateMap.Jump.started += OnJump;
        }

        private void EnableFightActions(){}
        private void EnableParkourActions(){}

        private void OnWalk(InputAction.CallbackContext context) => AddNewInput(context.ReadValue<Vector2>());
        private void OnRun(InputAction.CallbackContext context) => IsRunning = context.ReadValueAsButton();
        private void OnJump(InputAction.CallbackContext context) => IsJumped = context.ReadValueAsButton();

        private void AddNewInput(Vector2 input)
        {
            for (var i = sizeOfInputLog - 1; i > 0; i--) InputLog[i] = InputLog[i - 1];
            InputLog[0] = input;
        }
        
        public enum Map
        {
            Movement,
            Fight,
            Parkour
        }
    }
}
