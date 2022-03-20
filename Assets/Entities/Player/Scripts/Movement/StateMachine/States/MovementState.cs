using Entities.Player.Scripts.Movement.StateMachine.Substates;
using Entities.Player.Scripts.Movement.StateMachine.Substates.MovementSubstates;

namespace Entities.Player.Scripts.Movement.StateMachine.States
{
    public class MovementState : IState
    {
        private PlayerStateMachine _context;
        private StateFactory _factory;
        private MovementSubstateFactory _substateFactory;
        
        public ISubstate CurrentSubstate { get; private set; }

        public MovementState(PlayerStateMachine context, StateFactory factory)
        {
            _context = context;
            _factory = factory;
        }
        
        public void EnterState()
        {
            _substateFactory = new MovementSubstateFactory(_context);
            CurrentSubstate = _substateFactory.Idle();
            CurrentSubstate.EnterState();
        }

        public void UpdateState()
        {
            if (CurrentSubstate.StateTimer > _context.minStateTime) 
                if (CurrentSubstate.TrySwitchState(out var newState)) SwitchState(newState);
            
            CurrentSubstate.UpdateState();
        }

        public void ExitState()
        {
            CurrentSubstate.ExitState();
        }

        public bool TrySwitchState(out IState newState)
        {
            newState = null;
            return false;
        }
        
        private void SwitchState(ISubstate newState)
        {
            CurrentSubstate.ExitState();
            CurrentSubstate = newState;
            CurrentSubstate.EnterState();
        }
    }
}
