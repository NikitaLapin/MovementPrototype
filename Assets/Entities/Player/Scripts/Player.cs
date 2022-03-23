using Entities.Player.Movement.StateMachine;
using Entities.Player.Scripts.Movement.MovementInput;
using Entities.Player.Scripts.Movement.StateMachine;
using UnityEngine;

namespace Entities.Player.Scripts
{
    [RequireComponent(typeof(PlayerStateMachine))]
    [RequireComponent(typeof(CharacterInputHandler))]
    public class Player : MonoBehaviour
    {
        private PlayerStateMachine _playerStateMachine;
        private CharacterInputHandler _inputHandler;
        
        private void Start()
        {
            _playerStateMachine = GetComponent<PlayerStateMachine>();
            _inputHandler = GetComponent<CharacterInputHandler>();
        }

        private void OnNewInput(Vector2 input)
        {
            
        }
    }
}
