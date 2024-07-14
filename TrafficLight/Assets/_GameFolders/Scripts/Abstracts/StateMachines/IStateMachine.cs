namespace TrafficLight.Abstracts.States
{
    public interface IStateMachine
    {
        void Tick();
        void SetNormalStateAndConditions(IState currentState, IState nextState, System.Func<bool> condition,
            bool isFirstState = false);
        void SetAnyState(IState nextState, System.Func<bool> condition);
    }
}