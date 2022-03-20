using Entities.Player.Scripts.Movement.StateMachine.Substates;

namespace Entities.Player.Scripts.Movement.StateMachine.States
{
    public interface IState
    {
        public ISubstate CurrentSubstate { get; }
        public void EnterState();
        public void UpdateState();
        public void ExitState();
        public bool TrySwitchState(out IState newState);
    }
}
