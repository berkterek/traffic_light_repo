namespace TrafficLight.Abstracts.States
{
    public interface IUpdateState
    {
        public IState CurrentState { get; }
        public IState NextState { get; }
        public System.Func<bool> Condition { get; }
    }
}