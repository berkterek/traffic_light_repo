using TrafficLight.Abstracts.Controllers;
using TrafficLight.Abstracts.Movements;
using TrafficLight.Enums;
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
        ITrafficController _trafficController;
        bool _isNearWaitPoint;

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
            if (IsTrafficNotNull()) return;

            _isNearWaitPoint = false;
            _mover.Tick(_currentTarget.position);
        }

        void FixedUpdate()
        {
            if (_isNearWaitPoint) return;

            _mover.FixedTick();
        }

        void LateUpdate()
        {
            UpdateTarget();
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.TryGetComponent(out ITrafficController trafficController)) return;

            _trafficController = trafficController;
            if (_trafficController.CurrentLightColor == LightColor.Red ||
                _trafficController.CurrentLightColor == LightColor.RedAmber)
            {
                SetNextPoint();
            }
        }

        void OnTriggerExit2D(Collider2D other)
        {
            _trafficController = null;
            _isNearWaitPoint = false;
        }

        private void UpdateTarget()
        {
            if (Vector3.Distance(_transform.position, _currentTarget.position) < 0.1f)
            {
                SetNextPoint();
            }
        }

        private void SetNextPoint()
        {
            _pointIndex++;
            if (_pointIndex >= _points.Length)
            {
                _pointIndex = 0;
            }

            _currentTarget = _points[_pointIndex];
        }

        private bool IsTrafficNotNull()
        {
            if (_trafficController != null)
            {
                if (_trafficController.CurrentLightColor == LightColor.Red ||
                    _trafficController.CurrentLightColor == LightColor.RedAmber)
                {
                    var position = _trafficController.TrafficWaitPoint.position;
                    _isNearWaitPoint = Vector3.Distance(position, _transform.position) < 0.1f;
                    _mover.Tick(position);
                    return true;
                }
                else if (_trafficController.CurrentLightColor == LightColor.Amber)
                {
                    if (_isNearWaitPoint)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}