using TrafficLight.Enums;

namespace TrafficLight.Abstracts.Controllers
{
    public interface ITrafficController
    {
        LightColor CurrentLightColor { get; set; }
        LightColor OldLightColor { get; set; }
    }
}