using TrafficLight.Enums;
using UnityEngine;

namespace TrafficLight.Abstracts.DataContainers
{
    public interface ITrafficLightDataContainer
    {
        float RedDuration { get; }
        float RedAmberDuration { get; }
        float AmberDuration { get; }
        float GreenDuration { get; }
        Color GetColor(LightColor lightColor);
    }
}