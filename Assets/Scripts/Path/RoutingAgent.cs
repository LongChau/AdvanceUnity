using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityAdvance
{
    public enum ERountingMethod
    {
        OneWay = 0,
        Cycle = 1,
        PingPong = 2,
    }

    public enum EMovementMethod
    {
        FullSpeed = 0,
        Lerp = 1,
        Damp = 2,
    }

    public class RoutingAgent : MonoBehaviour
    {
        [SerializeField]
        private MovementPath _movementPath;

        [SerializeField]
        private float _movementSpeed;

        [SerializeField]
        private EMovementMethod _movementMethod;

        [SerializeField]
        private ERountingMethod _rountingMethod;

        private Vector3 _startPoint;
        private Vector3 _endPoint;

        private int curPointIndex = 0;

        private bool _isLastPointReached;

        private Action OnLastPointReached;

        [SerializeField]
        private Compass _compass;

        private Vector3 _localPositionToCompass;
        public Vector3 FakeTarget
        {
            get
            {
                var deltaX = _localPositionToCompass.x;
                var fakeTargetLocalPosition = new Vector3(deltaX, 0, 0);
                return _compass.transform.TransformPoint(fakeTargetLocalPosition);
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            //transform.position = _movementPath[0];
            Init();
        }

        private void Init()
        {
            curPointIndex = 0;
            _startPoint = _movementPath[curPointIndex];
            _endPoint = _movementPath[curPointIndex + 1];
            _isLastPointReached = false;

            RepositionCompass();
        }

        private void Reset()
        {
            Init();
        }

        private void RepositionCompass()
        {
            _compass.transform.position = _endPoint;
            _compass.transform.forward = _endPoint - _startPoint;
        }

        // Update is called once per frame
        void Update()
        {
            if (_isLastPointReached)
                return;

            CheckLastPoint();

            LookAtNextPoint();
            SetOneway();
            CheckMovementMethod();
        }

        private bool CheckLastPoint()
        {
            if (transform.position == _movementPath.LastPoint)
            {
                Debug.Log("Last point reach!");
                _isLastPointReached = true;

                OnLastPointReached?.Invoke();

                CheckRoutingMethod();
            }
            else
                _isLastPointReached = false;

            return _isLastPointReached;
        }

        private void CheckRoutingMethod()
        {
            switch (_rountingMethod)
            {
                case ERountingMethod.Cycle:
                    SetCycle();
                    break;
                case ERountingMethod.PingPong:
                    SetPingPong();
                    break;
            }
        }

        private void LookAtNextPoint()
        {
            //transform.LookAt(_endPoint);
            transform.rotation = _compass.transform.rotation;
        }

        private bool IsNearCheckPoint()
        {
            _localPositionToCompass = _compass.transform.InverseTransformPoint(transform.position);
            Debug.Log($"_localPositionToCompass: {_localPositionToCompass}");
            return _localPositionToCompass.z >= 0;
        }

        private bool IsNearPoint(Vector3 point)
        {
            if (Vector3.Distance(transform.position, point) <= 0.1f)
                return true;

            return false;
        }

        private void SetOneway()
        {
            //if (IsNearPoint(_endPoint))
            if (IsNearCheckPoint())
            {
                if (curPointIndex < _movementPath.PointCount - 1)
                {
                    Debug.Log("Nextpoint!!! Go go!!!");
                    curPointIndex++;

                    var newEnd = _movementPath[curPointIndex];
                    if (newEnd != null)
                    {
                        _startPoint = _endPoint;
                        _endPoint = newEnd;
                    }

                    RepositionCompass();
                }
            }
        }

        private void SetCycle()
        {
            _startPoint = _movementPath[curPointIndex];
            curPointIndex = 0;
            _endPoint = _movementPath[curPointIndex];
            _isLastPointReached = false;
        }

        private void SetPingPong()
        {
            _movementPath.ListPathPoints.Reverse();
            Reset();
        }

        private void CheckMovementMethod()
        {
            switch (_movementMethod)
            {
                case EMovementMethod.FullSpeed:
                    MoveWith_MoveTowards();
                    break;
                case EMovementMethod.Lerp:
                    MoveWith_Lerp();
                    break;
                case EMovementMethod.Damp:
                    MoveWith_Damp();
                    break;
            }
        }

        private void MoveWith_MoveTowards()
        {
            //transform.position = Vector3.MoveTowards(transform.position,
            //    _endPoint, _movementSpeed * Time.deltaTime);

            transform.Translate(transform.forward * Time.deltaTime, Space.World);
        }

        private void MoveWith_Lerp()
        {
            transform.position = Vector3.Lerp(transform.position, _endPoint, 1 * Time.deltaTime);
        }

        private Vector3 _velocity;
        private void MoveWith_Damp()
        {
            transform.position = Vector3.SmoothDamp(transform.position, _endPoint, 
                ref _velocity, 10);
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawSphere(FakeTarget, 0.35f);
        }
    }
}
