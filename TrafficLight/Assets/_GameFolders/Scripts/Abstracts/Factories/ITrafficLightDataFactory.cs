using TrafficLight.Abstracts.DataContainers;

namespace TrafficLight.Abstracts.Factories
{
    public interface ITrafficLightDataFactory
    {
        ITrafficLightDataContainer Create();
    }
}