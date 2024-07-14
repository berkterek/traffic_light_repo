using TrafficLight.Abstracts.Controllers;
using TrafficLight.Abstracts.States;
using TrafficLight.Enums;
using UnityEngine;

namespace TrafficLight.StateMachines.States
{
    public class GreenState : IState
    {
        readonly float _maxTime;
        readonly ITrafficController _trafficController;
        float _currentTime;

        public GreenState(ITrafficController trafficController, float maxTime)
        {
            _maxTime = maxTime;
            _trafficController = trafficController;
        }
        
        public void Enter()
        {
            if (_trafficController.CurrentLightColor == LightColor.None)
            {
                _trafficController.CurrentLightColor = LightColor.Green;    
            }
            
            Debug.Log($"Enter traffic color state => {_trafficController.CurrentLightColor}");
            _currentTime = 0f;
        }

        public void Exit()
        {
            Debug.Log($"Exit traffic color state => {_trafficController.OldLightColor}");
        }

        public void Tick()
        {
            _currentTime += Time.deltaTime;

            if (_currentTime < _maxTime) return;

            _trafficController.OldLightColor = _trafficController.CurrentLightColor;
            _trafficController.CurrentLightColor = LightColor.Amber;
        }
    }
}