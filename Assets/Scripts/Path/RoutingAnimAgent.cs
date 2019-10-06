using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityAdvance
{
    public class RoutingAnimAgent : MonoBehaviour
    {
        [SerializeField]
        private MovementPath _movementPath;

        [SerializeField]
        private float _movementSpeed;

        private Vector3 _startPoint;
        private Vector3 _endPoint;

        private float _totalLength;
        private float _elapseDistance;

        // Start is called before the first frame update
        void Start()
        {
            CalculateRouteLength();
        }

        private void CalculateRouteLength()
        {
            _totalLength = 0;
            for (int i = 1; i < _movementPath.PointCount; i++)
            {
                _totalLength += Vector3.Distance(_movementPath[i - 1], _movementPath[i]);
            }
        }

        private void Update()
        {
            MovementWithSmoothDamp();
        }

        private float _velocity;
        private void MovementWithSmoothDamp()
        {
            _elapseDistance = Mathf.SmoothDamp(_elapseDistance, _totalLength,
                ref _velocity, 1f);
            Move(_elapseDistance);
        }

        private int curPointIndex = 0;
        private void Move(float distance)
        {
            for (int i = 1; i < _movementPath.PointCount; i++)
            {
                var sectionDistance = Vector3.Distance(_movementPath[i - 1], _movementPath[i]);
                if (distance <= sectionDistance)
                {
                    LocateAgentInSection(i, distance, sectionDistance);
                    return;
                }

                distance -= sectionDistance;
            }
        }

        private void LocateAgentInSection(int pointIndex, float curDistance, float sectionDistance)
        {
            _startPoint = _movementPath[pointIndex - 1];
            _endPoint = _movementPath[pointIndex];

            //cach 1:
            //var _distanceDoneOnSection = curDistance / sectionDistance;
            //transform.position = Vector3.Lerp(_startPoint, _endPoint, _distanceDoneOnSection);

            //cach 2:
            var direction = (_endPoint - _startPoint);
            direction.Normalize();

            transform.position = _startPoint + direction * curDistance;

            transform.LookAt(_endPoint);
        }

        private bool IsNearPoint(Vector3 point)
        {
            if (Vector3.Distance(transform.position, point) <= 0.1f)
                return true;

            return false;
        }
    }
}
