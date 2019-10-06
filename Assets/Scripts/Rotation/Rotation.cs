using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityAdvance
{
    public class Rotation : MonoBehaviour
    {
        [SerializeField]
        private GameObject _myGun;
        [SerializeField]
        private GameObject _target;

        [SerializeField]
        private Transform _leftRightJoint;

        [SerializeField]
        private Transform _upDownJoint;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            //FaceToTarget();

            RotateLeftRight();
            RotateUpDown();
        }

        private void RotateLeftRight()
        {
            var aimingPosition = _target.transform.position;
            aimingPosition.y = _leftRightJoint.position.y;
            var direction = aimingPosition - _leftRightJoint.position;
            _leftRightJoint.forward = direction;
        }

        private void RotateUpDown()
        {
            _upDownJoint.LookAt(_target.transform);
        }

        //private Vector3 _horizontalCross;
        //private void FaceToTarget()
        //{
        //    //_myGun.transform.LookAt(_target.transform);

        //    _horizontalCross = Vector3.Cross( Vector3.up, (_target.transform.position - _myGun.transform.position).normalized );

            
        //}
    }
}