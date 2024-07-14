using TrafficLight.Abstracts.DataContainers;
using UnityEngine;
using UnityEngine.Serialization;

namespace TrafficLight.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Light Data", menuName = "Terek Gaming/Data Containers/New Traffic Light")]
    public class TrafficLightDataContainerSO : ScriptableObject, ITrafficLightDataContainer

    {
        [SerializeField] float _redDuration = 5f;
        [SerializeField] float _redAmberDuration = 2f;
        [SerializeField] float _amberDuration = 3f;
        [SerializeField] float _greenDuration = 5f;

        public float RedDuration => _redDuration;
        public float RedAmberDuration => _redAmberDuration;
        public float AmberDuration => _amberDuration;
        public float GreenDuration => _greenDuration;
    }
}