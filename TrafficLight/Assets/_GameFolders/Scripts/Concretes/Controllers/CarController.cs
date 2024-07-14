using TrafficLight.Abstracts.Movements;
using TrafficLight.Helpers;
using TrafficLight.Movements;
using UnityEngine;

namespace TrafficLight.Controllers
{
    public class CarController : MonoBehaviour
    {
        [SerializeField] float _speed = 30f;
        [SerializeField] Transform _transform;
        [SerializeField] Transform _currentTarget;
        [SerializeField] Transform[] _points;

        IMoverStrategy _mover;
        int _pointIndex = 0;

        void OnValidate()
        {
            this.GetReference(ref _transform);
        }

        void Awake()
        {
            _mover = new MoveWithTransform(_transform, _speed);
        }

        void Start()
        {
            _transform.position = _points[_pointIndex].position;
            _currentTarget = _points[_pointIndex];
        }

        void Update()
        {
            _mover.Tick(_currentTarget.position);
        }

        void FixedUpdate()
        {
            _mover.FixedTick();
        }

        void LateUpdate()
        {
            UpdateTarget();
        }

        private void UpdateTarget()
        {
            if (Vector3.Distance(_transform.position, _currentTarget.position) < 0.1f)
            {
                _pointIndex++;
                if (_pointIndex >= _points.Length)
                {
                    _pointIndex = 0;
                }

                _currentTarget = _points[_pointIndex];
            }
        }
    }    
}