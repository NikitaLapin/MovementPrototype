using UnityEngine;

namespace Entities.Player.Scripts.Movement.StateMachine.Substates.MovementSubstates
{
    public class Jump : ISubstate
    {
        private readonly PlayerStateMachine _context;
        private readonly MovementSubstateFactory _factory;
        private readonly Vector3 _lastInput;
        public float StateTimer { get; private set; }

        public Jump(PlayerStateMachine context, MovementSubstateFactory factory, Vector3 lastInput)
        {
            _context = context;
            _factory = factory;
            _lastInput = lastInput;
        }
        
        public void EnterState() => StateTimer = 0f;
        

        public void UpdateState()
        {
            StateTimer += Time.deltaTime;
            MoveCharacter();
        }

        public void ExitState() => _context.SetDefaultGravity();
        

        public bool TrySwitchState(out ISubstate newState)
        {
            newState = null;
            if (_context.PlayerMover.GetMove(_context.GravityMoveName).y <= 0f) newState = _factory.Fall(_lastInput);
            else if (_context.CharacterController.isGrounded) newState = _factory.Idle();

            return newState != null;
        }

        private void MoveCharacter()
        {
            var possibleGravitation = (_context.jumpStrength + _context.gravity * StateTimer) * Time.deltaTime;
            if(!_context.isGravitated) _context.PlayerMover.ChangeMove(_context.GravityMoveName, Vector3.zero);
            else _context.PlayerMover.ChangeMove(_context.GravityMoveName, new Vector3(0f, possibleGravitation, 0f));
            
            var newMove = new Vector3(_lastInput.x, 0f, _lastInput.z);
            _context.PlayerMover.ChangeMove(_context.StateMoveName, newMove);
        }
    }
}