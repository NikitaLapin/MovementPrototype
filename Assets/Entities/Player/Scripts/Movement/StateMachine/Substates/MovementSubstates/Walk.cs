using UnityEngine;

namespace Entities.Player.Scripts.Movement.StateMachine.Substates.MovementSubstates
{
    public class Walk : ISubstate
    {
        public float StateTimer { get; private set; }
        
        private readonly PlayerStateMachine _context;
        private readonly MovementSubstateFactory _factory;
        
        private Vector3 _previousPosition = Vector3.zero;
        private Vector3 _currentPosition = Vector3.zero;

        public Walk(PlayerStateMachine context, MovementSubstateFactory factory)
        {
            _context = context;
            _factory = factory;
        }


        public void EnterState()
        {
            StateTimer = 0f;
            _context.SetDefaultGravity();
        }

        public void UpdateState()
        {
            StateTimer += Time.deltaTime;
            MoveCharacter();
        }

        public void ExitState(){}

        public bool TrySwitchState(out ISubstate newState)
        {
            newState = null;

            if (_context.GetLastMoveVector().normalized.magnitude == 0) newState = _factory.Idle();
            else if (_context.CharacterInput.IsRunning) newState = _factory.Run();
            else if (!_context.CharacterController.isGrounded) newState = _factory.Fall(_currentPosition - _previousPosition);
            else if (_context.CharacterInput.IsJumped) newState = _factory.Jump(_currentPosition - _previousPosition);
            
            return newState != null;
        }

        private void MoveCharacter()
        {
            _previousPosition = _currentPosition;

            var newMove = _context.GetLastMoveVector();
            newMove *= _context.walkSpeed * _context.GetSlopeModifier();
            _context.PlayerMover.ChangeMove(_context.StateMoveName, newMove);

            _currentPosition = _context.transform.position;
        }
    }
}