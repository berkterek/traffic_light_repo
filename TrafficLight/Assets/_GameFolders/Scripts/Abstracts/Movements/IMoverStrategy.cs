using UnityEngine;

namespace TrafficLight.Abstracts.Movements
{
    public interface IMoverStrategy
    {
        void Tick(Vector3 direction);
        void FixedTick();
    }
}
