using UnityEngine;

namespace TrafficLight.Helpers
{
    public static class MonoExtensions
    {
        public static void GetReference<T>(this MonoBehaviour monoBehaviour, ref T value) where T : Component
        {
            if (value == null)
            {
                value = monoBehaviour.GetComponentInChildren<T>();
            }
        }
    }
}