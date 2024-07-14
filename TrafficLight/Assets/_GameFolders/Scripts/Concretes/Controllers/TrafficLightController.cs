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

namespace TrafficLight.Controllers
{
    public class TrafficLightController : MonoBehaviour,ITrafficController
    {
        [SerializeField] bool _isRedStart = false;
        [SerializeField] bool _isGreenStart = false;
        [SerializeField] LightColor _currentLightColor;
        [SerializeField] LightColor _oldLightColor;
        [SerializeField] Transform _transform;
        [SerializeField] Transform _trafficWaitPoint;
        [SerializeField] SpriteRenderer _spriteRenderer;

        IStateMachine _stateMachine;
        public ITrafficLightDataContainer LightDataContainer { get; private set; }
        public Transform TrafficWaitPoint => _trafficWaitPoint;
        public SpriteRenderer SpriteRenderer => _spriteRenderer;

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
            this.GetReference(ref _spriteRenderer);
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
            LightDataContainer = trafficDataFactory.Create();

            IState red = new RedState(this, LightDataContainer.RedDuration);
            IState green = new GreenState(this, LightDataContainer.GreenDuration);
            IState redAmber = new RedAmberState(this, LightDataContainer.RedAmberDuration);
            IState amber = new AmberState(this, LightDataContainer.AmberDuration);

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