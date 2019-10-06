using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityAdvance
{
    public class HelicopterController : MonoBehaviour
    {
        [SerializeField]
        private float _speed = 5f;

        [SerializeField]
        private Transform _heliModel;

        private Quaternion _firstRotation;
        private Quaternion _rotation;
        // Start is called before the first frame update
        void Start()
        {
            _firstRotation = _heliModel.rotation;
            _rotation = Quaternion.Euler(15f, 0f, 0f);
        }

        // Update is called once per frame
        void Update()
        {
            //_heliModel.rotation = Quaternion.Lerp(_rotation, _firstRotation, 2f * Time.time);

            if (Input.GetKey(KeyCode.UpArrow))
            {
                UpdateMovement();
                UpdateRotation();
                //_heliModel.rotation = rotation;
            }
        }

        private void UpdateRotation()
        {
            _heliModel.rotation = Quaternion.Lerp(_firstRotation, _rotation, 2f * Time.time);
        }

        private void UpdateMovement()
        {
            transform.Translate(transform.forward * _speed * Time.deltaTime);
        }
    }
}