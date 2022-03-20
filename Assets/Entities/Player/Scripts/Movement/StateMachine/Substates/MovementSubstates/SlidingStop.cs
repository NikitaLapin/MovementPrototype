using UnityEngine;

namespace Entities.Player.Scripts.Movement.StateMachine.Substates.MovementSubstates
{
    public class SlidingStop : ISubstate
    {
        private readonly PlayerStateMachine _context;
        private readonly MovementSubstateFactory _factory;
        private readonly Vector3 _previousMove;
        public float StateTimer { get; private set; }
        private float _duration;

        public SlidingStop(PlayerStateMachine context, MovementSubstateFactory factory, Vector3 previousMove)
        {
            _context = context;
            _factory = factory;
            _previousMove = previousMove;
        }
        
        public void EnterState()
        {
            StateTimer = 0f;
            _context.SetDefaultGravity();
            _duration = _context.stopAfterRunCurve.keys[_context.stopAfterRunCurve.length - 1].time;
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
            if (StateTimer >= _duration || _context.stopAfterRunCurve.Evaluate(StateTimer) == 0) newState = _factory.Idle();
            if (StateTimer > _duration * _context.moveAfterStopOffset)
                if (_context.GetLastMoveVector().magnitude != 0) newState = _factory.Walk();

            return newState != null;
        }

        private void MoveCharacter()
        {
            var newMove = new Vector3(_previousMove.x, 0f, _previousMove.z);
            newMove *= _context.stopAfterRunCurve.Evaluate(StateTimer);
            _context.PlayerMover.ChangeMove(_context.StateMoveName, newMove);
        }
    }
}