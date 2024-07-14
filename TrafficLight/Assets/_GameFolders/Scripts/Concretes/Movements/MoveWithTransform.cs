using TrafficLight.Abstracts.Movements;
using UnityEngine;

namespace TrafficLight.Movements
{
    public class MoveWithTransform : IMoverStrategy
    {
        readonly Transform _transform;
        readonly float _speed;
        Vector3 _direction;
        
        public MoveWithTransform(Transform transform, float speed = 20f)
        {
            _transform = transform;
            _speed = speed;
        }

        public void Tick(Vector3 direction)
        {
            _direction = (direction - _transform.position).normalized;
            _direction = _speed * Time.deltaTime * _direction;
        }

        public void FixedTick()
        {
            _transform.position += _direction;
        }
    }
}