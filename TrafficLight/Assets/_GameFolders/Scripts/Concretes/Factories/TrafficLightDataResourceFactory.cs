using TrafficLight.Abstracts.DataContainers;
using TrafficLight.Abstracts.Factories;
using UnityEngine;

namespace TrafficLight.Factories
{
    public class TrafficLightDataResourceFactory : ITrafficLightDataFactory
    {
        readonly string _path;
        
        public TrafficLightDataResourceFactory(string path)
        {
            _path = path;
        }
        
        public ITrafficLightDataContainer Create()
        {
            var result = Resources.Load<ScriptableObject>($"LightDataContainers/{_path}");
            
            return result as ITrafficLightDataContainer;
        }
    }
}