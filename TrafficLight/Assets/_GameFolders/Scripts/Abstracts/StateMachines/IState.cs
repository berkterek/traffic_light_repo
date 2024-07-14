namespace TrafficLight.Abstracts.States
{
    public interface IState
    {
        void Enter();
        void Exit();
        void Tick();
    }
}