using UnityEngine;

namespace Entities.Player.Scripts.Movement.StateMachine.Substates.MovementSubstates
{
    public class Idle : ISubstate
    {
        private readonly PlayerStateMachine _context;
        private readonly MovementSubstateFactory _factory;
        public float StateTimer { get; private set; }

        public Idle(PlayerStateMachine context, MovementSubstateFactory factory)
        {
            _context = context;
            _factory = factory;
        }

        public void EnterState()
        {
            StateTimer = 0f;
            _context.PlayerMover.ChangeMove(_context.StateMoveName, Vector3.zero);
            _context.SetDefaultGravity();
        }

        public void UpdateState() => StateTimer += Time.deltaTime;
        public void ExitState(){}

        public bool TrySwitchState(out ISubstate newState)
        {
            newState = null;
            
            if (_context.GetLastMoveVector().normalized.magnitude != 0) newState = _factory.Walk();
            else if (!_context.CharacterController.isGrounded) newState = _factory.Fall(Vector3.zero);
            else if (_context.CharacterInput.IsJumped) newState = _factory.Jump(Vector3.zero);
            else if(_context.TryGetIncline(out var incline) && incline >= _context.CharacterController.slopeLimit) newState = _factory.SlopeSlide();
            
            return newState != null;
        }
    }
}