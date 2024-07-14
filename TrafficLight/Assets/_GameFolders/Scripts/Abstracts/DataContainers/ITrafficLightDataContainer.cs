namespace TrafficLight.Abstracts.DataContainers
{
    public interface ITrafficLightDataContainer
    {
        public float RedDuration { get; }
        public float RedAmberDuration { get; }
        public float AmberDuration { get; }
        public float GreenDuration { get; }
    }
}