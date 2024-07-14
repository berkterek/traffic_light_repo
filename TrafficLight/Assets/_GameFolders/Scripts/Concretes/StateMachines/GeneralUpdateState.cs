using TrafficLight.Abstracts.States;

namespace TrafficLight.StateMachines
{
    public class GeneralUpdateState : IUpdateState
    {
        public IState CurrentState { get; }
        public IState NextState { get; }
        public System.Func<bool> Condition { get; }
        
        public GeneralUpdateState(IState currentState, IState nextState, System.Func<bool> condition)
        {
            CurrentState = currentState;
            NextState = nextState;
            Condition = condition;
        }
    }
    
    
}