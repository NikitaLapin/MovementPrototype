using UnityEngine;

namespace Entities.Player.Scripts.Movement.StateMachine.Substates.MovementSubstates
{
    public class SlopeSlide : ISubstate
    {
        public float StateTimer { get; private set; }
        
        private PlayerStateMachine _context;
        private MovementSubstateFactory _factory;

        public SlopeSlide(PlayerStateMachine context, MovementSubstateFactory factory)
        {
            _context = context;
            _factory = factory;
        }
        
        public void EnterState() => StateTimer = 0f;
        public void UpdateState()
        {
            
        }

        public void ExitState()
        {
            throw new System.NotImplementedException();
        }

        public bool TrySwitchState(out ISubstate newState)
        {
            throw new System.NotImplementedException();
        }
    }
}