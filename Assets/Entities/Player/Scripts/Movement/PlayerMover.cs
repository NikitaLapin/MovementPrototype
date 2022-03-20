using System.Collections.Generic;
using UnityEngine;

namespace Entities.Player.Scripts.Movement
{
    public class PlayerMover : MonoBehaviour
    {
        public List<Move> CurrentMoves { private get; set; } = new List<Move>();
        private CharacterController _playerController;
        private Vector3 _mainMove;

        private void Start()
        {
            _playerController = GetComponent<CharacterController>();
        }

        private void Update()
        {
            SumAllVectors();
            _playerController.Move(_mainMove);
        }

        public Vector3 GetMove(string moveName)
        {
            foreach (var move in CurrentMoves) if (move.MoveType == moveName) return move.MoveVector;
            return Vector3.zero;
        }

        public void ChangeMove(string moveName, Vector3 newMove)
        {
            foreach (var move in CurrentMoves)
            {
                if (move.MoveType == moveName)
                {
                    move.MoveVector = newMove;
                    return;
                }
            }
            
            CurrentMoves.Add(new Move(newMove, moveName));
        }

        private void SumAllVectors()
        {
            _mainMove = Vector3.zero;
            foreach (var move in CurrentMoves) _mainMove += move.MoveVector;
        }
    }

    public class Move
    {
        public Move(Vector3 move, string name)
        {
            MoveType = name;
            MoveVector = move;
        }
        
        public string MoveType { get; set; }
        public Vector3 MoveVector { get; set; }
    }
}
