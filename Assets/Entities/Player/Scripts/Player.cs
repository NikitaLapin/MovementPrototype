using Entities.Player.Scripts.Movement.StateMachine;
using UnityEditor;
using UnityEngine;

namespace Entities.Player.Scripts
{
    [RequireComponent(typeof(PlayerStateMachine))]
    public class Player : MonoBehaviour
    {
        private PlayerStateMachine _playerStateMachine;
        private void Start()
        {
            _playerStateMachine = GetComponent<PlayerStateMachine>();
        }
    }
}
