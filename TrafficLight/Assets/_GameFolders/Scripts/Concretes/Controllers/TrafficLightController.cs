using TrafficLight.Abstracts.DataContainers;
using TrafficLight.Abstracts.Factories;
using TrafficLight.Factories;
using TrafficLight.Helpers;
using UnityEngine;

namespace TrafficLight.Controllers
{
    public class TrafficLightController : MonoBehaviour
    {
        [SerializeField] Transform _transform;

        ITrafficLightDataContainer _lightDataContainer;

        void OnValidate()
        {
            this.GetReference(ref _transform);
        }

        void Awake()
        {
            Init();
        }

        private void Init()
        {
            ITrafficLightDataFactory trafficDataFactory = new TrafficLightDataResourceFactory(ConstHelper.TrafficDataPathKey);
            _lightDataContainer = trafficDataFactory.Create();
        }
    }
}