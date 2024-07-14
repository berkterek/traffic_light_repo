using TrafficLight.Abstracts.DataContainers;
using TrafficLight.Enums;
using UnityEngine;

namespace TrafficLight.Abstracts.Controllers
{
    public interface ITrafficController
    {
        LightColor CurrentLightColor { get; set; }
        LightColor OldLightColor { get; set; }
        ITrafficLightDataContainer LightDataContainer { get; }
        SpriteRenderer SpriteRenderer { get; }
        Transform TrafficWaitPoint { get; }
    }
}