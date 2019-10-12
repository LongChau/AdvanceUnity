using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityAdvance
{
    public class HelicopterController : MonoBehaviour
    {
        [SerializeField]
        private float _movementSpeed = 5f;

        [SerializeField]
        private float _rotateSpeed = 5f;

        [SerializeField]
        private Transform _heliModel;

        [SerializeField]
        private Vector3 _flatForward;

        private Quaternion _firstRotation;
        private Quaternion _rotation;

        [SerializeField]
        private float _rotateAngle;

        [SerializeField]
        private float _maxLookDownAngle = 15f;

        [SerializeField]
        private Vector3 _lookDownForward;

        public Vector3 FlatForward
        {
            get
            {
                return Vector3.ProjectOnPlane(transform.forward, Vector3.up).normalized;
            }
            set => _flatForward = value;
        }

        public Vector3 LookDownForward
        {
            get
            {
                return Quaternion.AngleAxis(_maxLookDownAngle, _lookDownForward) * FlatForward;
            }
            set => _lookDownForward = value;
        }

        // Start is called before the first frame update
        void Start()
        {
            _firstRotation = _heliModel.rotation;
            _rotation = Quaternion.Euler(_maxLookDownAngle, 0f, 0f);
        }

        // Update is called once per frame
        void Update()
        {
            //_heliModel.rotation = Quaternion.Lerp(_rotation, _firstRotation, 2f * Time.time);

            if (Input.GetKey(KeyCode.UpArrow))
            {
                UpdateVerticalMovement();
                //_heliModel.rotation = rotation;

                if (Input.GetKey(KeyCode.RightArrow))
                {
                    UpdateRotationRight();
                }
                else if (Input.GetKey(KeyCode.LeftArrow))
                {
                    UpdateRotationLeft();
                }
            }
            else if (Input.GetKey(KeyCode.D))
            {
                UpdateRotationRight();
            }
            else if (Input.GetKey(KeyCode.A))
            {
                UpdateRotationLeft();
            }
            else
            {
                //_heliModel.rotation = Quaternion.Lerp(_heliModel.rotation, new Quaternion(0f, 0f, 0f, 0f), 2f * Time.fixedDeltaTime);
                UpdateRotationUp();
            }

            UpdateLeftRightMovement();
        }

        private void UpdateVerticalMovement()
        {
            UpdateMovement();
            UpdateRotationDown();
            //_heliModel.rotation = rotation;
        }

        private void UpdateLeftRightMovement()
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                MoveRight();
                RollRight();
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                MoveLeft();
                RollLeft();
            }
        }

        private void MoveLeft()
        {
            transform.Translate(Vector3.left * _movementSpeed * Time.deltaTime, Space.Self);
        }

        private void RollLeft()
        {
            var angle = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, _maxLookDownAngle);
            transform.rotation = Quaternion.Lerp(transform.rotation,
                angle,
                2f * Time.fixedDeltaTime);

            //transform.Rotate(Vector3.left, _maxLookDownAngle, Space.Self);
        }

        private void MoveRight()
        {
            transform.Translate(Vector3.right * _movementSpeed * Time.deltaTime, Space.Self);
        }

        private void RollRight()
        {
            var angle = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, -_maxLookDownAngle);
            transform.rotation = Quaternion.Lerp(transform.rotation,
               angle,
               2f * Time.fixedDeltaTime);

            //transform.Rotate(Vector3.right, -_maxLookDownAngle, Space.Self);
        }

        private void UpdateRotationDown()
        {
            //_heliModel.rotation = Quaternion.Lerp(_heliModel.rotation, _rotation, 2f * Time.fixedDeltaTime);
            //transform.rotation = Quaternion.Euler(_maxLookDownAngle, 0f, 0f);

            //transform.rotation = Quaternion.Lerp(transform.rotation, _rotation, 2f * Time.fixedDeltaTime);

            var angle = Quaternion.Euler(_maxLookDownAngle, transform.eulerAngles.y, transform.eulerAngles.z);
            transform.rotation = Quaternion.Lerp(transform.rotation, angle, 2f * Time.fixedDeltaTime);
        }

        private void UpdateRotationUp()
        {
            //transform.forward = FlatForward;
            //transform.rotation = Quaternion.Euler(0f, 0f, 0f);

            var angle = Quaternion.Euler(0f, transform.eulerAngles.y, transform.eulerAngles.z);
            transform.rotation = Quaternion.Lerp(transform.rotation,
                angle,
                2f * Time.fixedDeltaTime);
        }

        private void UpdateRotationRight()
        {
            //transform.Rotate(Vector3.up, _rotateAngle * Time.deltaTime);
            RotateAroundY(_rotateSpeed * Time.deltaTime);
        }

        private void UpdateRotationLeft()
        {
            //transform.Rotate(Vector3.up, - _rotateAngle * Time.deltaTime);
            RotateAroundY(-_rotateSpeed * Time.deltaTime);
        }

        private void RotateAroundY(float angle)
        {
            transform.rotation *= Quaternion.AngleAxis(angle, Vector3.up);
        }

        private void UpdateMovement()
        {
            //transform.Translate(transform.forward * _speed * Time.deltaTime);
            transform.position += FlatForward * _movementSpeed * Time.deltaTime;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawRay(transform.position, FlatForward * 10f);

            _rotateAngle = Vector3.Angle(_heliModel.forward, FlatForward);

            Gizmos.DrawRay(transform.position, LookDownForward * 10f);
        }
    }
}