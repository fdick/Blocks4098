namespace Code.StateMachine
{
    public interface IState
    {
        public void Enter();
        public void Exit();
    }
}