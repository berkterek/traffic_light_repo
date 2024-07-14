using System;
using TrafficLight.Abstracts.Controllers;
using TrafficLight.Abstracts.DataContainers;
using TrafficLight.Abstracts.Factories;
using TrafficLight.Abstracts.States;
using TrafficLight.Enums;
using TrafficLight.Factories;
using TrafficLight.Helpers;
using TrafficLight.StateMachines;
using TrafficLight.StateMachines.States;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TrafficLight.Controllers
{
    public class TrafficLightController : MonoBehaviour,ITrafficController
    {
        [SerializeField] bool _isRedStart = false;
        [SerializeField] bool _isGreenStart = false;
        [SerializeField] LightColor _currentLightColor;
        [SerializeField] LightColor _oldLightColor;
        [SerializeField] Transform _transform;

        ITrafficLightDataContainer _lightDataContainer;
        IStateMachine _stateMachine;

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

        void Update()
        {
            _stateMachine.Tick();
        }

        private void Init()
        {
            ITrafficLightDataFactory trafficDataFactory = new TrafficLightDataResourceFactory(ConstHelper.TrafficDataPathKey);
            _lightDataContainer = trafficDataFactory.Create();

            IState red = new RedState(this, _lightDataContainer.RedDuration);
            IState green = new GreenState(this, _lightDataContainer.GreenDuration);
            IState redAmber = new RedAmberState(this, _lightDataContainer.RedAmberDuration);
            IState amber = new AmberState(this, _lightDataContainer.AmberDuration);

            if ((_isGreenStart && _isRedStart) || (!_isGreenStart && !_isRedStart))
            {
                _isGreenStart = System.Convert.ToBoolean(Random.Range(0, 2));
                _isRedStart = !_isGreenStart;
            }

            _stateMachine = new GeneralStateMachine();
            _stateMachine.SetNormalStateAndConditions(red, redAmber, () => CurrentLightColor == LightColor.RedAmber && OldLightColor == LightColor.Red, _isRedStart);
            _stateMachine.SetNormalStateAndConditions(redAmber, amber, () => CurrentLightColor == LightColor.Amber && OldLightColor == LightColor.RedAmber);
            _stateMachine.SetNormalStateAndConditions(amber, green, () => CurrentLightColor == LightColor.Green && OldLightColor == LightColor.Amber);
            _stateMachine.SetNormalStateAndConditions(green, amber, () => CurrentLightColor == LightColor.Amber && OldLightColor == LightColor.Green, _isGreenStart);
            _stateMachine.SetNormalStateAndConditions(amber, redAmber, () => CurrentLightColor == LightColor.RedAmber && OldLightColor == LightColor.Amber);
            _stateMachine.SetNormalStateAndConditions(redAmber, red, () => CurrentLightColor == LightColor.Red && OldLightColor == LightColor.RedAmber);
        }
    }
}