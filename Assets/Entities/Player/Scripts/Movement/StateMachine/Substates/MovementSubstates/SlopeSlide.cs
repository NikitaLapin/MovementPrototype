using UnityEngine;

namespace Entities.Player.Scripts.Movement.StateMachine.Substates.MovementSubstates
{
    public class SlopeSlide : ISubstate
    {
        private PlayerStateMachine _context;
        private MovementSubstateFactory _factory;

        public SlopeSlide(PlayerStateMachine context, MovementSubstateFactory factory)
        {
            _context = context;
            _factory = factory;
        }
        
        public float StateTimer { get; private set; }
        public void EnterState() => StateTimer = 0f;

        public void UpdateState()
        {
            StateTimer += Time.deltaTime;
            MoveCharacter();
        }

        public void ExitState()
        {
        }

        public bool TrySwitchState(out ISubstate newState)
        {
            newState = null;
            if (_context.CharacterInput.IsJumped) newState = _factory.Jump(_context.PlayerMover.GetMove(_context.StateMoveName));
            else if (!_context.TryGetIncline(out var incline)) newState = _factory.Fall(_context.PlayerMover.GetMove(_context.StateMoveName));
            else if (incline <= _context.CharacterController.slopeLimit) newState = _factory.Idle();

            return newState != null;
        }

        private void MoveCharacter()
        {
            if(!_context.TryGetIncline(out var incline)) return;
            
            var ray = new Ray(_context.Transform.position, Vector3.down);
            Physics.Raycast(ray, out var hitInfo, _context.CharacterController.height);
            var slideDirection = new Vector3(hitInfo.normal.x, -hitInfo.normal.y, hitInfo.normal.z);
            
            var move =  slideDirection * _context.slidingSlopeSpeed.Evaluate(incline) * _context.slopeSpeed;
            move += _context.GetLastMoveVector() * _context.walkSpeed;
            _context.PlayerMover.ChangeMove(_context.StateMoveName, move);
        }
    }
}