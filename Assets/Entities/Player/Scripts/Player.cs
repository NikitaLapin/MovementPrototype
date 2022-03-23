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
        [NonSerialized] public CharacterInputHandler InputHandler;
        private PlayerStateMachine _playerStateMachine;

        private void Start()
        {
            _playerStateMachine = GetComponent<PlayerStateMachine>();
            InputHandler = GetComponent<CharacterInputHandler>();
        }
    }
}
