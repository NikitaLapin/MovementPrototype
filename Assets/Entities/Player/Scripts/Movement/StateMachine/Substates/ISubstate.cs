namespace Entities.Player.Scripts.Movement.StateMachine.Substates
{
    public interface ISubstate
    {
        public float StateTimer { get; }
        public void EnterState();
        public void UpdateState();
        public void ExitState();
        public bool TrySwitchState(out ISubstate newState);
    }
}
