using TrafficLight.Abstracts.Controllers;
using TrafficLight.Abstracts.DataContainers;
using TrafficLight.Abstracts.Factories;
using TrafficLight.Enums;
using TrafficLight.Factories;
using TrafficLight.Helpers;
using UnityEngine;
using UnityEngine.Serialization;

namespace TrafficLight.Controllers
{
    public class TrafficLightController : MonoBehaviour,ITrafficController
    {
        [SerializeField] LightColor _currentLightColor;
        [SerializeField] LightColor _oldLightColor;
        [SerializeField] Transform _transform;

        ITrafficLightDataContainer _lightDataContainer;

        public LightColor CurrentLightColor
        {
            get => _currentLightColor;
            set => _currentLightColor = value;
        }

        public LightColor OldLightColor
        {
            get => _oldLightColor;
            set => _oldLightColor = value;
        }

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