namespace Entities.Player.Scripts.Movement.StateMachine.States
{
    public class StateFactory
    {
        private PlayerStateMachine _context;

        public StateFactory(PlayerStateMachine context)
        {
            _context = context;
        }

        public IState MovementState() => new MovementState(_context, this);
    }
}
