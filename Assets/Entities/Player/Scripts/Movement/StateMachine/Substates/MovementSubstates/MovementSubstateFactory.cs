using UnityEngine;

namespace Entities.Player.Scripts.Movement.StateMachine.Substates.MovementSubstates
{
    public class MovementSubstateFactory
    {
        private readonly PlayerStateMachine _context;

        public MovementSubstateFactory(PlayerStateMachine context)
        {
            _context = context;
        }

        public ISubstate Idle() => new Idle(_context, this);
        public ISubstate Walk() => new Walk(_context, this);
        public ISubstate Run() => new Run(_context, this);
        public ISubstate SlidingStop(Vector3 lastMove) => new SlidingStop(_context, this, lastMove);
        public ISubstate Jump(Vector3 lastMove) => new Jump(_context, this, lastMove);
        public ISubstate Fall(Vector3 lastMove) => new Fall(_context, this, lastMove);
    }
}
