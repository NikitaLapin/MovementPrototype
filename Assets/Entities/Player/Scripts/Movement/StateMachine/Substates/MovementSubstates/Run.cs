using UnityEngine;

namespace Entities.Player.Scripts.Movement.StateMachine.Substates.MovementSubstates
{
    public class Run : ISubstate
    {
        private readonly PlayerStateMachine _context;
        private readonly MovementSubstateFactory _factory;
        public float StateTimer { get; private set; }
        
        private Vector3 _previousPosition = Vector3.zero;
        private Vector3 _currentPosition = Vector3.zero;

        public Run(PlayerStateMachine context, MovementSubstateFactory factory)
        {
            _context = context;
            _factory = factory;
        }

        public void EnterState()
        {
            StateTimer = 0f;
            _context.gravity *= 2;
            _context.SetDefaultGravity();
        }

        public void UpdateState()
        {
            StateTimer += Time.deltaTime;
            MoveCharacter();
        }

        public void ExitState() => _context.gravity /= 2;

        public bool TrySwitchState(out ISubstate newState)
        {
            newState = null;
            if (StateTimer < _context.minStateTime) return false;
            var lastMove = _currentPosition - _previousPosition;
            
            if (_context.GetLastMoveVector().normalized.magnitude == 0) newState = _factory.SlidingStop(lastMove);
            else if (!_context.CharacterInput.IsRunning) newState = _factory.SlidingStop(lastMove);
            else if (_context.CharacterInput.IsJumped) newState = _factory.Jump(lastMove);
            else if (!_context.CharacterController.isGrounded) newState = _factory.Fall(lastMove);
            
            return newState != null;
        }
        
        private void MoveCharacter()
        {
            _previousPosition = _currentPosition;
            
            var newMove = _context.GetLastMoveVector();
            newMove *= _context.walkSpeed + _context.runSpeedUpCurve.Evaluate(StateTimer) * _context.runModifier * _context.GetSlopeModifier();
            _context.PlayerMover.ChangeMove(_context.StateMoveName, newMove);
            
            _currentPosition = _context.transform.position;
        }
    }
}