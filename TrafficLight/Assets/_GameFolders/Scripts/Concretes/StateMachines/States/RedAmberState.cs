using TrafficLight.Abstracts.Controllers;
using TrafficLight.Abstracts.States;
using TrafficLight.Enums;
using UnityEngine;

namespace TrafficLight.StateMachines.States
{
    public class RedAmberState : IState
    {
        readonly float _maxTime;
        readonly ITrafficController _trafficController;
        float _currentTime;

        public RedAmberState(ITrafficController trafficController, float maxTime)
        {
            _maxTime = maxTime;
            _trafficController = trafficController;
        }
        
        public void Enter()
        {
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

            var oldState = _trafficController.OldLightColor;
            _trafficController.OldLightColor = _trafficController.CurrentLightColor;
            
            if (oldState == LightColor.Red)
            {
                _trafficController.CurrentLightColor = LightColor.Amber;
            }
            else
            {
                _trafficController.CurrentLightColor = LightColor.Red;
            }
        }
    }
}