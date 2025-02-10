namespace Code.Gameplay.StateMachine
{
    public interface IStateManager
    {
        void InitializeStateMachine();
        void StartStateMachine();
        void Tick();
    }
}