using TrafficLight.Abstracts.Controllers;
using TrafficLight.Abstracts.States;
using TrafficLight.Enums;
using UnityEngine;

namespace TrafficLight.StateMachines.States
{
    public class AmberState : IState
    {
        readonly float _maxTime;
        readonly ITrafficController _trafficController;
        float _currentTime;

        public AmberState(ITrafficController trafficController, float maxTime)
        {
            _maxTime = maxTime;
            _trafficController = trafficController;
        }
        
        public void Enter()
        {
            _trafficController.SpriteRenderer.color = _trafficController.LightDataContainer.GetColor(LightColor.Amber);
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
            
            if (oldState == LightColor.Green)
            {
                _trafficController.CurrentLightColor = LightColor.RedAmber;
            }
            else
            {
                _trafficController.CurrentLightColor = LightColor.Green;
            }
        }
    }
}